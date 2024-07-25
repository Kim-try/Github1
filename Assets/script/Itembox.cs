using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
public class Itembox : MonoBehaviour
{
    public bool isPlayer;
    public GameObject Player;
    public GameObject BoxUI;
    public GameObject InventoryUI;
    public int itemboxcount = 4;
    public List<Item> boxitems = new List<Item>();
    public static Itembox instance;
    public BoxSlot[] boxslots;
    public Transform boxslotHolder;
    private void Awake()
    {
        instance = this;
        Player= GameObject.Find("Player");
    }
    private void Update()
    {
        Echeck();
        BoxOpen();
        //구현할거 1. 상점창 열기 2. 게임끝날때 자동판매 
    }
    public void Echeck()
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
        else if (isPlayer)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void BoxOpen()
    {
        if (isPlayer)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                BoxUI.gameObject.SetActive(true);
                InventoryUI.SetActive(true);
                GameObject.Find("InventoryController").GetComponent<InventoryUI>().isOpen = true;
            }
        }
    }
    public void BoxItemGain(Item newboxitem, int value) //고칠것 1.inventory의 값을 없애면 안됨 즉 복사해서 넣어야함
    {
        if (boxitems.Count == 0)
        {
            Item newitem = newboxitem;
            boxitems.Add(newitem);
            boxslots[0].boxslotitemcount++;
        }
        else if (newboxitem != null)
        {
            bool exist = boxitems.Exists(x => x.itemcode == newboxitem.itemcode);
            for (int i = 0; i < boxitems.Count; i++)
            {              
                if (exist)
                {
                    if (boxitems[i].itemcode == newboxitem.itemcode)
                    {
                        boxslots[i].boxslotitemcount += value;
                        break;
                    }
                }
                else if (!exist)
                {
                    if (boxitems.Count >= itemboxcount)
                    {
                        itemboxcount++;
                    }
                    Item newitem = newboxitem;
                    boxitems.Add(newitem);
                    boxslots[boxitems.Count - 1].boxslotitemcount+= value;
                    break;
                }
            }
        }
    }
    
}
