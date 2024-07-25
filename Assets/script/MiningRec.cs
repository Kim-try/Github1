using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class MiningRec : MonoBehaviour
{
    public int machinedamage;
    public float MiningCountDown;
    public GameObject MiningMachine;
    public GameObject MiningNoise;
    public bool IsMining = false;
    public MapObject[] Mapobjects;
    private void Update()
    {
        MiningCountDown += Time.deltaTime;
        machinedamage = MiningMachine.GetComponent<Mining>().itemdamage;
        if (IsMining)
        {
            MiningNoise.SetActive(true);
        }
        else MiningNoise.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapObject"))//이거 인식되는 오브젝트들을 전부 리스트로 받을까?
        {
            IsMining = true;
            if (MiningCountDown > 60.0f)
            {
                collision.gameObject.GetComponent<MapObject>().MapObjectDameged(machinedamage);
                MiningCountDown = 0.0f;
                Debug.Log("충돌중");
            }
        }
        //if (collision.gameObject.CompareTag("Item")) 아이템 드랍되면 자동줍기 하는건 나중에
    }
}
