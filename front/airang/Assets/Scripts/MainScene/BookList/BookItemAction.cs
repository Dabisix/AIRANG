using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BookItemAction : MonoBehaviour
    ,IPointerClickHandler
{
    private GameObject AR_tag_image;
    private Book bookInfo;

    public Book Book
    {
        get => bookInfo;
        set => bookInfo = value;
    }

    private void Start()
    {
        
    }

    public void checkARTag()
    {
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
