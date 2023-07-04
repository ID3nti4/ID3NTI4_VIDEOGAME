using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public static float px, py, pz, rx, ry, rz;
    public static bool checkpointStored = false;
    public static bool lastCheckpoint = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StoreCheckpoint(other.gameObject.transform);
        }
    }

    private void StoreCheckpoint(Transform playerTransform)
    {
        checkpointStored = true;
        PlayerPrefs.SetString("RespawnLevel", SceneManager.GetActiveScene().name);
        px = playerTransform.position.x;
        py = playerTransform.position.y;
        pz = playerTransform.position.z;
        rx = playerTransform.rotation.eulerAngles.x;
        ry = playerTransform.rotation.eulerAngles.y;
        rz = playerTransform.rotation.eulerAngles.z;
        Debug.Log("se guarda checkpoint");

        if(this.tag == "Last")
        {
            lastCheckpoint = true;
        }
    }

    public static void ClearCheckpoint()
    {
        checkpointStored = false;
        PlayerPrefs.SetInt("CheckpointStored", 0);
        PlayerPrefs.SetInt("CavernLightSide", 0);
        PlayerPrefs.SetInt("CavernTimelinePlayed", 0);
    }
}