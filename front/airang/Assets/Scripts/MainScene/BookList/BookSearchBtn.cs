using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class BookSearchBtn : MonoBehaviour
{
    private TMP_InputField inputField;

    [SerializeField]
    public GameObject searchField;

    [SerializeField]
    GameObject bookListScrollView; // import All books View
    BookListLoader bookListLoader;

    [SerializeField]
    Camera mainCamera;
    MainMainCamera cameraController;

    private string prev_keyword = "";

    // Start is called before the first frame update
    void Start()
    {
        // connect with Input Field in component of same level
        inputField = searchField.GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(delegate { searchBook(inputField); });

        // book lists component loader
        bookListLoader = bookListScrollView.GetComponent<BookListLoader>();

        // get Camera controll
        cameraController = mainCamera.GetComponent<MainMainCamera>();
    }

    public void toggleSearchInput()
    {
        searchField.SetActive(!searchField.activeSelf);
        
        // remove search result when search input is inactive
        if(!searchField.activeSelf && !inputField.text.Equals(""))
        {
            bookListLoader.eraseAllBooks();
            bookListLoader.renderBooks(GameManager.getInstance().Books);
        }

        // when search field is active
        if(searchField.activeSelf) {
            inputField.Select();
        }
    }

    void searchBook(TMP_InputField input)
    {
        // check recent keyword
        if (prev_keyword == input.text) return;
        prev_keyword = input.text;

        // change camera to main book
        cameraController.MainMoveCamera(0);

        // rerender book
        bookListLoader.eraseAllBooks();

        if (input.text.Length > 0)
        {
            // search with book name
            List<Book> searched_books = new List<Book>();
            foreach (Book book in GameManager.getInstance().Books)
            {
                if (book.BookName.Contains(input.text))
                    searched_books.Add(book);
            }

            bookListLoader.renderBooks(searched_books);
        }
        else // keyword is none 
        {
            // show all
            bookListLoader.renderBooks(GameManager.getInstance().Books);
        }
    }
}
