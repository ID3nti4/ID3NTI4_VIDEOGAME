using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEndController : MonoBehaviour
{
    public UIFader uiFader1;
    public UIFader uiFader2;
    public UIFader uiFader3;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);
        uiFader1.FadeToTransparent();
        yield return new WaitForSeconds(2.0f);
        uiFader2.FadeToTransparent();
        yield return new WaitForSeconds(3.0f);
        uiFader3.FadeToTransparent();
        yield return new WaitForSeconds(1.0f);
        while (1<2)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
