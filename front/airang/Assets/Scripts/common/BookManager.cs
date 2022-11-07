using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookManager : MonoBehaviour
{
    static BookManager instance = null;

    // singleton Pattern implemented
    public static BookManager getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // if object is corupted
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private Book cur_book;
    private int cur_page = 1; // current page

    // each pages infomations
    private List<GameObject> contents = new List<GameObject>();


    public Book CurBook
    {
        get => cur_book;
        set => cur_book = value;
    }

    public string BookName
    {
        get => cur_book.BookName;
    }

    public int TotalPages
    {
        get => cur_book.TotalPages;
    }

    public int CurPage
    {
        get => cur_page;
        set => cur_page = value;
    }

    public GameObject Content
    {
        get => contents[cur_page];
    }

    public string Script
    {
        get
        {
            if (cur_book.Lang)
                return cur_book.KScripts[cur_page];
            else
                return cur_book.EScripts[cur_page];
        }
    }

    // check book info and ready for reading
    public void InitBook()
    {
        cur_book = BookManager.getInstance().CurBook;

        if(cur_book.KScripts.Count != 0) getBookInfo();
        loadContents();
    }

    public void loadContents()
    {
        GameObject[] objects = Resources.LoadAll<GameObject>("book" + cur_book.BookId);

        contents.Clear();
        contents.Add(null);
        for (int i = 0; i < objects.Length; i++)
            contents.Add(objects[i]);
    }

    public bool getBookInfo()
    {
        // get Script info and totalpage from server
        RESTManager.getInstance().Get("book/" + cur_book.BookId).Then(res =>
        {
            JObject bookInfo = JObject.Parse(res.Text);

            foreach (string text in bookInfo["kcontent"])
                cur_book.KScripts.Add(text);
            foreach (string text in bookInfo["econtent"])
                cur_book.EScripts.Add(text);

            Debug.Log(res.Text);
            cur_book.TotalPages = cur_book.KScripts.Count;
        }).Catch(err =>
        {
            // TODO : check network warn
            Debug.Log("error on get BookInfo");
        });

        return true;
    }

    public void changeScene(bool isFade = false)
    {
        string next_scene_name = this.gameObject.scene.name;
        
        if (cur_book.UseARPages[cur_page] > 0) // use AR
            next_scene_name = "ARBookScene";
        else // not use AR
            next_scene_name = "BookScene";

        // not use ARBook(only text)
        if (!cur_book.UseAR)
            next_scene_name = "NonPicBookPagingScene";

        // book ended or start

        Debug.Log(cur_page);
        if (cur_page <= 0)
            next_scene_name = "BookSettingScene";
        else if(cur_page > cur_book.TotalPages)
            next_scene_name = "MainScene";


        Debug.Log("changeScene : page - " + cur_page);
        if(isFade)
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, next_scene_name));
        else
            SceneManager.LoadScene(next_scene_name);
    }
}
