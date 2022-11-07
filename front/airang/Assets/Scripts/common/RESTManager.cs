using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Response
{
    private string response = null;
    private string error = null;

    public string Reponse
    {
        get => response;
        set => response = value;
    }

    public string Error
    {
        get => error;
        set => error = value;
    }
}

public class EMethod {
    public const string POST = "POST",
        GET = "POST",
        PUT = "POST",
        DELETE = "POST";
}

public class RESTManager : MonoBehaviour
{
    private const string basePath = "http://localhost:8081/api/";

    static RESTManager instance = null;

    private RequestHelper requestOptions;

    // singleton Pattern implemented
    public static RESTManager getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // if object is corupted
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        requestOptions = new RequestHelper();
        requestOptions.ContentType = "application/json";
        requestOptions.EnableDebug = true;
    }

    private void initRequest(string URL, object data = null)
    {
        //Authorization
        requestOptions.Headers = new Dictionary<string, string> {{ "access-token", PlayerPrefs.GetString("accessToken") ?? "" }};
        requestOptions.Uri = basePath + URL;
        requestOptions.Body = data;
    }

    private RSG.IPromise<ResponseHelper> requestNewAccessToken()
    {
        // set Options for only Refresh request
        RequestHelper requestOptionsForRefresh = new RequestHelper();
        requestOptionsForRefresh.ContentType = "application/json";
        requestOptionsForRefresh.Headers = new Dictionary<string, string> { { "refresh-token", PlayerPrefs.GetString("refreshToken") ?? "" } };
        requestOptionsForRefresh.Uri = basePath + "auth";

        Debug.Log("리프레쉬 토큰 재발급 시작");

        return RestClient.Get(requestOptionsForRefresh);
    }

    public RSG.IPromise<ResponseHelper> Get(string URL, bool needToken = true)
    {
        initRequest(URL);

        // for token not needed request
        if (!needToken)
            return RestClient.Get(requestOptions);

        return RestClient.Get(requestOptions).Then(res =>
        {
            // first request response
            if (res.Headers.ContainsKey("token_expired"))
                return requestNewAccessToken();
            else
                return RestClient.Get(requestOptions);
        }).Then(res => {
            if (res.Request.url.Contains("api/auth"))
            {
                // new Access Token gained
                PlayerPrefs.SetString("accessToken", res.Text);
                requestOptions.Headers = new Dictionary<string, string> { { "access-token", PlayerPrefs.GetString("accessToken") ?? "" } };
            }
            // rerequest 
            return RestClient.Get(requestOptions);
        });
    }

    public RSG.IPromise<ResponseHelper> Post(string URL, object data, bool needToken = true)
    {
        initRequest(URL, data);

        // for token not needed request
        if (!needToken)
            return RestClient.Post(requestOptions);

        return RestClient.Post(requestOptions).Then(res =>
        {
            // first request response
            if (res.Headers.ContainsKey("token_expired"))
                return requestNewAccessToken();
            else
                return RestClient.Post(requestOptions);
        }).Then(res => {
            if (res.Request.url.Contains("api/auth"))
            {
                // new Access Token gained
                PlayerPrefs.SetString("accessToken", res.Text);
                requestOptions.Headers = new Dictionary<string, string> { { "access-token", PlayerPrefs.GetString("accessToken") ?? "" } };
            }
            // rerequest 
            return RestClient.Post(requestOptions);
        });
    }

    public RSG.IPromise<ResponseHelper> Put(string URL, object data, bool needToken = true)
    {
        initRequest(URL, data);

        // for token not needed request
        if (!needToken)
            return RestClient.Put(requestOptions);

        return RestClient.Put(requestOptions).Then(res =>
        {
            // first request response
            if (res.Headers.ContainsKey("token_expired"))
                return requestNewAccessToken();
            else
                return RestClient.Put(requestOptions);
        }).Then(res => {
            if (res.Request.url.Contains("api/auth"))
            {
                // new Access Token gained
                PlayerPrefs.SetString("accessToken", res.Text);
                requestOptions.Headers = new Dictionary<string, string> { { "access-token", PlayerPrefs.GetString("accessToken") ?? "" } };
            }
            // rerequest 
            return RestClient.Put(requestOptions);
        });
    }

    public RSG.IPromise<ResponseHelper> Delete(string URL, bool needToken = true)
    {
        initRequest(URL);

        // for token not needed request
        if (!needToken)
            return RestClient.Delete(requestOptions);

        return RestClient.Delete(requestOptions).Then(res =>
        {
            // first request response
            if (res.Headers.ContainsKey("token_expired"))
                return requestNewAccessToken();
            else
                return RestClient.Delete(requestOptions);
        }).Then(res => {
            if (res.Request.url.Contains("api/auth"))
            {
                // new Access Token gained
                PlayerPrefs.SetString("accessToken", res.Text);
                requestOptions.Headers = new Dictionary<string, string> { { "access-token", PlayerPrefs.GetString("accessToken") ?? "" } };
            }
            // rerequest 
            return RestClient.Delete(requestOptions);
        });
    }

    public void DownloadFile()
    {
        // TODO
        var fileUrl = "";
        var fileType = AudioType.MPEG;

        RestClient.Get(new RequestHelper
        {
            Uri = fileUrl,
            DownloadHandler = new DownloadHandlerAudioClip(fileUrl, fileType)
        }).Then(res => {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = ((DownloadHandlerAudioClip)res.Request.downloadHandler).audioClip;
            audio.Play();
        }).Catch(err => {
            Debug.Log("Error " + err.Message);
        });
    }
}
