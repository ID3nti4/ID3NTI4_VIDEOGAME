using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAtCheckpoint : MonoBehaviour
{
    public bool Ignore = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (Ignore) return;

        if (PlayerPrefs.GetInt("CheckpointStored") != 0)
        {
            float tx = PlayerPrefs.GetFloat("RespawnPosition.x");
            float ty = PlayerPrefs.GetFloat("RespawnPosition.y");
            float tz = PlayerPrefs.GetFloat("RespawnPosition.z");
            float ex = PlayerPrefs.GetFloat("RespawnEuler.x");
            float ey = PlayerPrefs.GetFloat("RespawnEuler.y");
            float ez = PlayerPrefs.GetFloat("RespawnEuler.z");
            Vector3 position = new Vector3(tx, ty, tz);
            Vector3 eulerAngles = new Vector3(ex, ey, ez);
            this.transform.position = position;
            this.transform.rotation = Quaternion.Euler(eulerAngles);
        }
    }

}
