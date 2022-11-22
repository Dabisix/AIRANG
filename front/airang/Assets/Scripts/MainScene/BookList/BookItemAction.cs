using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BookItemAction : MonoBehaviour
    ,IPointerClickHandler
{
    private GameObject AR_tag_image;
    private Book bookInfo;

    private const string BOOK_COVER_PATH = "BookCovers/";
    private const int NUM_DEFAULT_BOOK_IMAGE = 6;

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

        // read book start
        BookManager bm = BookManager.getInstance();
        bm.CurBook = bookInfo;

        // confirm recording
        int needRecording = GameManager.getInstance().AskingRecording;
        if (needRecording == 0)
        {
            GameManager.getInstance().confirm("읽는 모습을 녹화하시겠습니까?", () =>
            {
                WebCamController.getInstance().startRecording();
                WebCamController.getInstance().WebCam.Pause();

                // add read log
                RESTManager.getInstance().Put("book/log/" + bm.CurBook.BookId, null);
                bm.InitBook();
            }, () => {
                // add read log
                RESTManager.getInstance().Put("book/log/" + bm.CurBook.BookId, null);
                bm.InitBook();
            });
        } else if(needRecording == 1) 
        {
            // add read log
            RESTManager.getInstance().Put("book/log/" + bm.CurBook.BookId, null);
            bm.InitBook();
        } else // need Recording == 2
        {
            WebCamController.getInstance().startRecording();
            WebCamController.getInstance().WebCam.Pause();

            // add read log
            RESTManager.getInstance().Put("book/log/" + bm.CurBook.BookId, null);
            bm.InitBook();
        }
    }
}
