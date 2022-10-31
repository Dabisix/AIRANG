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

    private void Awake()
    {
        StartCoroutine(GetAccessToken());
    }

    // access token 
    IEnumerator GetAccessToken()
    {
        if (PlayerPrefs.HasKey("accessToken"))
        {
            // 일단 로그인할때 access token PlayerPrefs 에 저장했으니 있긴 있을것
            Debug.Log("access token : " + PlayerPrefs.GetString("accessToken"));
            accessToken = PlayerPrefs.GetString("accessToken");

            // 저장해놓은 access token이 유효한지 확인하자~
            using (UnityWebRequest request = UnityWebRequest.Get(URL + "user"))
            {
                request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("access-token", accessToken); //accessToken 헤더에 담기
                yield return request.SendWebRequest();

                // access token 이 유효하면
                if (!request.isHttpError)
                {
                    JObject json = JObject.Parse(request.downloadHandler.text); //string Json���� ��ȯ
                    Debug.Log(json["name"]);
                    nameInputField.text = (string)json["name"]; //�̸� input field�� �ֱ�
                }
                else
                {
                    // access token 유효하지 않음, refresh token 을 가져오장
                    StartCoroutine(GetRefreshToken());
                }
            }
        }

    }

    IEnumerator GetRefreshToken()
    {
        if (PlayerPrefs.HasKey("refreshToken"))
        {
            Debug.Log("refresh token : " + PlayerPrefs.GetString("refreshToken"));
            refreshToken = PlayerPrefs.GetString("refreshToken");

            using (UnityWebRequest request = UnityWebRequest.Get(URL + "auth"))
            {
                request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("refresh-token", refreshToken); //refreshToken 헤더에 담기

                yield return request.SendWebRequest();
                Debug.Log(request.downloadHandler.text);

                // refresh token�� ��ȿ��
                if (request.downloadHandler.text != null)
                {
                    PlayerPrefs.SetString("accessToken", request.downloadHandler.text); // accesstoken 다시 저장
                    StartCoroutine(GetAccessToken()); // 새로 발급받은 access token으로 회원정보 가져오기
                }
                else
                {
                    // refresh token 도 만료됨 다시 로그인해~~
                }

            }

        }
    }

    public void ModifyBtn()
    {
        // 비밀번호가 일치할 경우에만
        if(pwInputField.text == checkPwInputField.text)
        {
            StartCoroutine(ModifyBtnCo());
        }
        else
        {
            alertMsg.text = "비밀번호가 일치하지 않습니다!";
            alertPanel.gameObject.SetActive(true);
        }
    }

    IEnumerator ModifyBtnCo() {

        User.UserController user = new User.UserController
        {
            name = nameInputField.text, pw = pwInputField.text
        };

        string jsonData = JsonUtility.ToJson(user);
        using (UnityWebRequest request = UnityWebRequest.Put(URL + "user", jsonData)) { 
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("access-token", accessToken); //accessToken 헤더에 담기

            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
            Debug.Log(request.isHttpError);
            if (!request.isHttpError)
            {
                Debug.Log("회원 정보 수정 성공");
                alertPanel.gameObject.SetActive(false);
            }
            else
            {
                //정보 수정 실패할 일이 있을까???
            }
        }
    }




}
