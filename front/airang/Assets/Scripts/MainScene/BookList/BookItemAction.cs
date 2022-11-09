using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private const int NUM_DEFAULT_BOOK_IMAGE = 4;

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

        // for random default image
        System.Random rand = new System.Random();
        if (book_cover == null) // set default image with random
            book_cover = Resources.Load<Sprite>(BOOK_COVER_PATH + "default" + rand.Next(1, NUM_DEFAULT_BOOK_IMAGE + 1));
   
        GetComponent<Image>().sprite = book_cover;
    }

    public void setBookTitle()
    {
        if (bookInfo == null) return;

        // set Title
        GameObject text = this.gameObject.transform.Find("Title").gameObject;
        text.GetComponent<TextMeshProUGUI>().text = bookInfo.BookName;
    }

    // enable AR tag if book support AR
    public void checkARTag()
    {
        if (bookInfo == null) return;

        if(AR_tag_image == null)
            AR_tag_image = this.gameObject.transform.Find("ARTag").gameObject;
        AR_tag_image.SetActive(bookInfo.UseAR);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (bookInfo == null) return;

        BookManager bm = BookManager.getInstance();

        bm.CurBook = bookInfo;
        bm.CurPage = 0;
        bm.changeScene(true);
    }
}
