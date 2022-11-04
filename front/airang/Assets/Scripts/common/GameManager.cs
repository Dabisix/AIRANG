using Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    static GameManager instance = null;

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

    private void getAllBooksList()
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
        }).Catch(err =>
        {
            Debug.Log(err.Message);
        });
    }

    private void Start()
    {
        /*RESTManager.getInstance().Post("user/signup", new User.UserController
        {
            email = "wsu223@gmail.com",
            name = "test",
            pw = "test"
        }, false).Then(res =>
        {
            Debug.Log("회원가입 성공");
        });*/

        /* RESTManager.getInstance().Post("user", new User.UserController
         {
             email = "wsu223@gmail.com",
             pw = "test"
         }, false).Then(res =>
         {
             JObject json = JObject.Parse(res.Text);

             PlayerPrefs.SetString("refreshToken", (string)json["token"]["refresh_TOKEN"]);
             PlayerPrefs.SetString("accessToken", (string)json["token"]["access_TOKEN"]);

             return;*/

        getAllBooksList();
    }


    // json book list to List of Book object
    private List<Book> ResponseToBookList(string book_list)
    {
        JObject json = JObject.Parse(book_list);

        List<Book> ret = new List<Book>();
        foreach (var book in json["booklist"])
            ret.Add(new Book((int)book["bid"], (string)book["title"], (bool)book["aflag"]));

        return ret;
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
}
