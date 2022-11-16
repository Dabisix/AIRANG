using Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{ 
    static GameManager instance = null;

    // alert object
    GameObject alertBoard;
    // comfrim object
    GameObject confirmBoard;

    // singleton Pattern implemented
    public static GameManager getInstance()
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

    public void getAllBooksList()
    {
        RESTManager.getInstance().Post("book", new BookSearchOption(0, 0, "", -1)).Then(res =>
        {
            // get All books
            books = ResponseToBookList(res.Text);

            return RESTManager.getInstance().Post("book", new BookSearchOption(0, 2, "", 0));
        }).Then(res =>
        {
            // get hot books
            popular_books = ResponseToBookList(res.Text);

            return RESTManager.getInstance().Get("book/star");
        }).Then(res =>
        {
            // get Favorite books
            favor_books = ResponseToBookList(res.Text);

            return RESTManager.getInstance().Get("book/log");
        }).Then(res =>
        {
            // get Recently books
            books_log = ResponseToBookList(res.Text);
            return RESTManager.getInstance().Get("book/recommend");
        }).Then( res => {
            ResponseRecommendToBookList(res.Text);
        }).Catch(err =>
        {
            // TODO : 네트워크 환경 확인 메세지
            Debug.Log(err.Message);

            // When server is closing
            // books = FileManager.getInstance().loadData().Books;
        });

        // save book list
        // FileManager.getInstance().saveData(new SavedData(books));
    }

    private void Start()
    {
        // getAllBooksList();
    }

    // json book list to List of Book object
    private List<Book> ResponseToBookList(string book_list)
    {
        JObject json = JObject.Parse(book_list);

        List<Book> ret = new List<Book>();
        Debug.Log(json["booklist"]);
        foreach (var book in json["booklist"])
            ret.Add(new Book((int)book["bid"], (string)book["title"], (bool)book["aflag"]));

        return ret;
    }
    private void ResponseRecommendToBookList(string book_list)
    {
        JObject json = JObject.Parse(book_list);

        Debug.Log(json);
        if(json["starRec"].Type is JTokenType.Null)
		{
            Debug.Log("즐겨찾기한 책 목록이 없음!");
            targetStarBook = null;
            books_starrecommend = null;

        }
		else
		{
            var starbook = json["starRec"]["targetBook"];
            targetStarBook = new Book((int)starbook["bid"], (string)starbook["title"], (bool)starbook["aflag"]);
            List<Book> starRec = new List<Book>();
            foreach (var book in json["starRec"]["recList"])
            {
                starRec.Add(new Book((int)book["bid"], (string)book["title"], (bool)book["aflag"]));
            }
            books_starrecommend = starRec;
            Debug.Log(books_starrecommend);
        }

        if (json["logRec"].Type is JTokenType.Null)
        {
            Debug.Log("읽었던 책 목록이 없음!");
            targetLogBook = null;
            books_logrecommend = null;
        }
        else
        {
            var logbook = json["logRec"]["targetBook"];
            targetLogBook = new Book((int)logbook["bid"], (string)logbook["title"], (bool)logbook["aflag"]);

            List<Book> logRec = new List<Book>();
            foreach (var book in json["logRec"]["recList"])
            {
                logRec.Add(new Book((int)book["bid"], (string)book["title"], (bool)book["aflag"]));
            }
            books_logrecommend = logRec;
            Debug.Log(books_logrecommend);
        }
    }

    // book lists from server
    private List<Book> books = new List<Book>();
    private List<Book> popular_books = new List<Book>();

    public List<Book> Books
    {
        get => books ?? new List<Book>();
        set => books = value;
    }
    public List<Book> PopularBooks
    {
        get => popular_books ?? new List<Book>();
        set => popular_books = value;
    }


    // my desk area book list
    private List<Book> favor_books = new List<Book>();
    private List<Book> books_log = new List<Book>();
    private List<Book> books_starrecommend = new List<Book>();
    private List<Book> books_logrecommend = new List<Book>();

    public Book targetStarBook;
    public Book targetLogBook;

	public List<Book> Favor_Books
    {
        get => favor_books ?? new List<Book>();
        set => favor_books = value;
    }
    public List<Book> RecentBooks
    {
        get => books_log ?? new List<Book>();
        set => books_log = value;
    }

    public List<Book> RecommendStarBooks
    {
        get => books_starrecommend ?? new List<Book>();
        set => books_starrecommend = value;
    }
    public List<Book> RecommendLogBooks
    {
        get => books_logrecommend ?? new List<Book>();
        set => books_logrecommend = value;
    }

    public void alert(string message)
    {
        // get Alert Prefab
        GameObject alertBoardPrefab = Resources.Load<GameObject>("Prefabs/common/Alert");
        if (alertBoard != null)
            Destroy(alertBoard);

        alertBoard = Instantiate(alertBoardPrefab);
        alertBoard.SetActive(true);

        alertBoard.GetComponentInChildren<TextSetter>().setText(message);
    }

    public delegate void m_Delegate();

    public void confirm(string message, m_Delegate onConfirm, m_Delegate onCancle = null)
    {
        // get Alert Prefab
        GameObject alertBoardPrefab = Resources.Load<GameObject>("Prefabs/common/Confirm");
        if (confirmBoard != null)
            Destroy(confirmBoard);

        confirmBoard = Instantiate(alertBoardPrefab);
        confirmBoard.SetActive(true);
        confirmBoard.GetComponentInChildren<TextSetter>().setText(message);

        // add onClick callback
        Button[] btns = confirmBoard.GetComponentsInChildren<Button>();
        foreach(Button btn in btns)
        {
            if(btn.name == "Cancle")
                if(onCancle != null) btn.onClick.AddListener(() => onCancle());
            else
                if(onConfirm != null) btn.onClick.AddListener(() => onConfirm());
        }
    }
}
