using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookItemAction : MonoBehaviour
    ,IPointerClickHandler
{
    private GameObject AR_tag_image;
    private Book bookInfo;

    private const string BOOK_COVER_PATH = "BookCovers/";

    public Book Book
    {
        get => bookInfo;
        set => bookInfo = value;
    }
    
    // function must called after Bookinfo assigned
    public void setBookCoverFromResource()
    {
        if (bookInfo == null) return;

        // load book cover splite with Book ID
        Sprite book_cover = Resources.Load<Sprite>(BOOK_COVER_PATH + "Book" + bookInfo.BookId);

        if (book_cover == null) // set default image
            book_cover = Resources.Load<Sprite>(BOOK_COVER_PATH + "default");
   
        GetComponent<Image>().sprite = book_cover;
    }

    // enable AR tag if book support AR
    public void checkARTag()
    {
        if (bookInfo == null) return;

        if(AR_tag_image == null)
            AR_tag_image = this.gameObject.transform.GetChild(0).gameObject;
        AR_tag_image.SetActive(bookInfo.UseAR);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (bookInfo == null) return;

        BookManager bm = BookManager.getInstance();

        bm.CurBook = bookInfo;
        bm.CurPage = 0;
        bm.changeScene();
    }
}
