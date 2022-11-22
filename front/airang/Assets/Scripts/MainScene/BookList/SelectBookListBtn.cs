using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectBookListBtn : MonoBehaviour
{
    public GameObject[] BookListCanvas;
    public ActiveIdx activeId;

	public void SelectedBtn()
    {
		switch (gameObject.name)
		{
            case "AllBookListBtn":
                ActiveBookCanvas(0);
                break;
            case "BestBookListBtn":
                ActiveBookCanvas(1);
                break;
            case "ARBookListBtn":
                ActiveBookCanvas(2);
                break;
            case "RecentBookListBtn":
                ActiveBookCanvas(0);
                break;
            case "FavorBookListBtn":
                ActiveBookCanvas(1);
                break;
            case "RelatedBookListBtn-1":
                ActiveBookCanvas(3);
                break;
            case "RelatedBookListBtn-2":
                ActiveBookCanvas(4);
                break;
        }
    }

    void ActiveBookCanvas(int selectedIdx)
	{
        if (activeId.GetActiveId() != selectedIdx)
        {
            BookListCanvas[activeId.GetActiveId()].SetActive(false);
        }
        BookListCanvas[selectedIdx].SetActive(true);
        activeId.SetActiveId(selectedIdx);
    }

}
