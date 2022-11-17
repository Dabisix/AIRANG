using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookManager : MonoBehaviour
{
    #region SINGLETON
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
    #endregion

    #region BOOKINFO
    private Book cur_book;
    private int cur_page = 1; // current page

    private bool lang; // true : Korea, false : English
    private int narr;

    // each pages infomations
    private List<GameObject> contents = new List<GameObject>();

    // AR Constant
    Dictionary<int, Dictionary<int, int>> AR_INFO;

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

    public int ARType
    {
        get => cur_book.UseARPages[cur_page];
    }

    public GameObject Content
    {
        get => contents[cur_page];
    }

    public bool Lang
    {
        get => lang;
        set => lang = value;
    }

    public int Narration
    {
        get => narr;
        set => narr = value;
    }

    public string Script
    {
        get
        {
            if (lang)
                return cur_book.KScripts[cur_page];
            else
                return cur_book.EScripts[cur_page];
        }
    }
    #endregion

    private void Start()
    {
        // get hardcoded info
        initARInfo();
    }

    #region ARPAGE
    private void initARInfo()
    {
        AR_INFO = new Dictionary<int, Dictionary<int, int>>();

        // 토끼와 거북이
        Dictionary<int, int> tmp_AR_pages = new Dictionary<int, int>();
        tmp_AR_pages.Add(5, 1); // Book page number, AR Info
        tmp_AR_pages.Add(8, 2);

        AR_INFO.Add(3, tmp_AR_pages); // Book ID, AR Info dic

        // 백설공주
        Dictionary<int, int> tmp_AR_pages2 = new Dictionary<int, int>();
        tmp_AR_pages2.Add(3, 3);
        tmp_AR_pages2.Add(9, 4);
        tmp_AR_pages2.Add(17, 5);
        tmp_AR_pages2.Add(25, 6);
        AR_INFO.Add(2, tmp_AR_pages2);
    }

    private void setARInfo()
    {
        // get AR info
        Dictionary<int, int> tmp_saved_use_AR = AR_INFO.GetValueOrDefault(cur_book.BookId);

        List<int> tmp_use_AR = new List<int>();
        for (int i = 0; i <= cur_book.TotalPages; i++)
            tmp_use_AR.Add(0);

        if (tmp_saved_use_AR != null)
        {
            foreach (var item in tmp_saved_use_AR)
            {
                tmp_use_AR[item.Key] = item.Value;
            }

        }
        cur_book.UseARPages = tmp_use_AR;
    }
    #endregion

    // check book info and ready for reading
    public void InitBook()
    {
        getBookSetting();
        getCheckPoint();
        loadContents();
        getBookInfo();
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

            cur_book.KScripts.Add(""); // for index 0
            cur_book.EScripts.Add("");

            // set Scripts
            JObject bookInfo = JObject.Parse(res.Text);
            foreach (string text in bookInfo["kcontent"])
                cur_book.KScripts.Add(text);
            foreach (string text in bookInfo["econtent"])
                cur_book.EScripts.Add(text);

            // set total pages
            cur_book.TotalPages = cur_book.KScripts.Count;

            // set AR info (in code)
            setARInfo();

            changeScene(true);
        }).Catch(err =>
        {
            // TODO : check network warn
            Debug.Log("error on get BookInfo");
        });

        return true;
    }

    #region ACCESSFILE
    public void getCheckPoint()
    {
        cur_page = PlayerPrefs.GetInt("book" + cur_book.BookId, 1);
    }

    public void setCheckPoint(int page)
    {
        PlayerPrefs.SetInt("book" + cur_book.BookId, page);
    }

    public void getBookSetting()
    {
        // settings
        lang = PlayerPrefs.GetInt("lang", 0) == 0 ? true : false;
        narr = PlayerPrefs.GetInt("narr", 1);
    }
    #endregion

    // 부모 녹음 파일 확인
    public bool needRecord()
    {
        // 폴더 이름 확인하기
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            string FileDirectory = dir.Name;
            // 현재 책아이디랑 같은 폴더가 있으면 부모 녹음 파일이 있는것, 녹음 필요없음
            if (cur_book.BookId.ToString() == FileDirectory)
                return false;
        }
        return true;
    }

    public void goToMain()
    {
        // request book list
        GameManager gm = GameManager.getInstance();

        RESTManager.getInstance().Get("book/star").Then(res =>
        {
            // get Favorite books
            gm.Favor_Books = gm.ResponseToBookList(res.Text);
            return RESTManager.getInstance().Get("book/log");
        }).Then(res =>
        {
            // get Recently books
            gm.RecentBooks = gm.ResponseToBookList(res.Text);
            return RESTManager.getInstance().Get("book/recommend");

        }).Then(res =>
        {
            gm.ResponseRecommendToBookList(res.Text);

            // move to main Scene
            Debug.Log("changeScene : page - " + cur_page);
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "MainScene"));
        }).Catch(err =>
        {
            gm.alert("책 목록을 불러오는중 \n문제가 발생하였습니다");
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, "MainScene"));
        });
    }

    public void changeScene(bool isFade = false)
    {
        string next_scene_name = this.gameObject.scene.name;

        // book ended or start
        if (cur_page <= 0 || cur_page > cur_book.TotalPages)
        {
            // reset checkpoint
            setCheckPoint(1);

            goToMain();
            return;
        }
        else
        {
            if (cur_book.UseARPages[cur_page] > 0) // use AR
                next_scene_name = "ARBookScene";
            else // not use AR
                next_scene_name = "BookScene";

            // not use ARBook(only text)
            if (!cur_book.UseAR)
                next_scene_name = "NonPicBookPagingScene";
        }

        Debug.Log("changeScene : page - " + cur_page);
        if (isFade)
            StartCoroutine(GameObject.FindObjectOfType<SceneFader>().FadeAndLoadScene(SceneFader.FadeDirection.In, next_scene_name));
        else
            SceneManager.LoadScene(next_scene_name);
    }
}
