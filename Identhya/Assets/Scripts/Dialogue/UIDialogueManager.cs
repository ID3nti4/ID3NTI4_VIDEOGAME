using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueManager : MonoBehaviour
{
    public Text DialogueLine;

    public RectTransform ButtonX;
    public RectTransform ButtonY;
    public RectTransform ButtonB;
    bool ShowX, ShowY, ShowB;

    public Text AskX;
    public Text AskY;
    public Text AskB;

    public Color color = Color.white;

    float CurrentOpacity = 0.0f;
    float CurrentScale = 0.0f;
    public float OpacitySpeed = 4.0f;

    private void Start()
    {
        DialogueLine.text = "";
        AskX.text = AskY.text = AskB.text = "";
        ShowX = ShowY = ShowB = true;
        ScaleImmediately(0.0f);
        FadeToOpacityImmediately(0.0f);
        ShowX = ShowY = ShowB = false;
    }

    public void ScaleImmediately(float scale)
    {
        CurrentScale = scale;
        if(ShowX)
            ButtonX.localScale = scale * Vector3.one;
        if (ShowY)
            ButtonY.localScale = scale * Vector3.one;
        if (ShowB)
            ButtonB.localScale = scale * Vector3.one;
    }

    public void FadeToOpacityImmediately(float op)
    {
        DialogueLine.color = new Color(color.r, color.g, color.b, op);
        CurrentOpacity = op;
    }

    public void ShowLine(string line)
    {
        DialogueLine.text = line;
    }

    public void ShowAsks(string ask1, string ask2)
    {
        ShowX = true;
        ShowY = false;
        ShowB = true;

        AskX.text = ask1;
        AskY.text = "";
        AskB.text = ask2;

        ScaleButtonsIn();
    }

    public void ShowAsks(string ask1, string ask2, string ask3)
    {
        ShowX = true;
        ShowY = true;
        ShowB = true;

        AskX.text = ask1;
        AskY.text = ask3;
        AskB.text = ask2;

        ScaleButtonsIn();
    }

    public void HideAsks()
    {
        ScaleButtonsOut();
        AskX.text = AskY.text = AskB.text = "";
    }

    public Coroutine ScaleButtonsIn()
    {
        return StartCoroutine(Scale(1.0f));
    }

    public Coroutine ScaleButtonsOut()
    {
        return StartCoroutine(Scale(0.0f));
    }

    public Coroutine FadeToOpaque()
    {
        return StartCoroutine(FadeToOpacity(1.0f));
    }

    public Coroutine FadeToTransparent()
    {
        return StartCoroutine(FadeToOpacity(0.0f));
    }

    // Note: UIDialogueManager should not control scale or opacity; add scripts to text and buttons to take care of this (TextFader, Scaler, etc...)
    IEnumerator FadeToOpacity(float op)
    {
        while (CurrentOpacity != op)
        {
            if (CurrentOpacity < op)
            {
                CurrentOpacity = Mathf.Min(CurrentOpacity + OpacitySpeed * Time.deltaTime, op);
            }
            else
            {
                CurrentOpacity = Mathf.Max(CurrentOpacity - OpacitySpeed * Time.deltaTime, op);
            }

            DialogueLine.color = new Color(color.r, color.g, color.b, CurrentOpacity);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Scale(float scale)
    {
        while (CurrentScale != scale)
        {
            if (CurrentScale < scale)
            {
                CurrentScale = Mathf.Min(CurrentScale + OpacitySpeed * Time.deltaTime, scale);
            }
            else
            {
                CurrentScale = Mathf.Max(CurrentScale - OpacitySpeed * Time.deltaTime, scale);
            }

            if(ShowX) ButtonX.localScale = CurrentScale * Vector3.one;
            if (ShowY) ButtonY.localScale = CurrentScale * Vector3.one;
            if (ShowB) ButtonB.localScale = CurrentScale * Vector3.one;



            yield return new WaitForEndOfFrame();
        }
    }
}
