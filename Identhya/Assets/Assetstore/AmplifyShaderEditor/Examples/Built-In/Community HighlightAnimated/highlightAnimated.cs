using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFHC_Shader_Samples
{

	public class highlightAnimated : MonoBehaviour
    {

        public Material mat;

        void Start()
        {
            mat = GetComponent<Renderer>().material;
        }

        void OnMouseEnter()
        {
            switchhighlighted(true);
		}

        public void OnMouseExit()
        {
            switchhighlighted(false);
        }

        public void switchhighlighted(bool highlighted)
        {
            mat.SetFloat("_Highlighted", (highlighted ? 1.0f : 0.0f));
        }

    }

}