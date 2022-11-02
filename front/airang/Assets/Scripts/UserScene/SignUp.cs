using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SignUp : MonoBehaviour
{
    private const string URL = "http://localhost:8081/api/";

    public TMP_InputField nameInput;
    public TMP_InputField idInput;
    public TMP_InputField pwInput;

    public GameObject signupObject;
    public GameObject confirmObject;
    public GameObject alertObject;
    public GameObject signinCanvas;

    public TextMeshProUGUI confirmMsg;
    public TextMeshProUGUI alertMsg;


    private bool isConfirmed; // 중복확인 했는지 상태
    private void Awake()
    {    
        isConfirmed = false;
    }

    // 중복확인 버튼 눌렀을때
    public void ConfirmBtn()
    {
        if(alertObject.gameObject.activeSelf == true) // 그전에 경고창 떠있으면 누른순간 없애기
        {
            alertObject.gameObject.SetActive(false);
        }

        if (IsEmail(idInput.text)) //이메일 형식 맞을때
        {
            StartCoroutine(ConfirmEmailCo());
        }
        else
        {
            alertMsg.text = "이메일 형식이\n맞지 않습니다!";
            alertObject.gameObject.SetActive(true);
            Invoke("AlertBubbleTime", 1.25f);
        }
    }

    // 이메일 중복검사
    IEnumerator ConfirmEmailCo() {
        Debug.Log(idInput.text);
        User.UserController user = new User.UserController { email = idInput.text };

        string jsonData = JsonUtility.ToJson(user); // 데이터 json으로 바꾸고

        using (UnityWebRequest request = UnityWebRequest.Post(URL + "user/id", jsonData))
        {

            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
            // 사용가능한 이메일
            if (request.downloadHandler.text == "success")
            {
                isConfirmed = true; // 중복확인 함
                Debug.Log(request.downloadHandler.text);
                alertMsg.text = "사용 가능한 이메일입니다.\n 회원가입을 진행해주세요!";
                alertObject.gameObject.SetActive(true);
                Invoke("AlertBubbleTime", 1.25f);
            }
            else
            {
                // 중복 이메일
                Debug.Log(request.error);
                alertMsg.text = "중복된 이메일 입니다.\n 다른 이메일을 입력해주세요!";
                alertObject.gameObject.SetActive(true);
                Invoke("AlertBubbleTime", 1.25f);
            }
        }

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
        Debug.Log("가입하기 버튼 누름");
        Debug.Log(nameInput.text);
        if (nameInput.text == "")
        {
            alertMsg.text = "이름을 입력해주세요.";
            alertObject.gameObject.SetActive(true);

            Invoke("AlertBubbleTime", 1.25f);
        }
        else if (idInput.text == "")
        {
            alertMsg.text = "이메일을 입력해주세요.";
            alertObject.gameObject.SetActive(true);

            Invoke("AlertBubbleTime", 1.25f);

        }
        else if (!IsEmail(idInput.text))
        {
            alertMsg.text = "이메일 형식이 맞지 않습니다.";
            alertObject.gameObject.SetActive(true);

            Invoke("AlertBubbleTime", 1.25f);

        }
        else if (!isConfirmed)
        {
            alertMsg.text = "이메일 중복확인을 해주세요.";
            alertObject.gameObject.SetActive(true);

            Invoke("AlertBubbleTime", 1.25f);
        }
        else if (pwInput.text== "")
        {
            pwInput.text = "비밀번호를 입력해주세요.";
            alertObject.gameObject.SetActive(true);

            Invoke("AlertBubbleTime", 1.25f);

        }
        else
        {
            StartCoroutine(SignUpCo());
        }
    }

    IEnumerator SignUpCo()
    {
        // 입력값 넣어서 User 객체 새로 만들기
        User.UserController user = new User.UserController
        {
            email = idInput.text,
            name = nameInput.text,
            pw = pwInput.text
        };

        //json 형식으로 바꾸기
        string jsonData = JsonUtility.ToJson(user);
        Debug.Log(jsonData);

        UnityWebRequest request = UnityWebRequest.Post(URL + "user/signup", jsonData);
        
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        if (request.error == null)
        {
            Debug.Log(request.downloadHandler.text);
            alertMsg.text = "회원가입이 완료되었습니다 😊";
            alertObject.gameObject.SetActive(true);

            Invoke("changeCanvas", 1.25f);
            request.Dispose();

        }
        else
        {
            Debug.Log(request.error);
            request.Dispose();
        }

    }

    /*private void ChangeScene()
    {
        SceneManager.LoadScene("SignInScene");
    }*/

    private void changeCanvas()
	{
        gameObject.SetActive(false);
        signinCanvas.SetActive(true);
    }

    private void AlertBubbleTime()
    {
        alertObject.gameObject.SetActive(false);
    }
}
