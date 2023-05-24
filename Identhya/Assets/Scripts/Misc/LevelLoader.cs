using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public string LevelName = "I2New";
    public GameObject LoaderContent;
    public Image LoaderFill;
    public Text LoaderText;

    public bool isLaunched;
    string initialText;
    int loadingPoint;
    float timer;
    bool isLoading = true;

    void Start()
    {
        initialText = LoaderText.text;

        LoaderContent.SetActive(false);

        if (isLaunched)
        {
            StartCoroutine(LoadAsynchronously());
        }        
    }

    void Update() {
        if (isLoading)
        {
            if (timer <= 0)
            {
                if (loadingPoint < 3)
                {
                    LoaderText.text = LoaderText.text + ".";
                    ++loadingPoint;
                }
                else
                {
                    LoaderText.text = initialText;
                    loadingPoint = 0;
                }
            }
            timer += Time.deltaTime;
            if(timer >= 1)
            {
                timer = 0f;
            }
        }        
    }

    public void LaunchGame()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LevelName);

        LoaderContent.SetActive(true);
        isLoading = true;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);

            LoaderFill.fillAmount = progress;

            yield return null;
        }

        isLoading = false;
    }
}
