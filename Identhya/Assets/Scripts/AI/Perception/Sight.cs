using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public GameObject SeekingObject_N;
    public Transform SightTransform_A;
    [SerializeField] List<string> DetectableTags;
    [SerializeField] float UpdateInterval = 0.5f;
    [SerializeField] float MaxDistance = 30.0f;
    [SerializeField] float VisionAngle = 90.0f;
    [SerializeField] float ExtraHeightInTarget = 0.5f;
    [SerializeField] float ExtraHeightInSource = 0.5f;

    public delegate void OnObjectDetectStateDelegate(GameObject obj, GameObject sighter);

    public OnObjectDetectStateDelegate OnObjectSighted;
    public OnObjectDetectStateDelegate OnObjectLost;

    public bool tracking = false;

    public bool Enabled = true;

    void Start()
    {
        if(SightTransform_A==null)
        {
            SightTransform_A = this.gameObject.transform;
        }
        StartCoroutine(SightUpdate());
    }

    public void SetVisionAngle(float Angle)
    {
        VisionAngle = Angle;
    }

    public bool CanSeeTarget()
    {
        return tracking;
    }

    IEnumerator SightUpdate()
    {
        for(; ; )
        {
            if (Enabled)
            {

                Vector3 Origin = this.transform.position + Vector3.up * ExtraHeightInSource;

                if (SeekingObject_N != null)
                {
                    RaycastHit Hit;
                    bool detected = false;

                    Vector3 Destination = SeekingObject_N.transform.position + Vector3.up * ExtraHeightInTarget;
                    Vector3 SeekingDirection = (Destination - Origin).normalized;
                    Debug.DrawLine(Origin, Origin + SeekingDirection * MaxDistance, Color.yellow);
                    if (Physics.Raycast(Origin, SeekingDirection, out Hit, MaxDistance))
                    {
                        //Debug.Log("Hitting: " + Hit.collider.gameObject.name);
                        if (SeekingObject_N == Hit.collider.gameObject)
                        {
                            if (Vector3.Dot(SeekingDirection, this.transform.forward) > Mathf.Cos(VisionAngle / 2.0f))
                            {
                                Debug.DrawLine(Origin, Hit.point, Color.green);
                                //Debug.Log("<color=green> Seeing " + Hit.collider.gameObject.name + "</color>");

                                OnObjectSighted?.Invoke(Hit.collider.gameObject, this.gameObject);
                                tracking = true;

                                detected = true;
                            }
                        }
                        else
                        {
                            if (tracking)
                            {
                                tracking = false;
                                OnObjectLost?.Invoke(SeekingObject_N, this.gameObject);
                            }
                        }
                    }
                    if (!detected)
                    {
                        //Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward * MaxDistance, Color.red);
                    }
                }
                else
                {
                    RaycastHit Hit;
                    if (Physics.Raycast(Origin, this.transform.forward, out Hit, MaxDistance))
                    {
                        Debug.DrawLine(Origin, Hit.point, Color.green);
                        Debug.Log("<color=green> Seeing " + Hit.collider.gameObject.name + "</color>");
                    }
                    else
                    {
                        Debug.DrawLine(Origin, Origin + this.transform.forward * MaxDistance, Color.red);
                    }
                }

            }

            yield return new WaitForSeconds(UpdateInterval);
        }
    }
}
