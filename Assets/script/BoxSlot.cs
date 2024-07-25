using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BoxSlot : MonoBehaviour
{
    public Item boxitem;
    public Image boxitemimage;
    public int boxslotitemcount;
    public Inventory inventory;
    public ShopNPC shopnpc;
    public Button Boxslot;
    private void Awake()
    {
        Boxslot = this.gameObject.GetComponent<Button>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void OnEnable()
    {
        shopnpc = GameObject.Find("ShopNPC").GetComponent<ShopNPC>();
    }
    public void UpdateBoxSlotUI()
    {
        boxitemimage.sprite = boxitem.itemImage;
        boxitemimage.gameObject.SetActive(true);
    }
    public void TakeOutItem()
    {
        for(int i = 0; i < boxslotitemcount; i++)
        {
            inventory.ItemGain(boxitem);
        }
        boxslotitemcount = 0;
        shopnpc.BoxCheck();
    }
}
