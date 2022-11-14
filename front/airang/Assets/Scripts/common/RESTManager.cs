using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class BookSearchOption
{
    public BookSearchOption(int aFlag, int sort, string keyword, int pageNo)
    {
        this.aFlag = aFlag;
        this.sort = sort;
        this.keyword = keyword;
        this.pageNo = pageNo;
    }

    public int aFlag;
    public int sort;
    public string keyword;
    public int pageNo;
}

public class RESTManager : MonoBehaviour
{
    private const string basePath = "https://k7b305.p.ssafy.io/api/";

    static RESTManager instance = null;

    private RequestHelper requestOptions = new RequestHelper();

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
        requestOptions.ContentType = "application/json";
        requestOptions.EnableDebug = true;
    }

    private void initRequest(string URL, object data = null)
    {
        //Authorization
        requestOptions.Headers = new Dictionary<string, string> { { "access-token", PlayerPrefs.GetString("accessToken") ?? "" } };
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
        }).Then(res =>
        {
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
        }).Then(res =>
        {
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
        }).Then(res =>
        {
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
        }).Then(res =>
        {
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

    public RSG.IPromise<ResponseHelper> getNarr(int bookId, int pageNum)
    {
        var fileUrl = basePath + "book/narration";
        var fileType = AudioType.MPEG;

        RequestHelper requestHelper = new RequestHelper();
        requestHelper.Headers = new Dictionary<string, string> { { "id", bookId + "" }, { "page", pageNum + "" } };
        requestHelper.DownloadHandler = new DownloadHandlerAudioClip(fileUrl, fileType);
        requestHelper.Uri = fileUrl;

        return RestClient.Get(requestHelper);
    }
}
