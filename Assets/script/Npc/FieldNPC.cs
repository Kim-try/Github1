using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldNPC : Npc
{
    public static FieldNPC Instance;
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
