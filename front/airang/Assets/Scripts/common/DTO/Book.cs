using System.Collections.Generic;

public class Book
{
    private int book_id;
    private string book_name;
    private int checkpoint_page;
    private int total_pages;

    private bool use_ar;

    private List<int> use_ar_pages;
    private List<string> e_scripts;
    private List<string> k_scripts;

    public Book(int book_id, string book_name, bool use_ar)
    {
        this.book_id = book_id;
        this.book_name = book_name;
        this.checkpoint_page = 1;
        this.use_ar = use_ar;

        this.use_ar_pages = new List<int>() { 0 };
        this.e_scripts = new List<string>();
        this.k_scripts = new List<string>();
    }

    public Book(int book_id, string book_name, int total_pages, bool use_ar, List<int> use_ar_pages, List<string> e_scripts, List<string> k_scripts)
    {
        this.book_id = book_id;
        this.book_name = book_name;
        this.checkpoint_page = 1;
        this.use_ar = use_ar;
        this.total_pages = total_pages;
        this.use_ar_pages = use_ar_pages;
        this.e_scripts = e_scripts;
        this.k_scripts = k_scripts;
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

    public bool UseAR
    {
        get => use_ar;
    }

    public List<int> UseARPages
    {
        get => use_ar_pages;
        set => use_ar_pages = value;
    }

    public List<string> EScripts
    {
        get => e_scripts;
        set => e_scripts = value;
    }

    public List<string> KScripts
    {
        get => k_scripts;
        set => k_scripts = value;
    }
}
   
