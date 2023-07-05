using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UILevel : MonoBehaviour
{

    public string textValue;  
    public GameObject textElement; 
    public TMP_Text LevelTextMeshPro; 

    // Start is called before the first frame update
    void Start()
    {
        LevelTextMeshPro = textElement.GetComponent<TMP_Text>();


    }

    // Update is called once per frame
    void Update()
    {
        textValue = LevelSystem.currentLevel.ToString();

        LevelTextMeshPro.text = textValue; 
    }
}
