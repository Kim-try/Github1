using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemimage;
    public GameObject ItemPanel;
    public Button EquipButton;
    public Button MoveButton;
    public bool activepanel = false;
    public Slider slider;
    public int slotitemcount;
    public ishand ishand;
    private void Awake()
    {
        ishand = GameObject.Find("Hand").transform.GetChild(0).GetComponent<ishand>();
    }
    private void Start()
    {
        EquipButton.onClick.AddListener(PanelItem);
        MoveButton.onClick.AddListener(MoveItem);
        slotitemcount = 0;
    }
    public void UpdateSlotUI()
    {
        itemimage.sprite = item.itemImage;
        itemimage.gameObject.SetActive(true);
    }
    public void RemoveSlotUI()
    {
        item = null;
        itemimage.gameObject.SetActive(false);
    }

    public void OnCLickButton()//버튼 클릭으로 변경해서 클릭하면 panel on 클릭 한번 더 하면 off
    {
        activepanel = !activepanel;
        ItemPanel.SetActive(activepanel);
    }

    public void PanelItem()
    {
        if (item != null)
        {
            if (item.Type == Item.ItemType.Equipment)
            {
                if(ishand.ishanditem ==null)
                {
                    ishand.ishanditem = item;
                    ishand.sprite.sprite = item.itemImage;
                    ItemPanel.SetActive(false);
                }
                else if (ishand.ishanditem != null && ishand.ishanditem.itemcode != item.itemcode)
                {
                    ishand.ishanditem = item;
                    ishand.sprite.sprite = item.itemImage;
                    ItemPanel.SetActive(false);
                }
                else if(ishand.ishanditem != null && ishand.ishanditem.itemcode == item.itemcode)
                {
                    ishand.ishanditem = null;
                    ishand.sprite.sprite = null;
                    ItemPanel.SetActive(false);
                }
            }
            else
            {
                ItemPanel.SetActive(false);
            }
        }
        else return;
    }
    public void MoveItem()
    {
        slider.gameObject.SetActive(true);
        slider.GetComponent<SliderController>().SliderCheck(this);
    }
    public void RemoveItem(int value)
    {
        slotitemcount -= value;
    }
}
