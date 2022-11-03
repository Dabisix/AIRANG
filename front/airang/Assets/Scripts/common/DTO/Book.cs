using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book
{
    private int book_id;
    private string book_name;
    private int checkpoint_page;
    private int total_pages;

    private List<bool> use_ar;

    public Book(int book_id, string book_name, int total_pages, List<bool> use_ar)
    {
        this.book_id = book_id;
        this.book_name = book_name;
        this.checkpoint_page = 1;
        this.total_pages = total_pages;
        this.use_ar = use_ar;
    }

    public int BookId
    {
        get => book_id;
        set => book_id = value;
    }

    public string BookName
    {
        get => book_name;
        set => book_name = value;
    }

    public int checkpointPage
    {
        get => checkpoint_page;
        set => checkpoint_page = value;
    }

    public int TotalPages
    {
        get => total_pages;
        set => total_pages = value;
    }

    public List<bool> UseAR
    {
        get => use_ar;
    }
}
   
