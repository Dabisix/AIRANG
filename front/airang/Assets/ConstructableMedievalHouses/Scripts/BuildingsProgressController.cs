using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CybeRockStudio
{
    public class BuildingsProgressController : MonoBehaviour
    {
        /// <summary>
        /// Indicates The Progress Of The Construction
        /// </summary>
        [Range(0, 100)]
        public float Progress;

        /// <summary>
        /// Gets The Starting DefaultValue Of The Material Parameter
        /// </summary>
        float defaultValue;

        /// <summary>
        /// With This  You Can Control The Speed Of The Construction Animation
        /// </summary>
        public float constructionSpeed;

        /// <summary>
        /// Indicates If The Building Is Spawned To Kickstart The Construction Animation
        /// </summary>
        [HideInInspector]
        public bool isPlaced;

        /// <summary>
        /// The Mesh Renderer Component Of Each Building To Get The Material From
        /// </summary>
        Renderer rend;

        private void Awake()
        {
            /// Get The MeshRenderer Component
            rend = GetComponentInChildren<Renderer>();

            /// Get The Shader
            rend.sharedMaterial.shader = Shader.Find("BuildingEffect");
        }

        private void Start()
        {
            /// Get The MeshRenderer Component At Start
            rend = GetComponentInChildren<Renderer>();
            /// Get The Shader At Start
            rend.sharedMaterial.shader = Shader.Find("BuildingEffect");
            /// Get The Shader Parameter At Start
            defaultValue = rend.material.GetFloat("_Progress");
        }

        private void Update()
        {
            AnimateTheConstruction();
        }

        public void AnimateTheConstruction()
        {
            /// Checks If Its Spawned & Then Starts The Animation

            if (isPlaced)
            {
                if (Progress < 100)
                {
                    Progress += constructionSpeed * Time.deltaTime;
                }
                else
                {
                    Progress = 100;
                }
            }

            rend.material.SetFloat("_Progress", defaultValue - Progress);
        }
    }
}