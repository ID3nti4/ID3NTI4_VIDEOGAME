using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    public bool Enabled = true;
    public bool DisableOnSensing = false;
    public float SensorCooldown = 0.0f;
    public Transform SightTransform_A;
    [SerializeField] float ExtraHeightInSource = 0.5f;
    [SerializeField] float UpdateInterval = 0.15f;
    [SerializeField] float MaxProbeDistance = 3.0f;

    public delegate void OnObstacleSensedDelegate(GameObject obj);
    public OnObstacleSensedDelegate OnObstacleSensed;

    // Start is called before the first frame update
    void Start()
    {
        if(SightTransform_A==null)
        {
            SightTransform_A = this.gameObject.transform;
        }
        StartCoroutine(SonarProbe());
    }

    IEnumerator SonarProbe()
    {
        for (; ; )
        {
            if (Enabled)
            {

                Vector3 Origin = this.transform.position + Vector3.up * ExtraHeightInSource;

                
                RaycastHit Hit;
                if (Physics.Raycast(Origin, this.transform.forward, out Hit, MaxProbeDistance))
                {
                    Debug.DrawLine(Origin, Hit.point, Color.red);
                    //Debug.Log("<color=green> Sensing " + Hit.collider.gameObject.name + "</color>");
                    OnObstacleSensed?.Invoke(Hit.collider.gameObject);
                    if(DisableOnSensing)
                    {
                        Enabled = false;
                    }
                    yield return new WaitForSeconds(SensorCooldown);
                }
                else
                {
                    Debug.DrawLine(Origin, Origin + this.transform.forward * MaxProbeDistance, Color.magenta);
                }

            }

            yield return new WaitForSeconds(UpdateInterval);
        }
    }
}
