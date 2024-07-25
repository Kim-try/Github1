using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public bool isPlayer;
    public GameObject Player;
    public Inventory inventory;
    private void Awake()
    {
        Player = GameObject.Find("Player").gameObject;
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void Update()
    {
        if ((Player.transform.position - gameObject.transform.position).magnitude >= 1.8)
        {
            isPlayer = false;
        }
        else
            isPlayer = true;
        if (!isPlayer)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else if(isPlayer)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                inventory.ItemGain(item);
                Destroy(this.gameObject);
            }
        }
    }
}
