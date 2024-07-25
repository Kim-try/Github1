using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopNPC : Npc
{
    public static ShopNPC instance;
    public GameObject BoxUI;
    public GameObject InventoryUI;
    public int itemboxcount = 4;
    public List<Item> boxitems = new List<Item>();
    public BoxSlot[] boxslots;
    public Transform boxslotHolder;
    public bool issell=false;
    public bool isbuy=false;
    public int CellMoney;
    private void Awake()
    {
        instance = this;
        Player = GameObject.Find("Player").gameObject;
        InventoryUI = GameObject.Find("InventoryController").gameObject;
        BoxUI = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        boxslotHolder = BoxUI.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Transform>();
    }
    private void Start()
    {
        boxslots = boxslotHolder.GetComponentsInChildren<BoxSlot>();
    }
    private void Update()
    {
        Pcheck();
        Talk(this.npctype);
    }
    public void ShopOpen(bool value)
    {
        if (value)
        {
            BoxUI.gameObject.SetActive(true);
            GameObject.Find("InventoryController").GetComponent<InventoryUI>().isOpen = true;
            InventoryUI.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void BoxItemGain(Item newboxitem, int value)
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
                    boxslots[boxitems.Count - 1].boxslotitemcount += value;
                    break;
                }
            }
        }
    }
    public void ItemSell()
    {
        for (int i =  0; i <boxitems.Count ; i++)
        {
            CellMoney += boxitems[i].itemmoney * boxslots[i].boxslotitemcount;
            boxslots[i].boxslotitemcount = 0;
            boxslots[i].boxitem = null;
            boxslots[i].boxitemimage.sprite = null;
        }
        boxitems.RemoveAll(x => x.itemcode > 0);
        BoxCheck();
        Debug.Log(CellMoney);
        PlayerPrefs.SetInt("Money", GameManager.instance.Money + CellMoney);
        CellMoney = 0;
    }
    public void BoxCheck()
    {
        for (int i = 0; i < boxitems.Count; i++)
        {
            if (boxslots[i].boxslotitemcount == 0)
            {
                boxitems.RemoveAt(i);
                boxslots[i].boxitem = null;
                boxslots[i].boxitemimage.sprite = null;
                for (int j = i; j < boxitems.Count; j++)
                {
                    if (boxslots[j + 1].boxslotitemcount != 0)
                    {
                        boxslots[j].boxitem = boxslots[j + 1].boxitem;
                        boxslots[j].boxitemimage.sprite = boxslots[j + 1].boxitemimage.sprite;
                        boxslots[j].boxslotitemcount = boxslots[j + 1].boxslotitemcount;
                    }
                    boxslots[boxitems.Count].boxitem = null;
                    boxslots[boxitems.Count].boxitemimage.sprite = null;
                    boxslots[boxitems.Count].boxslotitemcount = 0;
                }
            }
        }
        Debug.Log("BoxCheck");
    }
}
