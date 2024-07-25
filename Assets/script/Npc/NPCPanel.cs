using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCPanel : MonoBehaviour
{
    public static NPCPanel instance;
    public Npc npc;
    public TextMeshProUGUI textms;
    public Button CloseButton;
    public Button TalkButton;
    public Button InteractButton;
    public Npc.NPCType type;
    public ShopNPC shop;
    public TownScene town;
    public AdministrationNPC adm;
    public PristNPC prist;

    private void OnEnable()
    {
        instance = this;
        npc = GetComponent<Npc>();
        shop = GameObject.Find("ShopNPC").GetComponent<ShopNPC>();
        town = GameObject.Find("TownSceneManager").GetComponent<TownScene>();
        adm = GameObject.Find("AdministrationNPC").GetComponent<AdministrationNPC>();
        prist = GameObject.Find("PristNPC").GetComponent<PristNPC>();
        CloseButton.onClick.AddListener(PanelClose);
        TalkButton.onClick.AddListener(TalkOn);
        InteractButton.onClick.AddListener(InteractOn);
    }
    public void NPCcheck(Npc.NPCType index)
    {
        switch (index)
        {
            case Npc.NPCType.ShopNPC:
                textms.text = "물건 좀 보고가시죠";
                type = Npc.NPCType.ShopNPC;
                break;
            case Npc.NPCType.TownScene:
                textms.text = "안녕하세요";
                type = Npc.NPCType.TownScene;
                break;
            case Npc.NPCType.AdministrationNPC:
                textms.text = "당신의 시민 등급은" + GameManager.instance.CitizonGrade + "입니다." + " " + "돈은 충분하십니까?";
                type = Npc.NPCType.AdministrationNPC;
                break;
            case Npc.NPCType.PristNPC:
                textms.text = "신을 믿습니까";
                type = Npc.NPCType.PristNPC;
                break;
            case Npc.NPCType.FieldNPC:
                textms.text = "정산하고가";
                type = Npc.NPCType.FieldNPC;
                break;
        }      
    }
    void PanelClose()
    {
        gameObject.SetActive(false);   
        type = Npc.NPCType.None;
    }
    void TalkOn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void InteractOn()
    {
        switch (type)
        {
            case Npc.NPCType.ShopNPC:
                shop.ShopOpen(true);
                gameObject.SetActive(false);
                break;
            case Npc.NPCType.TownScene:
                town.FieldSceneLoad(true);
                gameObject.SetActive(false);
                break;
            case Npc.NPCType.AdministrationNPC:
                AdministrationNPC.Instance.GradeUpDown(GameManager.instance.CitizonGrade, GameManager.instance.Money);
                gameObject.SetActive(false);
                break;
            case Npc.NPCType.PristNPC:
                gameObject.SetActive(false);
                break;
            case Npc.NPCType.FieldNPC:
                gameObject.SetActive(false);
                break;
        }
    }
}
