using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class TitlesController : MonoBehaviour
{
    public GameObject title;
    public GameObject start;
    public GameObject background;

    public AudioClip track;
    public VideoPlayer videoPlayer;

    bool showingTitle = false;

    public float IntroDuration = 25.0f;
    IEnumerator Start()
    {
        //PlayerPrefs.SetInt(PlayOnPlayerPref.Key, 0);
        title.SetActive(false);
        start.SetActive(false);
        background.SetActive(false);
        yield return new WaitForSeconds(IntroDuration);
        videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);
        ShowStuff();
        
    }

    private void ShowStuff()
    {
        title.SetActive(true);
        start.SetActive(true);
        background.SetActive(true);
        FindObjectOfType<MusicController>().Play(track, true);
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            if (!showingTitle)
            {
                videoPlayer.Stop();
                videoPlayer.gameObject.SetActive(false);
                ShowStuff();
                showingTitle = true;
            }
            else
            {
                start.SetActive(false);
                LevelLoader levelLoader = GetComponent<LevelLoader>();
                levelLoader.LaunchGame();
            }
        }
    }
}
