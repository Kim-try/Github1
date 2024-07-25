using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Burst.CompilerServices;
public class SliderController : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI SliderText;
    public Slot slot;
    public Button SliderButton;
    public int buttonvalue = 0;
    public Inventory inventory;
    public Player player;
    public ishand ishand;
    private void Awake()
    {
        ishand = GameObject.Find("IsHand").GetComponent<ishand>();
        player = GameObject.Find("Player").GetComponent<Player>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        slider.onValueChanged.AddListener(SliderTextChange);
        SliderButton.onClick.AddListener(SliderButtonCheck);
    }
    public void SliderCheck(Slot newslot)
    {
        slot = null;
        slot = newslot;
        slider.maxValue = newslot.slotitemcount;
        slider.minValue = 0;
    }
    public void SliderTextChange(float value)
    {
        SliderText.text = $"Count {slider.value:F0}";
    }

    public void SliderButtonCheck()
    {
        Vector3 RanPos = new Vector3(GameObject.Find("Player").transform.position.x + Random.Range(-1.0f,2.0f), GameObject.Find("Player").transform.position.y + Random.Range(-1.0f, 2.0f), GameObject.Find("Player").transform.position.z) ;
        buttonvalue = Mathf.FloorToInt(slider.value);
        if (GameObject.Find("InventoryController").GetComponent<InventoryUI>().isOpen == true)
        {
            GameObject.Find("ShopNPC").GetComponent<ShopNPC>().BoxItemGain(slot.item, buttonvalue);
        }
        else if (GameObject.Find("InventoryController").GetComponent<InventoryUI>().isOpen == false)
        {
            for (int i = 0; i < buttonvalue; i++)
            {
                Instantiate(slot.item.itemPrefab, RanPos, Quaternion.identity);
                player.MoneyCount();
                player.WeightCheck();
            }
        }
        slot.GetComponent<Slot>().RemoveItem(buttonvalue);
        inventory.SlotCheck();
        ishand.ishandItemCheck();
        Player.instance.WeightCheck();
        gameObject.SetActive(false);
        slot.ItemPanel.SetActive(false);
    }
}
