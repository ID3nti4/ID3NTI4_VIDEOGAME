using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDogOnceController : MonoBehaviour
{
    public TextDialogue DialogueToRun;
    Dog[] dogs;
    List<HealthComponent> components;
    void Start()
    {
        components = new List<HealthComponent>();
        dogs = FindObjectsOfType<Dog>();
        foreach(Dog dog in dogs)
        {
            components.Add(dog.GetComponent<HealthComponent>());
        }
        foreach(HealthComponent component in components)
        {
            component.OnCharacterDamage += OnDamage;
        }
    }

    public void TearDown()
    {
        foreach (HealthComponent component in components)
        {
            component.OnCharacterDamage -= OnDamage;
        }
    }

    protected void OnDamage(float Amount, AttackModifier Modifier, GameObject Source)
    {
        FindObjectOfType<DialogueRunner>().DoDialogue(DialogueToRun);
        TearDown();
    }
}
