using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] static int currentExperience, maxExperience = 200, currentLevel = 1;

    private void Start()
    {
        Debug.Log("Current Experience: " + currentExperience);
        Debug.Log("Current Level: " + currentLevel);
    }

    public void GainExperience(int exp)
    {
        currentExperience += exp;

        Debug.Log("Current Experience: " + currentExperience);

        if(currentExperience >= maxExperience)
        {
            LevelUp();
        }

        Debug.Log("Current Experience: " + currentExperience);
    }

    private void LevelUp()
    {
        currentLevel += 1;
        currentExperience = 0;
        maxExperience += 200;
        Debug.Log("Level " + currentLevel + "!");
    }
  
}
