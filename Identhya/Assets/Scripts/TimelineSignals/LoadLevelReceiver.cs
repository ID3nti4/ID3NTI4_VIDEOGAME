﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelReceiver : MonoBehaviour
{
    public string LevelToLoad = "";
    public void LoadLevel()
    {
        Checkpoint.ClearCheckpoint();
        SceneManager.LoadScene(LevelToLoad);
    }
}
