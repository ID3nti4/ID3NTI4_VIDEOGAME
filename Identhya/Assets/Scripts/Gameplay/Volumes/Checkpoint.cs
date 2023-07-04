using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StoreCheckpoint(other.gameObject.transform);
        }
    }

    private void StoreCheckpoint(Transform playerTransform)
    {
        PlayerPrefs.SetInt("CheckpointStored", 1);
        PlayerPrefs.SetString("RespawnLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("RespawnPosition.x", playerTransform.position.x);
        PlayerPrefs.SetFloat("RespawnPosition.y", playerTransform.position.y);
        PlayerPrefs.SetFloat("RespawnPosition.z", playerTransform.position.z);
        PlayerPrefs.SetFloat("RespawnEuler.x", playerTransform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("RespawnEuler.y", playerTransform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("RespawnEuler.z", playerTransform.rotation.eulerAngles.z);
        Debug.Log("se guarda checkpoint");
    }

    public static void ClearCheckpoint()
    {
        PlayerPrefs.SetInt("CheckpointStored", 0);
        PlayerPrefs.SetInt("CavernLightSide", 0);
        PlayerPrefs.SetInt("CavernTimelinePlayed", 0);
    }
}