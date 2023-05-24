using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReactionComponent : MonoBehaviour
{
    public string[] HitAnimKey;
    public GameObject[] HitFX;
    public Animator animator;

    public string FresnelDamageEffectName = "_FresnelLevel";
    public string FresnelDamageColorName = "_FresnelColor";
    public Color HitDamageColor;

    public State HitState_N = null;
    //public Material DamageMaterial;

    public bool ReactToZeroDamage = false;

    [SerializeField] Material material = null;

    private void Start()
    {
        HealthComponent healthComponent = gameObject.GetComponent<HealthComponent>();
        if(healthComponent != null)
        {
            healthComponent.OnCharacterDamage += OnDamage;
        }
        animator = GetComponentInChildren<Animator>();

        if (material == null)
        {
            SkinnedMeshRenderer mr = GetComponentInChildren<SkinnedMeshRenderer>();
            if (mr != null)
            {
                material = mr.material;
            }

        }

        if(material != null)
        {
            material.SetFloat(FresnelDamageEffectName, 0.0f);
        }
    }

    protected void OnDamage(float Amount, AttackModifier Modifier, GameObject Source)
    {
        
        if(Amount == 0.0f && !ReactToZeroDamage)
        {
            return;
        }

        int HitAnimIndex = Random.Range(0, HitAnimKey.Length);
        animator.SetTrigger(HitAnimKey[HitAnimIndex]);

        if(HitFX.Length > 0)
        {
            int HitFXIndex = Random.Range(0, HitFX.Length);
            ((GameObject)Instantiate(HitFX[HitFXIndex])).transform.position = gameObject.transform.position;
        }

        if(HitState_N != null)
        {
            GetComponent<StateMachine>().SetState(HitState_N);
        }

        StartCoroutine(VisualDamage());
    }

    IEnumerator VisualDamage()
    {
        if(material != null)
        {
            material.SetFloat(FresnelDamageEffectName, 0.2f);
            yield return new WaitForEndOfFrame();
            material.SetFloat(FresnelDamageEffectName, 0.8f);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            material.SetFloat(FresnelDamageEffectName, 0.3f);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            material.SetFloat(FresnelDamageEffectName, 0.8f);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            material.SetFloat(FresnelDamageEffectName, 0.2f);
            yield return new WaitForEndOfFrame();
            material.SetFloat(FresnelDamageEffectName, 0.0f);
        }
    }

}
