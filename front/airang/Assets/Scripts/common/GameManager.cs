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
        } catch(Exception e)
        {
            // TODO : 앱 손상되었음
            Debug.LogError("Application file damaged " + e.Message);
        }
    }

    private List<Book> books;

    private Book cur_book; // current book

    public Book CurBook
    {
        get => cur_book;
        set => cur_book = value;
    }
}
