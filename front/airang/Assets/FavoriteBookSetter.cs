using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class FavoriteBookSetter : MonoBehaviour
{
    public Toggle starBtn;

    private int book_id;

    public void Start()
    {
        book_id = BookManager.getInstance().CurBook.BookId;

        starBtn.isOn = true; // default value
        // check reading book is favorite
        foreach (var book in GameManager.getInstance().Favor_Books)
        {
            if (book.BookId == book_id)
                starBtn.isOn = false;
        }
        
        // toggle event setting
        starBtn.onValueChanged.AddListener(val => {
            requestSetFavoriteBook();
        });
    }

    public void requestSetFavoriteBook()
    {
        // set Favorite
        RESTManager.getInstance().Put("book/star/" + book_id, null);
    }
}
