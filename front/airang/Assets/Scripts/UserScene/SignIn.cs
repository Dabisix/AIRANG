using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class SignIn : MonoBehaviour
{
    public StartMainCamera my_camera;
    public TMP_InputField idInputField;
    public TMP_InputField pwInputField;
    public DoorOpen doorOpen;
    public GameObject alertObject;
    public TextMeshProUGUI alertMsg;

    private const string URL = "http://localhost:8081/api/";

    public void SignInBtn()
    {
        StartCoroutine(SignInCo());
    }

    IEnumerator SignInCo()
    {
        // 이메일 형식 맞으면
        if (IsEmail(idInputField.text))
        {
            // 입력값 넣어서 User 객체 새로 만들기
            User.UserController user = new User.UserController
            {
                email = idInputField.text,
                pw = pwInputField.text
            };

            //json 형식으로 바꾸기
            string jsonData = JsonUtility.ToJson(user);

            using (UnityWebRequest request = UnityWebRequest.Post(URL + "user", jsonData))
            {
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();
                JObject json = JObject.Parse(request.downloadHandler.text); //string Json으로 변환
                Debug.Log(json["message"]);
                if ((string)json["message"] == "success")
                {
                    Debug.Log("액세스토큰 : " + (string)json["token"]["access_TOKEN"]);
                    Debug.Log("리프레쉬토큰 : "+ (string)json["token"]["refresh_TOKEN"]);

                    // 데이터 저장
                    PlayerPrefs.SetString("refreshToken", (string)json["token"]["refresh_TOKEN"]);
                    PlayerPrefs.SetString("accessToken", (string)json["token"]["access_TOKEN"]);

                    alertMsg.text = "환영합니다.";
                    //Invoke("ChangeScene", 1.25f); //메인씬으로 이동시켜야함
                    Invoke("openDoor", 2.7f);
                    my_camera.StartMoveCamera(1);
                    gameObject.SetActive(false);
                }
                else if((string)json["message"] == "pwErr")
                {
                    alertMsg.text = "비밀번호가 \n 일치하지 않습니다.";
                }
                else
                {
                    alertMsg.text = "아이디가 \n 일치하지 않습니다.";
                }
                alertObject.gameObject.SetActive(true);

            }
        }
        else
        {
            alertMsg.text = "이메일 형식이\n맞지 않습니다!";
            alertObject.gameObject.SetActive(true);
        }

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
        SceneManager.LoadScene("SearchPwScene");
    }


    private void openDoor()
    {
        doorOpen.openDoor();
    }
}
