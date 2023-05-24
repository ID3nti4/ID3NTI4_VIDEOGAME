using UnityEngine;

public class Dialogant : MonoBehaviour
{
    public NPCDialogue dial;
    public int pensamientoKira;
    public bool blockPlayerControllers;
    public bool canSkipLine;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            dial.RunDialogue(pensamientoKira);
            dial.SetCanSkipLines(canSkipLine);
            gameObject.SetActive(false);
            if (blockPlayerControllers)
            {
                FindObjectOfType<Kira>().SetPlayerBlocked(true);
            }
        }
    }
}
