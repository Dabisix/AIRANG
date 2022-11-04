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
    private List<string> scripts = new List<string>();  
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
        get => scripts[cur_page];
    }

    // check book info and ready for reading
    public void InitBook()
    {
        cur_book = GameManager.getInstance().CurBook;

        loadScripts();
        loadContents(GetContents());
    }

    public List<GameObject> GetContents()
    {
        return contents;
    }

    public void loadContents(List<GameObject> contents)
    {
        GameObject[] objects = Resources.LoadAll<GameObject>("book" + cur_book.BookId);

        contents.Clear();
        contents.Add(null);
        for (int i = 0; i < objects.Length; i++)
            contents.Add(objects[i]);
    }

    public bool loadScripts()
    {
        scripts.Clear();

        //" + cur_book.BookId
        RESTManager.getInstance().Get("book/3").Then(res =>
        {
            Debug.Log(res.Text);
        }).Catch(err =>
        {

        });

        return true;
    }

    public void changeScene()
    {
        string next_scene_name = this.gameObject.scene.name;
        
        if (cur_book.UseARPages[cur_page]) // use AR
            next_scene_name = "ARBookScene";
        else // not use AR
            next_scene_name = "BookScene";

        if (cur_page <= 0 || cur_page > cur_book.TotalPages)
            next_scene_name = "BookSettingScene";

        Debug.Log("changeScene : page - " + cur_page);
        SceneManager.LoadScene(next_scene_name);
    }
}
