using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMainCanvasButton : MonoBehaviour
{
    public MainMainCamera my_camera;
    public GameObject[] navigation;
    public GameObject searchBar;

    private bool isSearch = false;
    public void clickSearchBtn()
	{
		if (!isSearch)
		{
            searchBar.SetActive(true);
            isSearch = true;
		}
		else
		{
            searchBar.SetActive(false);
            isSearch = false;
		}
	}
    public void ClickBtnHome()
    {
        SetNavigationActive();
        MoveCamera(2);

        // Active search button
        searchBar.SetActive(false);
    }
    public void ClickBtnLibrary()
    {
        // instantiate books list
        // loadBookListWithViewName("BestBooksScrollView");
        // loadBookListWithViewName("BooksScrollView");

        MoveCamera(0);
        navigation[0].SetActive(false);
    }
    public void ClickBtnMyDesk()
    {
        // instantiate books list
        // loadBookListWithViewName("FavorBookScrollView");
        // loadBookListWithViewName("RecentBookScrollView");

        MoveCamera(1);
        navigation[1].SetActive(false);

        // Active search button
        searchBar.SetActive(false);
    }

    private void loadBookListWithViewName(string scroll_view_name)
    {
        GameObject recent_book_scroll_view = GameObject.Find(scroll_view_name);
        BookListLoader recent_book_book_loader = recent_book_scroll_view.GetComponent<BookListLoader>();
        recent_book_book_loader.loadBooks(false);
    }

    public void MoveCamera(int index)
    {
        my_camera.MainMoveCamera(index);
    }

    public void SetNavigationActive()
    {
        for (int i = 0; i < navigation.Length; ++i)
        {
            navigation[i].SetActive(true);
        }
    }
}
