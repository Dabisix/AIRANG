using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCreator : MonoBehaviour
{
    public void loadContentPrefab()
    {
        m_Prefab = BookManager.getInstance().Content;
    }

    public void renderContent()
    {
        if(m_RendedObject != null)
            Destroy(m_RendedObject);
        m_RendedObject = Instantiate(m_Prefab, new Vector3(0, 0), new Quaternion(0, 0, 0, 0));
    }

    private void Start()
    {
        loadContentPrefab();
        renderContent();
    }

    GameObject m_Prefab;
    GameObject m_RendedObject;
}
