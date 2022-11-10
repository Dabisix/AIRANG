using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using Models;

public class SignIn : MonoBehaviour
{
    public StartMainCamera my_camera;
    public DoorOpen doorOpen;
    public MainCharacterController mainCharacterController;

    public TMP_InputField idInputField;
    public TMP_InputField pwInputField;

    public GameObject SearchPWCanvas;
    public GameObject alertObject;

    public TextMeshProUGUI alertMsg;

    public void Login()
    {
        if(!IsEmail(idInputField.text))
        {
            alertMessage("이메일 형식이\n맞지 않습니다!");
            return;
        }

        RESTManager.getInstance().Post("user", new User.UserController
        {
            email = idInputField.text,
            pw = pwInputField.text
        }, false).Then(res =>
        {
            JObject json = JObject.Parse(res.Text); //string Json으로 변환

            if ((string)json["message"] == "success")
            {
                // login success
                PlayerPrefs.SetString("refreshToken", (string)json["token"]["refresh_TOKEN"]);
                PlayerPrefs.SetString("accessToken", (string)json["token"]["access_TOKEN"]);

                // get book list from server
                GameManager.getInstance().getAllBooksList();

                // find backdrop
                StartCoroutine(this.transform.Find("Image").GetComponent<UIMovementHandler>().LerpBackObject());

                // move to main Scene
                Invoke("openDoor",0.4f);
                //my_camera.StartMoveCamera(1);
                mainCharacterController.ChangePath();
            }
            else if ((string)json["message"] == "pwErr")
            {
                // PW not matched
                alertMessage("비밀번호가 \n 일치하지 않습니다.");
            }
            else
            {
                // ID not mathced
                alertMessage("존재하지 않는 \n 아이디입니다.");
            }
        }).Catch(err =>
        {
            Debug.Log(err.Message);
            alertMessage("네트워크 환경을 \n 확인해주세요");
        });
    }

    public void alertMessage(string message)
    {
        alertMsg.text = message;
        alertObject.gameObject.SetActive(true);
        Invoke("unenableAlert", 2.5f);
    }

    public void unenableAlert() {
        alertObject.gameObject.SetActive(false);
    }

    // 이메일 형식 검사
    private static bool IsEmail(string email)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([0-9a-zA-Z]+)@([0-9a-zA-Z]+)(\.[0-9a-zA-Z]+){1,}$");
        return regex.IsMatch(email);
    }

    // 비밀번호 찾기 씬으로 이동
    public void SearchPwBtn()
    {
        // unenalbe signup canvas and load login canvas
        StartCoroutine(this.transform.Find("Image").GetComponent<UIMovementHandler>().LerpBackObject());
        Invoke("loadSearchPWCanvas", 0.7f);
    }

    private void loadSearchPWCanvas()
    {
        // load login canvas with animation
        SearchPWCanvas.SetActive(true);
    }

    private void openDoor()
    {
        doorOpen.openDoor();
    }
}
