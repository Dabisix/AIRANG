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

    private bool parent_narr = false;
    private bool need_record;

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

    public bool ParentNarr
    {
        get => parent_narr;
        set => parent_narr = value;
    }

    public bool NeedRecord
    {
        get => need_record;
        set => need_record = value;
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
        // AR_INFO.Add(3, )
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
        loadContents();
        getBookInfo();
        
        // need_record = checkRecordVoice();
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
        // get File
        FileManager fb = FileManager.getInstance();
        SavedData tmp = fb.loadData();

        // get CheckPoint
        if (tmp.page_checkPoint.ContainsKey(cur_book.BookId))
            cur_page = tmp.page_checkPoint[cur_book.BookId];
        else
            cur_page = 1;
    }

    public void setCheckPoint()
    {
        // get File
        FileManager fb = FileManager.getInstance();
        SavedData tmp = fb.loadData();

        if (cur_page == 0) cur_page = 1;

        // save checkPoint
        BookManager bm = BookManager.getInstance();
        if (tmp.page_checkPoint.ContainsKey(cur_book.BookId))
            tmp.page_checkPoint[cur_book.BookId] = cur_page;
        else
            tmp.page_checkPoint.Add(cur_book.BookId, cur_page);

        fb.saveData(tmp);
    }

    public void getBookSetting()
    {
        // get File
        FileManager fb = FileManager.getInstance();
        SavedData tmp = fb.loadData();

        // settings
        lang = tmp.language;
        narr = tmp.narration;
    }

    public void setBookSetting(bool _lang, int narr)
    {
        // get File
        FileManager fb = FileManager.getInstance();
        SavedData tmp = fb.loadData();

        // setting
        tmp.language = _lang;


        fb.saveData(tmp);
    }
#endregion

    // 부모 녹음 파일 확인
    public bool checkRecordVoice()
    {
        Debug.Log("파일 확인해보러 들어왔음");
        //var fileNames = Directory.GetFiles(Application.persistentDataPath, "*.wav");

        // 폴더 이름 확인하기
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            string FileDirectory = dir.Name;
            Debug.Log("폴더 이름 : " + FileDirectory);
            // 현재 책아이디랑 같은 폴더가 있으면 부모 녹음 파일이 있는것, 녹음 필요없음
            if (cur_book.BookId.ToString() == FileDirectory)
            {
                return false;
            }
        }
        Debug.Log("녹음 파일 없다");
        return true;
    }

    public void changeScene(bool isFade = false)
    {
        string next_scene_name = this.gameObject.scene.name;

        // book ended or start
        if (cur_page <= 0 || cur_page > cur_book.TotalPages)
        {
            setCheckPoint();
            next_scene_name = "MainScene";
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
