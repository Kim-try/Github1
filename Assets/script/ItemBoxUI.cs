using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxUI : MonoBehaviour
{
    public Button CloseButton;
    public Button SellButton;
    public Button BuyButton;
    public GameObject InventoryUI;
    public BoxSlot[] boxslots;
    public Transform BoxslotHolder;
    public ShopNPC ShopNPC;
    private void OnEnable()
    {
        ShopNPC = GameObject.Find("ShopNPC").GetComponent<ShopNPC>();
        SellButton.onClick.AddListener(ShopNPC.ItemSell);
    }
    private void Start()
    {
        CloseButton.onClick.AddListener(BoxClose);
        boxslots = BoxslotHolder.GetComponentsInChildren<BoxSlot>();
    }
    private void Update()
    {
        BoxGain();
        for (int i = 0; i < boxslots.Length; i++)
        {
            if (i < 16)
            {
                boxslots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                boxslots[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    public void BoxClose()
    {
        bool b = Array.Exists(boxslots, x => x.boxslotitemcount > 0);
        if (!b)
        {
            gameObject.SetActive(false);
            InventoryUI.SetActive(false);
            GameObject.Find("InventoryController").GetComponent<InventoryUI>().isOpen = false;
        }
        else return;
    }
    public void BoxGain()
    {
        for (int i = 0; i < ShopNPC.instance.boxitems.Count; i++)
        {
            boxslots[i].boxitem = ShopNPC.instance.boxitems[i];
            boxslots[i].UpdateBoxSlotUI();
        }
    }
}
