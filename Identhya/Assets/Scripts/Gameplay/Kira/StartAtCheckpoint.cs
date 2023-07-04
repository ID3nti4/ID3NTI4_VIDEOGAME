using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAtCheckpoint : MonoBehaviour
{
    public bool Ignore = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (Ignore) return;

        if (Checkpoint.checkpointStored == true)
        {
            float tx = Checkpoint.px;
            float ty = Checkpoint.py;
            float tz = Checkpoint.pz;
            float ex = Checkpoint.rx;
            float ey = Checkpoint.ry;
            float ez = Checkpoint.rz;
            Vector3 position = new Vector3(tx, ty, tz);
            Vector3 eulerAngles = new Vector3(ex, ey, ez);
            this.transform.position = position;
            this.transform.rotation = Quaternion.Euler(eulerAngles);

            if (Checkpoint.lastCheckpoint == true)
            {
                FindObjectOfType<EquipmentController>().GetBoots();
                FindObjectOfType<EquipmentController>().GetGloves();
                FindObjectOfType<BoomerangController>().ForceSpawnBoomerang = true;
            }
        }
    }

}
