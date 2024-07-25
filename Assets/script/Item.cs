using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName ="New item", menuName ="New item/item")]
public class Item : ScriptableObject
{
    public ItemType Type;
    public string itemName;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public int itemcode;
    public int itemweight;
    public int itemmoney;
    public string itemexplanation;
    public bool isHand = false;
    public int itemgrade;
    public int itemdamage;
    public EquipType equiptype;
    public enum ItemType
    {
         Equipment, Used, Ingredient, Crystal
    }   
    public enum EquipType
    {
        LongDistanceWeapon, ShortDistanceWeapon, Pickax, Digger, Etc
    }
}
