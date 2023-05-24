using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Sprite InteractSprite;
    SpriteRenderer sprite;
    float InitialScale;
    float scale = 0.0f;
    [SerializeField] float scaleSpeed = 2.0f;
    Coroutine scaleCoRo;

    public delegate void OnObjectInteractedDelegate();
    public OnObjectInteractedDelegate OnObjectInteracted;

    protected InteractComponent interactingComponent = null;

    void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sprite = InteractSprite;
        InitialScale = sprite.gameObject.transform.localScale.x;
        sprite.gameObject.transform.localScale = Vector3.zero;
        scaleCoRo = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(scaleCoRo != null)
            {
                StopCoroutine(scaleCoRo);
            }
            scaleCoRo = StartCoroutine(ScaleIconUp());
            interactingComponent = other.gameObject.GetComponent<InteractComponent>();
            interactingComponent.SetInteractor(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (scaleCoRo != null)
            {
                StopCoroutine(scaleCoRo);
            }
            scaleCoRo = StartCoroutine(ScaleIconDown());
            if (interactingComponent != null)
            {
                interactingComponent.SetInteractor(null);
                interactingComponent = null;
            }
        }
    }

    protected IEnumerator ScaleIconUp()
    {
        while(scale < 1.0f)
        {
            yield return new WaitForEndOfFrame();
            scale += scaleSpeed * Time.deltaTime;
            sprite.gameObject.transform.localScale = Vector3.one * scale * InitialScale;
        }
        scale = 1.0f;
        sprite.gameObject.transform.localScale = Vector3.one * scale * InitialScale;

    }

    protected IEnumerator ScaleIconDown()
    {
        while (scale > 0.0f)
        {
            yield return new WaitForEndOfFrame();
            scale -= scaleSpeed * Time.deltaTime;
            sprite.gameObject.transform.localScale = Vector3.one * scale * InitialScale;
        }
        scale = 0.0f;
        sprite.gameObject.transform.localScale = Vector3.one * scale * InitialScale;

    }

    public virtual void Interact()
    {

    }

}
