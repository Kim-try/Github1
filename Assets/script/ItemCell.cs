using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCell : MonoBehaviour
{
    public Itembox itembox;
    public int CellMoney;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))//¾À ³¡³µÀ»¶§·Î º¯°æ
        {
            ItemCelling();
        }
    }
    public void ItemCelling()
    {

    }
}
