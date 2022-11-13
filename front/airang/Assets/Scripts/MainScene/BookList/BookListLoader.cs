using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BookListLoader : MonoBehaviour
{
    [SerializeField]
    GameObject bookItemPrefab;
    [SerializeField]
    Transform contentContainer;

    public enum BookListType
    {
        FavorBooks,
        RecentBooks,
        Books,
        PopularBooks,
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
            }
        }
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
            book.transform.localScale = new Vector3(2.0f, 1.25f, 1);
            book.transform.localPosition = new Vector3(book.transform.position.x, book.transform.position.y, 0);
            book.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
