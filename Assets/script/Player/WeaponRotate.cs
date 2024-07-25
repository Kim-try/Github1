using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class WeaponRotate : MonoBehaviour
{
    public static WeaponRotate instance; 
    public ishand inHand;
    public Transform isHandTrans;
    public MouseCur Mousecur;
    private void Awake()
    {
        Mousecur = GameObject.Find("MouseCur").GetComponent<MouseCur>();
        inHand = isHandTrans.GetComponentInChildren<ishand>();
        instance = this;
    }
    void Update()
    {
        if(inHand.ishanditem != null)
        {
            if (inHand.ishanditem.Type == Item.ItemType.Equipment)
            {
                inHand.ishanditem.isHand = true;
            }
        }      
        else return;
        Vector2 wea = Mousecur.transform.position - transform.position;
        float z = Mathf.Atan2(wea.y, wea.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }
}
