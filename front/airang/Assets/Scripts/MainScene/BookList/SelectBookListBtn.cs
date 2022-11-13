using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
