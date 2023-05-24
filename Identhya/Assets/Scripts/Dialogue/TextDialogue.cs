using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDialogue : MonoBehaviour
{
    //public string[] Lines;
    public DialogueItem[] items;
    public TextDialogue next_N;

    int CurrentLine = 0;
    int CurrentItem = 0;

    public void Reset()
    {
        CurrentLine = CurrentItem = 0;    
    }

    public DialogueItem GetNextItem()
    {
        if (CurrentItem < items.Length)
        {
            return items[CurrentItem++];
        }
        else return null;
    }
    /*public string GetNextLine()
    {
        if (CurrentLine < Lines.Length)
        {
            Debug.Log("Returning line of text: " + Lines[CurrentLine]);
            return Lines[CurrentLine++];
        }
        else return "";
    }*/
}
