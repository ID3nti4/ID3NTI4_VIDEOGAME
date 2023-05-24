using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbTestController : MonoBehaviour
{
    public GameObject climbobstacle;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.A))
        {
            climbobstacle.SetActive(false);
        }*/

        /*if(Input.GetKeyDown(KeyCode.B))
        {
            player.GetComponent<CapsuleCollider>().enabled = false;
        }*/

        if(Input.GetKeyDown(KeyCode.C))
        {
            player.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(40, 40, 40);
            Debug.Log("KOASKDOAKDOKA");
        }
    }
}
