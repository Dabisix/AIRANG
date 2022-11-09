using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class SignUp : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField idInput;
    public TMP_InputField pwInput;

    public GameObject signupObject;
    public GameObject confirmObject;
    public GameObject alertObject;
    public GameObject signinCanvas;

    public TextMeshProUGUI confirmMsg;
    public TextMeshProUGUI alertMsg;

    public GameObject loginCanvas;


    private bool isConfirmed; // 중복확인 했는지 상태
    private void Awake()
    {    
        isConfirmed = false;
    }

    // 중복확인 버튼 눌렀을때
    public void ConfirmBtn()
    {
        // 그전에 경고창 떠있으면 누른순간 없애기 
        if (alertObject.gameObject.activeSelf == true) {
            alertObject.gameObject.SetActive(false);
        }

        if (!IsEmail(idInput.text)) {
            alertMessage("이메일 형식이\n맞지 않습니다!");
            return;
        }

        // 이메일 중복검사
        RESTManager.getInstance().Post("user/id", new User.UserController { email = idInput.text }, false)
            .Then(res =>
            {
                if (res.Text == "success") {
                    isConfirmed = true;
                    alertMessage("사용 가능한 이메일입니다.\n 회원가입을 진행해주세요!");
                } else {
                    alertMessage("중복된 이메일 입니다.\n 다른 이메일을 입력해주세요!");
                }
            }).Catch(err =>
            {
                Debug.Log(err.Message);
                alertMessage("네트워크 환경을 \n 확인해주세요");
            });
    }

    // 이메일 형식 검사
    private static bool IsEmail(string email)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([0-9a-zA-Z]+)@([0-9a-zA-Z]+)(\.[0-9a-zA-Z]+){1,}$");
        return regex.IsMatch(email);
    }

    // 가입하기 버튼 눌렀을때 실행될 함수
    public void SignUpBtn()
    {
        if (nameInput.text == "")
            alertMessage("이름을 입력해주세요.");
        else if (idInput.text == "")
            alertMessage("이메일을 입력해주세요.");
        else if (!IsEmail(idInput.text))
            alertMessage("이메일 형식이 맞지 않습니다.");
        else if (!isConfirmed)
            alertMessage("이메일 중복확인을 해주세요.");
        else if (pwInput.text == "")
            alertMessage("비밀번호를 입력해주세요.");
        else
            signUp();
    }

    public void signUp()
    {
        RESTManager.getInstance().Post("user/signup", new User.UserController
        {
            email = idInput.text,
            name = nameInput.text,
            pw = pwInput.text
        }, false).Then(res =>
        {
            alertMessage("회원가입이 \n 완료되었습니다 😊");
            Invoke("changeCanvas", 1.25f);
        }).Catch(err =>
        {
            Debug.Log(err.Message);
            alertMessage("네트워크 환경을 \n 확인해주세요.");
        });
    }

    public void alertMessage(string message)
    {
        // show alert message and hide
        alertMsg.text = message;
        alertObject.gameObject.SetActive(true);
        Invoke("unenableAlert", 2.5f);
    }
    public void unenableAlert()
    {
        alertObject.gameObject.SetActive(false);
    }

    private void changeCanvas()
	{
        // unenalbe signup canvas and load login canvas
        StartCoroutine(this.transform.Find("Image").GetComponent<UIMovementHandler>().LerpBackObject());
        Invoke("loadLoginCanvas", 0.7f);
    }

    private void loadLoginCanvas()
    {
        // load login canvas with animation
        loginCanvas.SetActive(true);
    }
}
