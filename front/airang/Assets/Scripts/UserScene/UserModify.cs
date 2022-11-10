using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json.Linq;

public class UserModify : MonoBehaviour
{
    private const string URL = "http://localhost:8081/api/";
    private string accessToken;
    private string refreshToken;

    public TMP_InputField nameInputField;
    public TMP_InputField pwInputField;
    public TMP_InputField checkPwInputField;

    public GameObject alertPanel;
    public TextMeshProUGUI alertMsg;

    private void Start()
    {
        RESTManager.getInstance().Get("/user")  
            .Then(res =>
            {
                JObject userInfo = JObject.Parse(res.Text);
                nameInputField.text = (string)userInfo["name"];
            }).Catch(err =>
            {
                alertMessage("네트워크 환경을 확인해주세요");
            });
    }

    public void ModifyBtn()
    {
        // 비밀번호가 일치할 경우에만
        if(pwInputField.text == checkPwInputField.text)
        {
            RESTManager.getInstance().Put("user", new User.UserController
            {
                name = nameInputField.text,
                pw = pwInputField.text
            }).Then(res => {
                alertMessage("회원 정보 수정 성공");
                Invoke("inactiveUserModify", 2f);
            }).Catch(err =>
            {
                alertMessage("네트워크 환경을 확인해주세요");
            });
        }
        else {
            alertMessage("두 비밀번호가 일치하지 않습니다");
        }
    }

    private void inactiveUserModify()
    {
        StartCoroutine(this.transform.Find("Image").GetComponent<UIMovementHandler>().LerpBackObject());
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
