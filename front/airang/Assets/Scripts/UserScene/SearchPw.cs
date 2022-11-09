using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SearchPw : MonoBehaviour
{
    public TMP_InputField emailInput;

    public GameObject alertPanel;
    public TextMeshProUGUI alertMsg;

    public GameObject loginCanvas;

    public void SendPwBtn()
    {
        alertMessage("메일을 확인하는 중입니다.");

        RESTManager.getInstance().Post("user/mail", new User.UserController { email = emailInput.text }, false)
            .Then(res =>
            {
                alertMessage("임시 비밀번호가 \n 발급되었습니다! \n 메일을 확인해주세요.");
                Invoke("ChangeLoginCanvas", 1.5f);
            }).Catch(err =>
            {
                if(err.Message.Contains("404"))
                    alertMessage("존재하지 않는 이메일입니다.");
                else
                    alertMessage("네트워크 환경을 \n 확인해주세요.");
            });
    }

    private void ChangeLoginCanvas()
	{
        StartCoroutine(this.transform.Find("Image").GetComponent<UIMovementHandler>().LerpBackObject());
        Invoke("loadLoginCanvas", 0.7f);
    }

    private void loadLoginCanvas()
    {
        // load login canvas with animation
        loginCanvas.SetActive(true);
    }

    public void alertMessage(string message)
    {
        // show alert message and hide
        alertMsg.text = message;
        alertPanel.gameObject.SetActive(true);
        Invoke("unenableAlert", 2.5f);
    }
    public void unenableAlert()
    {
        alertPanel.gameObject.SetActive(false);
    }
}
