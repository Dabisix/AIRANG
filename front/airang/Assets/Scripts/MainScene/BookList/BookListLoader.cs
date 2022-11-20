using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookListLoader : MonoBehaviour
{
    [SerializeField]
    GameObject bookItemPrefab;
    [SerializeField]
    Transform contentContainer;

    public TextMeshProUGUI ListNullSign;
    

    public enum BookListType
    {
        FavorBooks,
        RecentBooks,
        Books,
        PopularBooks,
        ARBooks,
        StarRecommend,
        LogRecommend,
    };

    [SerializeField]
    public BookListType book_list_type;


	private void Start()
    {
        loadBooks(false);
        
    }




    // load books with type of book list
    public void loadBooks(bool mustRender)
    {
        if (mustRender || contentContainer.childCount == 0)
        {
            eraseAllBooks();
            switch (book_list_type)
            {
                case BookListType.Books:
                    // reder All books
                    renderBooks(GameManager.getInstance().Books);
                    break;
                case BookListType.PopularBooks:
                    // reder Popular books
                    renderBooks(GameManager.getInstance().PopularBooks);
                    break;
                case BookListType.FavorBooks:
                    // reder favorite books
                    renderBooks(GameManager.getInstance().Favor_Books);
                    break;
                case BookListType.RecentBooks:
                    // reder recently saw books
                    renderBooks(GameManager.getInstance().RecentBooks);
                    break;
                case BookListType.ARBooks:
                    renderBooks(FilterARBooks(GameManager.getInstance().Books));
                    break;
                case BookListType.StarRecommend:
                    renderBooks(GameManager.getInstance().RecommendStarBooks);
                    if (GameManager.getInstance().targetStarBook == null)
                    {
                        ListNullSign.text = "즐겨찾기한 책이 없습니다!";
						// if booklist is null
					}
					else
					{
                        ListNullSign.text = "즐겨찾기한 <b>[" + GameManager.getInstance().targetStarBook.BookName + "]</b> 관련된 책입니다.";
                    }
                    break;
                case BookListType.LogRecommend:
                    renderBooks(GameManager.getInstance().RecommendLogBooks);
                    if (GameManager.getInstance().targetLogBook == null)
                    {
                        ListNullSign.text = "읽었던 책이 없습니다!";
                        // if booklist is null
                    }
                    else
                    {
                        ListNullSign.text = "방금 읽은 <b>[" + GameManager.getInstance().targetLogBook.BookName + "]</b> 관련된 책입니다.";
                    }
                    break;
            }
        }
    }

    public List<Book> FilterARBooks(List<Book> books)
	{
        List<Book> ARBooks = new List<Book>();
        for(int i = 0; i < books.Count; ++i)
		{
			if (books[i].UseAR)
			{
                ARBooks.Add(books[i]);
			}
		}
        return ARBooks;
	}

    public void eraseAllBooks()
    {
        for(int i = 0; i < contentContainer.childCount; i++) {
            Destroy(contentContainer.GetChild(i).gameObject);
        }
    }

    // render at content in scrollView
    public void renderBooks(List<Book> books)
    {
        
        for (int i = 0; i < books.Count; i++)
        {
            GameObject book = Instantiate(bookItemPrefab);
            // set Book Information
            book.name = "Book" + books[i].BookId;
            book.GetComponent<BookItemAction>().Book = books[i];
            book.GetComponent<BookItemAction>().checkARTag();
            book.GetComponent<BookItemAction>().setBookCoverFromResource();
            book.GetComponent<BookItemAction>().setBookTitle();

            // set position
            book.transform.SetParent(contentContainer);
            book.transform.localScale = new Vector3(2.0f, 1.20f, 1);
            book.transform.localPosition = new Vector3(book.transform.position.x, book.transform.position.y, 0);
            book.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
