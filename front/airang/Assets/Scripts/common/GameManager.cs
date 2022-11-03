using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // for DEBUG
        books = new List<Book>();

        List<bool> useAR = new List<bool>();
        for (int i = 0; i <= 17; i++)
            useAR.Add(false);

        useAR[5] = true;
        useAR[9] = true;

        books.Add(new Book(0, "RabbitAndTurtle", 17, useAR));

        List<bool> useAR2 = new List<bool>();
        for (int i = 0; i <= 32; i++)
            useAR.Add(false);

        books.Add(new Book(1, "SnowWhite", 32, useAR2));

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

    private void Start()
    {
        // get book list from local file
        try
        {
            books = FileManager.getInstance().loadData().Books;
        } catch (Exception e)
        {
            // TODO : 앱 손상되었음
            Debug.LogError("Application file damaged " + e.Message);
        }
    }

    private List<Book> books;

    public List<Book> Books
    {
        get
        {
            if (books == null)
                return new List<Book>();
            else
                return books;
        }
    }

    private Book cur_book; // current book

    public Book CurBook
    {
        get => cur_book;
        set => cur_book = value;
    }

    public bool setCurrentBookWithName(string bookname)
    {
        for (int i = 0; i < books.Count; i++)
        {
            if (bookname.Equals(books[i].BookName))
            {
                cur_book = books[i];
                return true;
            }
        }
        return false;
    }
}
