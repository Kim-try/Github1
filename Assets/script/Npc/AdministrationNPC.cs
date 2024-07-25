using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministrationNPC : Npc
{
    public static AdministrationNPC Instance;
    private int UpgradeMoney;
    private void Awake()
    {
        Instance =this;
        Player = GameObject.Find("Player").gameObject;
    }

    private void Update()
    {
        Pcheck();
        Talk(this.npctype);
    }
    public void GradeUpDown(int index1, int index2)
    {
        switch (index1)
        {
            case 0:
                UpgradeMoney = 1000000;
                break;
            case 1:
                UpgradeMoney = 50000;
                break;
            case 2:
                UpgradeMoney = 10000;
                break;
            case 3:
                UpgradeMoney = 2000;
                break;
            case 4:
                UpgradeMoney = 500;
                break;
            case 5:
                UpgradeMoney = 100;
                break;             
        }   
        if (index2 >=UpgradeMoney)
        {
            PlayerPrefs.SetInt("Money", GameManager.instance.Money - UpgradeMoney);
            PlayerPrefs.SetInt("CitizonGrade", GameManager.instance.CitizonGrade +1);
        }
    }
}
