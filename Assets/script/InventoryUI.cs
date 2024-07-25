using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public bool activeinventory = false;
    public Slot[] slots;
    public Transform slotHolder;
    public bool isOpen;
    public Inventory inventory;
    public ishand ishand;

    private void Awake()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        ishand = GameObject.Find("Hand").transform.GetChild(0).GetComponent<ishand>();
    }
    private void Start()
    {
        inventoryPanel.SetActive(activeinventory);
        activeinventory = !activeinventory;
        inventoryPanel.SetActive(activeinventory);
    }
    private void Update()
    {
        InventoryGain();
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.inventorycount)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab)&&!isOpen)
        {
            activeinventory = !activeinventory;
            inventoryPanel.SetActive(activeinventory);
            if (transform.GetChild(0).GetChild(2).gameObject.activeSelf == true)
            {
                transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            }
            for(int i = 0;i < slots.Length; i++)
            {
                if (slots[i].transform.GetChild(1).gameObject.activeSelf == true)
                {
                    slots[i].transform.GetChild(1).gameObject.SetActive(false);
                }
            }
        }
        if(inventoryPanel.activeSelf ==true)
        {
            ishand.isItem = true;
        }
        else if (inventoryPanel.activeSelf == false)
        {
            ishand.isItem = false;
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].ItemPanel.SetActive(false);
                slots[i].activepanel = false;
            }
        }

    }
    public void InventoryGain()
    {
        for (int i = 0; i < inventory.inven.Count; i++)
        {
            slots[i].item = inventory.inven[i];
            slots[i].UpdateSlotUI();
        }
    }
}
