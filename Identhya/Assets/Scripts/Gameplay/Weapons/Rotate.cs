using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 AngularVelocity;
    // Start is called before the first frame update

    bool going = false;

    // Update is called once per frame
    void Update()
    {
        if (!going) return;
        this.transform.Rotate(AngularVelocity * Time.deltaTime);    
    }

    public void Go()
    {
        going = true;
    }

    public void Reset()
    {
        going = false;
        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
