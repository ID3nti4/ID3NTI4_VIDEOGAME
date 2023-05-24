using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneState_Chasing : DroneState
{
    DroneAIFlyTo aiFlyTo;
    Sight sight;
    Drone _drone;
    //Transform capturePos;
    Animator animator;
    public float CaptureTime = 8.0f;
    public GameObject ElectricityPrefab;

    bool isCapturing = false;

    [SerializeField] string CaptureAnimKey = "Capture";

    [SerializeField] AudioClip pursuitSound;

    [SerializeField] float Timer = 0.0f;
    [SerializeField] float LostTimer = 0.0f;

    new void Awake()
    {
        base.Awake();
        _drone = GetComponent<Drone>();
        sight = GetComponent<Sight>();
        aiFlyTo = GetComponent<DroneAIFlyTo>();
        animator = GetComponentInChildren<Animator>();
    }

    public override void OnStateEnter(State prevState)
    {
        Timer = 0;
        LostTimer = 0;

        GetComponent<DroneFXController>().Play(pursuitSound, true);
        aiFlyTo.StopMovement();
        Vector3 targetPos = _drone.Target.transform.position;
        Quaternion targetRot = _drone.Target.transform.rotation;
        targetPos = new Vector3(targetPos.x, targetPos.y + 5.0f, targetPos.z);
        aiFlyTo.FlyTo(targetPos, targetRot);
        aiFlyTo.stopAtTarget = true;
    }

    public override void OnStateLeave(State nextState)
    {
        
    }

    public override void UpdateState()
    {
        
        RaycastHit hit;
        
        if (!sight.CanSeeTarget())
        {
            Timer = 0.0f;
            LostTimer += Time.deltaTime;
            if (LostTimer > 1.0f)
            {
                machine.SetState(GetComponent<DroneState_Alerted>());
                aiFlyTo.StopMovement();
                return;
            }
        }
        else
        {
            FindObjectOfType<AttackMusicController>().PlayerUnderAttack();
            LostTimer = 0.0f;
            Timer += Time.deltaTime;
        }

        if (Timer >= CaptureTime && !isCapturing)
        {
            isCapturing = true;
            StartCoroutine(CaptureCoro());
            Debug.Log("Captured You!");
            
            
            //Player.GetComponent<KiraCapture>().Dead();
            //CanvasDead.SetActive(true);
        }
    }

    IEnumerator ElectrifyTarget()
    {
        while(1<2)
        {
            yield return new WaitForSeconds(0.75f);
            GameObject newGO = (GameObject)Instantiate(ElectricityPrefab);
            newGO.transform.position = _drone.Target.transform.position + Vector3.up * 0.5f + Random.insideUnitSphere * 0.25f;
        }
    }

    IEnumerator CaptureCoro()
    {
        StartCoroutine(ElectrifyTarget());
        UnityInputCharacterControls PlayerControls = _drone.Target.GetComponent<UnityInputCharacterControls>();
        Debug.Log("<color=green> ############################# DISABLE PLAYER INPUT ################################## </color>");
        Kira Player = _drone.Target.GetComponent<Kira>();
        Player.SetPlayerBlocked(true);
        //Player.DisablePhysics();
        //float heightOffset = transform.position.y - capturePos.position.y;
        Vector3 playerPos = Player.transform.position;
        Quaternion playerRot = Player.transform.rotation;
        
        playerPos = new Vector3(playerPos.x, playerPos.y + 3.0f, playerPos.z);

        aiFlyTo.FlyTo(playerPos, playerRot);
        GetComponent<DroneRCController>().PitchSpeed *= 2.0f;
        Quaternion droneRot = this.transform.rotation;
        Quaternion targetRot = Player.transform.rotation;

        float Elapsed = 0;
        while (!aiFlyTo.IsDone() && Elapsed < 5.0f)
        {
            //droneRot = Quaternion.Lerp(droneRot, targetRot, Time.deltaTime * 10f);
            //this.transform.rotation = droneRot;
            Debug.Log("Flying to player");
            yield return new WaitForEndOfFrame();
            Elapsed += Time.deltaTime;
        }

        //Player.GetComponent<Animator>().SetTrigger("Capture");
        //animator.SetTrigger("Capture");
        Debug.Log("Capture");

        yield return new WaitForSeconds(1f);

        PlayerControls.Enabled = false;
        Debug.Log("<color=green> ############################# HURT PLAYER ################################## </color>");
        FindObjectOfType<Kira>().gameObject.GetComponent<HealthComponent>().TakeDamage(100.0f, this.gameObject);
        //isCapturing = false;
    }
}
