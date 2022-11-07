using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // AR Constant
    Dictionary<int, Dictionary<int, int>> AR_INFO;

    private void initARInfo()
    {
        AR_INFO = new Dictionary<int, Dictionary<int, int>>();

        // ≈‰≥¢øÕ ∞≈∫œ¿Ã
        Dictionary<int, int> tmp_AR_pages = new Dictionary<int, int>();
        tmp_AR_pages.Add(5, 1); // Book page number, AR Info
        tmp_AR_pages.Add(9, 2);

        AR_INFO.Add(3, tmp_AR_pages); // Book ID, AR Info dic

        // πÈº≥∞¯¡÷
        // AR_INFO.Add(3, )
    }

    private void Start()
    {
        initARInfo();
    }

    private void setARInfo()
    {
        // get AR info
        Dictionary<int, int> tmp_saved_use_AR = AR_INFO.GetValueOrDefault(cur_book.BookId);

        List<int> tmp_use_AR = new List<int> ();
        Debug.Log(cur_book.TotalPages);
        for (int i = 0; i <= cur_book.TotalPages; i++)
            tmp_use_AR.Add(0);

        if(tmp_saved_use_AR != null)
        {
            foreach (var item in tmp_saved_use_AR)
            {

                tmp_use_AR[item.Key] = item.Value;
            }
                
        }
            

        cur_book.UseARPages = tmp_use_AR;
        Debug.Log(tmp_use_AR);
    }

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

        getBookInfo();
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
            cur_book.KScripts.Clear();
            cur_book.EScripts.Clear();

            JObject bookInfo = JObject.Parse(res.Text);
            foreach (string text in bookInfo["kcontent"])
                cur_book.KScripts.Add(text);
            foreach (string text in bookInfo["econtent"])
                cur_book.EScripts.Add(text);

            cur_book.TotalPages = cur_book.KScripts.Count;
            setARInfo();
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
