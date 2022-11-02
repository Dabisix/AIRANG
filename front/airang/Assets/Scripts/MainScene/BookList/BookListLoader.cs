using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BookListLoader : MonoBehaviour
{
    [SerializeField]
    GameObject bookItemPrefab;
    [SerializeField]
    Transform contentContainer;

    // Start is called before the first frame update
    void Start()
    {
        List<Book> books = GameManager.getInstance().Books;

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
