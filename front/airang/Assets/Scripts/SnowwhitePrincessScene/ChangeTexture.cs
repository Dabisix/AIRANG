using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    public Texture gameDataTex;
    public Material gameDataMat;


    public void ChangeShaderTexture()
    {
        gameDataMat.SetTexture("_MainTex", gameDataTex);
    }
}
