using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class NewBehaviourScript : MonoBehaviour
{
    //public string key;


    //AsyncOperationHandle<GameObject> opHandle;

    //public IEnumerator Start()
    //{
    //    key = "Book2/Page01";
    //    opHandle = Addressables.LoadAssetAsync<GameObject>(key);
    //    yield return opHandle;

    //    if (opHandle.Status == AsyncOperationStatus.Succeeded)
    //    {
    //        GameObject obj = opHandle.Result;
    //        Instantiate(obj, transform);
    //    }
    //}

    //void OnDestroy()
    //{
    //    Addressables.Release(opHandle);
    //}
    // Label strings to load
    private List<string> keys = new List<string>() { "Book2/Page02" , "Book2/Page01"};

    // Operation handle used to load and release assets
    AsyncOperationHandle<IList<GameObject>> loadHandle;
    private List<GameObject> contents = new List<GameObject>();

    // Load Addressables by Label
    public IEnumerator Start()
    {
        float x = 0, z = 0;
        loadHandle = Addressables.LoadAssetsAsync<GameObject>(
            keys,
            addressable =>
            {
                Debug.Log(addressable);
                contents.Add(addressable);
                //Gets called for every loaded asset
                //Instantiate<GameObject>(addressable,
                //    new Vector3(0, 0, 0),
                //    Quaternion.identity,
                //    transform);
                playSnowPrincess();
            }, Addressables.MergeMode.Union, // How to combine multiple labels 
            false); // Whether to fail and release if any asset fails to load
        yield return loadHandle;
    }


    public void playSnowPrincess()
	{
        Instantiate(contents[0], new Vector3(0, 0), new Quaternion(0, 0, 0, 0));
    }
   

    private void OnDestroy()
    {
        Addressables.Release(loadHandle);
        // Release all the loaded assets associated with loadHandle
        // Note that if you do not make loaded addressables a child of this object,
        // then you will need to devise another way of releasing the handle when
        // all the individual addressables are destroyed.
    }
}
