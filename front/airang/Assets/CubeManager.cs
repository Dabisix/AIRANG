using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace book
{
    public class CubeManager : MonoBehaviour
    {
        public ARRaycastManager aRRaycastManager;
        public GameObject cubePrefab;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void CreateCube(Vector3 position)
        {
            Instantiate(cubePrefab, position, Quaternion.identity);
        }

        private void DeleteCube(GameObject cubeObject)
        {
            Destroy(cubeObject);
        }
    }

}
