using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CybeRockStudio
{
    public class BuildingsPlacement : MonoBehaviour
    {
        /// <summary>
        /// An Array That Holds The Objects That Can Be Placed
        /// </summary>
        [SerializeField]
        private GameObject[] placeableObjectPrefabs;


        /// <summary>
        /// Current Selected Object Fom The Array To Be Spawned
        /// </summary>
        private GameObject currentPlaceableObject;


        /// <summary>
        /// The Ground LayerMask
        /// </summary>
        public LayerMask terrainLayer;

        /// <summary>
        /// Rotate The Spawned Object To The Desirted Orientation
        /// With The Mouse Wheel Before Placing It
        /// </summary>
        private float mouseWheelRotation;
        /// <summary>
        /// The Current Selected Prefab Index In The Array
        /// </summary>
        private int currentPrefabIndex = -1;

        /// <summary>
        /// Current GameObject Index
        /// </summary>

        public int objectToPlaceIndex = -1;

        /// <summary>
        /// Check To See Whether The Placement Process Is Taking Place
        /// </summary>
        public bool enablePlacement;

        private void Update()
        {
            HandleNewBuildingHotkey();

            if (currentPlaceableObject != null)
            {
                MoveCurrentBuildingByMouse();
                RotateBuildingByMouseWheel();
                ReleaseTheBuildingIfClicked();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                enablePlacement = false;
            }
        }

        /// <summary>
        /// Selects The Object To Spawn From The Prefab Array
        /// </summary>
        private void HandleNewBuildingHotkey()
        {
            if (enablePlacement)
            {
                for (int i = 0; i < placeableObjectPrefabs.Length; i++)
                {
                    if (PressedKeyOfCurrentPrefab(i))
                    {
                        Destroy(placeableObjectPrefabs[i]);
                        currentPrefabIndex = -1;
                    }
                    else
                    {
                        if (currentPlaceableObject != null)
                        {
                            Destroy(currentPlaceableObject);
                        }

                        currentPlaceableObject = Instantiate(placeableObjectPrefabs[objectToPlaceIndex]);
                        currentPrefabIndex = objectToPlaceIndex;
                        enablePlacement = false;
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// Sets The Prefab Index To The "index" Set In The Method's Argument
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>

        private bool PressedKeyOfCurrentPrefab(int index)
        {
            return currentPlaceableObject != null && currentPrefabIndex == index;
        }


        /// <summary>
        /// Moves The Instantiated Object With The Mouse Curor
        /// </summary>
        private void MoveCurrentBuildingByMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 1000, terrainLayer))
            {
                currentPlaceableObject.transform.position = hitInfo.point;
                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
        }

        /// <summary>
        /// Rotates The Instantiated Object With The Mouse Scroll Wheel
        /// </summary>

        private void RotateBuildingByMouseWheel()
        {
            Debug.Log(Input.mouseScrollDelta);
            mouseWheelRotation += Input.mouseScrollDelta.y;
            currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        }

        /// <summary>
        /// Spawns The Building In The Final Position Of The Mouse Cursor
        /// </summary>
        private void ReleaseTheBuildingIfClicked()
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentPlaceableObject.GetComponent<BuildingsProgressController>().isPlaced = true;
                currentPlaceableObject.GetComponent<BuildingsProgressController>().Progress = 0;
                currentPlaceableObject = null;
            }
        }

        public void SelectPlacementPrefab(int index)
        {
            objectToPlaceIndex = index;
        }

        /// <summary>
        /// Controlls Activation Of The Building Spawning
        /// </summary>
        public void EnableSpawning()
        {
            if (!enablePlacement)
            {
                enablePlacement = true;
            }
            else
            {
                enablePlacement = false;
            }
        }
    }
}