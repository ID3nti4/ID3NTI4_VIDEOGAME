using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMusicController : MonoBehaviour
{
    [SerializeField] AudioClip attackMusic;
    [SerializeField] float Cooldown = 2.0f;
    [SerializeField] AudioClip normalMusic;
    // Start is called before the first frame update
    void Start()
    {
        normalMusic = FindObjectOfType<MusicController>().GetPlayingClip();
        remain = -1.0f;
    }

    float remain;

    public void PlayerUnderAttack()
    {
        if(remain <= 0.0f)
        {
            Debug.Log("Playing attack music");
            normalMusic = FindObjectOfType<MusicController>().GetPlayingClip();
            FindObjectOfType<MusicController>().Play(attackMusic, true);
        }
        remain = Cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(remain > 0.0f)
        {
            remain -= Time.deltaTime;
            if(remain <= 0.0f)
            {
                Debug.Log("Stopping attack music");
                FindObjectOfType<MusicController>().Play(normalMusic, true);
            }
        }
    }
}
