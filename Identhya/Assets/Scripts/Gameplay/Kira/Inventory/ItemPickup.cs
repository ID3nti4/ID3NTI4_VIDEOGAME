using TMPro;
using UnityEngine;

public class ItemPickup : MonoBehaviour//Interactable
{
    /*
    [SerializeField] private TextMeshProUGUI lookPercentageLabel;

    [HideInInspector] public float LookPercentage;

    public Item item;
    public GameObject InvSlot;
    public bool pickedUp;

    void Update()
    {
        lookPercentageLabel.text = LookPercentage.ToString("F3");

        float distance = Vector3.Distance(player.position, transform.position);

        if (response && distance <= radius && Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }
    void PickUp()
    {
        //pickedUp = Inventory.instance.Add(item);

        if (pickedUp == true)
        {
            Debug.Log("Item picked up");
            Destroy(gameObject);
        }
    }
    void NewSlot(GameObject go)
    {
        go = Instantiate(InvSlot, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        go.transform.parent = GameObject.Find("ItemsParent").transform;

        Destroy(InvSlot);
        go.transform.localPosition = new Vector3(0f, 0f, 0f);
        //go.transform.localRotation = Quaternion.Euler(0, 0, 0);
        go.transform.localScale = new Vector3(250f, 250f, 250f);
    }*/
}