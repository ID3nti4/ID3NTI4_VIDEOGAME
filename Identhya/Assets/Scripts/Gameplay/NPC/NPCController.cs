using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    Animator animator;

    [SerializeField] string TalkingAnimKey = "Talking";
    [SerializeField] string TalkModeAnimKey = "TalkMode";

    float SpeakBlendState = 0.0f;
    float TargetBlendState = 0.0f;
    [SerializeField] float InterpolationSpeed = 10.0f;

    private void UpdateSoftVariables()
    {
        if (animator.GetBool(TalkingAnimKey))
        {
            SpeakBlendState = Mathf.Lerp(SpeakBlendState, TargetBlendState, InterpolationSpeed * Time.deltaTime);
            animator.SetFloat(TalkModeAnimKey, SpeakBlendState);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(TalkingAnimKey, false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSoftVariables();
    }

    public void SetSpeaking(bool spk)
    {
        animator.SetBool(TalkingAnimKey, spk);
    }

    public void SetAttitude(int att)
    {
        TargetBlendState = (((float)(att)) / 4.0f);
        Debug.Log("New target blend state: " + TargetBlendState);
    }
}
