using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VideoPlayer))]
public class LogoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public UIFader fader;
    public GameObject LogoObject;
    public string NextScene = "Intro";

    IEnumerator Start()
    {
        Checkpoint.ClearCheckpoint();
        fader.FadeToOpacityImmediately(1.0f);
        LogoObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        yield return fader.FadeToTransparent();
        yield return new WaitForSeconds(3.0f);
        yield return fader.FadeToOpaque();
        yield return new WaitForSeconds(0.25f);
        LogoObject.SetActive(false);
        fader.FadeToTransparent();
        videoPlayer.Play();
        while(!videoPlayer.isPlaying)
        {
            yield return new WaitForSeconds(0.25f);
        }
        while(videoPlayer.isPlaying)
        {
            yield return new WaitForSeconds(0.25f);
        }

        SceneManager.LoadScene(NextScene);

    }
}
