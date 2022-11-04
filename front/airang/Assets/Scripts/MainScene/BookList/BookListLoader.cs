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
        // get Container not used
        //contentContainer = this.gameObject.transform.Find("Viewport").Find("Content");
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

    void eraseAllBooks()
    {
        for(int i = 0; i < contentContainer.childCount; i++) {
            Destroy(contentContainer.GetChild(i).gameObject);
        }
    }

    // render at content in scrollView
    void renderBooks(List<Book> books)
    {
        for (int i = 0; i < books.Count; i++)
        {
            var item = Instantiate(bookItemPrefab);
            item.name = books[i].BookName;

            // set position
            item.transform.SetParent(contentContainer);
            item.transform.localScale = new Vector3(1.15f, 1.37f, 1);
            item.transform.localPosition = new Vector3(item.transform.position.x, item.transform.position.y, 0);
        }
    }
}
