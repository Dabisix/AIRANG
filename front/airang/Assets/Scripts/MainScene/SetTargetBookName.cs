using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTargetBookName : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public bool isStar;
    void Start()
    {
		if (isStar)
		{
            buttonText.text = "즐겨찾기한 " + GameManager.getInstance().targetStarBook.BookName + " 관련된 책입니다.";
        }
		else
		{
            buttonText.text = "방금 읽은 " + GameManager.getInstance().targetLogBook.BookName + " 관련된 책입니다.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
