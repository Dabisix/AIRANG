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
            InitBook();
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

    private Book cur_book = new Book(0, "book1", 4, new List<bool> { true, true, true, false, false });
    private int cur_page = 1; // current page

    // each pages infomations
    private List<GameObject> contents = new List<GameObject>();
    private List<string> scripts;
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
        set => cur_page = (value < 1 || value > TotalPages ? 1 : value);
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
        // cur_book = GameManager.getInstance().CurBook;

        loadScripts();
        loadContents(GetContents());
    }

    public List<GameObject> GetContents()
    {
        return contents;
    }

    public void loadContents(List<GameObject> contents)
    {
        Debug.Log("bookname " + cur_book.BookName);
        GameObject[] objects = Resources.LoadAll<GameObject>(cur_book.BookName);

        contents.Clear();
        contents.Add(null);
        for (int i = 0; i < objects.Length; i++)
            contents.Add(objects[i]);
    }

    public bool loadScripts()
    {
        // TODO : replace with REST API
        scripts = new List<string>();

        scripts.Add("asdf1");
        scripts.Add("asdf2");

        return true;
    }

    public void changeScene()
    {
        string next_scene_name = this.gameObject.scene.name;
        
        if (cur_book.UseAR[cur_page]) // use AR
            next_scene_name = "ARBookScene";
        else // not use AR
            next_scene_name = "BookScene";

        Debug.Log("changeScene " + cur_page);
        SceneManager.LoadScene(next_scene_name);
    }
}
