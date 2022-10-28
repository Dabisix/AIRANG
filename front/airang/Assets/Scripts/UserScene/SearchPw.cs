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

    private const string URL = "http://localhost:8081/api/";

    public void SendPwBtn()
    {
        alertMsg.text = "������ Ȯ���ϴ� �� �Դϴ�.";
        alertPanel.gameObject.SetActive(true);
        StartCoroutine(SendPwBtnCo());
    }

    IEnumerator SendPwBtnCo()
    {
        User.UserController user = new User.UserController { email = emailInput.text };
        string jsonData = JsonUtility.ToJson(user);
        UnityWebRequest request = UnityWebRequest.Post(URL + "user/mail", jsonData);

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        if(request.downloadHandler.text == "���� ���� ����")
        {
            alertMsg.text = "�ӽ� ��й�ȣ�� �߱޵Ǿ����ϴ�! \n ������ Ȯ�����ּ���.";

        }
        else
        {
            alertMsg.text = "���Ե��� ���� �̸����Դϴ� \n ȸ�������� ���ּ���.";

        }
        request.Dispose();
    }

}
