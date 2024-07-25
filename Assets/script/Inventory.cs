using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<Item> inven = new List<Item>();
    public int inventorycount = 4;
    public int maxitemweight = 100;
    public int itemweight;
    public static Inventory Instance;
    public Slot[] slots;
    public Transform slotHolder;
    private void Awake()
    {
        Instance = this;
        slots = slotHolder.GetComponentsInChildren<Slot>();
    }
    public void ItemGain(Item index)
    {
        if (inven.Count == 0)
        {
            Item newitem = index;
            slots[0].slotitemcount++;
            inven.Add(newitem);
        }
        else if (inven != null)
        {
            bool exist = inven.Exists(x => x.itemcode == index.itemcode);
            for (int i = 0; i < inven.Count; i++)
            {
                if (exist)
                {
                    if (inven[i].itemcode == index.itemcode)
                    {
                        slots[i].slotitemcount++;
                        break;
                    }
                }
                else if (!exist)
                {
                    if(index.Type == Item.ItemType.Crystal)
                    {
                        Player.instance.Crystal += index.itemmoney;
                    }
                    else
                    {
                        if (inven.Count >= inventorycount)
                        {
                            inventorycount++;
                        }
                        Item newitem = index;
                        inven.Add(newitem);
                        slots[inven.Count - 1].slotitemcount++;
                        break;
                    }                  
                }
            }
        }
        Player.instance.MoneyCount();
        Player.instance.WeightCheck();
    }
    public void SlotCheck()
    {
        for (int i = 0; i < inven.Count; i++)
        {
            if (slots[i].slotitemcount == 0)
            {
                inven.RemoveAt(i);
                slots[i].item = null;
                slots[i].itemimage.sprite = null;
                for (int j = i; j < inven.Count; j++)
                {
                    if (slots[j + 1].slotitemcount != 0)
                    {
                        slots[j].item = slots[j + 1].item;
                        slots[j].itemimage.sprite = slots[j + 1].itemimage.sprite;
                        slots[j].slotitemcount = slots[j + 1].slotitemcount;
                    }
                    slots[inven.Count].item = null;
                    slots[inven.Count].itemimage.sprite = null;
                    slots[inven.Count].slotitemcount = 0;
                }
            }
        }
    }
    public void SceneCheck()
    {
        slotHolder = GameObject.Find("InventoryController").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Transform>();
        slots = slotHolder.GetComponentsInChildren<Slot>();
    }
}
