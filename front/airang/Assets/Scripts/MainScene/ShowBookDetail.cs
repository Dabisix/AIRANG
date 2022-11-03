using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShowBookDetail : MonoBehaviour
    ,IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.getInstance().setCurrentBookWithName(this.gameObject.name))
            SceneManager.LoadScene("BookSettingScene");
        else
            Debug.Log("book not found");
    }

}
