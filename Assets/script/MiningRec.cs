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
        if (collision.gameObject.CompareTag("MapObject"))//�̰� �νĵǴ� ������Ʈ���� ���� ����Ʈ�� ������?
        {
            IsMining = true;
            if (MiningCountDown > 60.0f)
            {
                collision.gameObject.GetComponent<MapObject>().MapObjectDameged(machinedamage);
                MiningCountDown = 0.0f;
                Debug.Log("�浹��");
            }
        }
        //if (collision.gameObject.CompareTag("Item")) ������ ����Ǹ� �ڵ��ݱ� �ϴ°� ���߿�
    }
}
