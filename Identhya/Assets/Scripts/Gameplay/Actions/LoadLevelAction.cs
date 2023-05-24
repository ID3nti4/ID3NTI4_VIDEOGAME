using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAction : GameplayAction
{
    public string levelToLoad = "";

    public override Coroutine DoAction(GameObject source)
    {
        if (levelToLoad == "")
        {
            string LevelToLoad = PlayerPrefs.GetString("RespawnLevel");
            SceneManager.LoadSceneAsync(LevelToLoad);
        }
        else
        {
            Checkpoint.ClearCheckpoint();
            SceneManager.LoadSceneAsync(levelToLoad);
        }
        return StartCoroutine(FinishImmediately());
    }
}
