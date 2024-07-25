using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PristNPC : Npc
{
    public static PristNPC Instance;
    private void Awake()
    {
        Instance = this;
        Player = GameObject.Find("Player").gameObject;
    }
    private void Update()
    {
        Pcheck();
        Talk(this.npctype);
    }
}
