using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mining : MonoBehaviour
{
    public float maxhealth;
    public float health;
    public float maxshieldhealth;
    public float shieldhealth;
    public bool isshield = true;
    public int itemdamage;
    public float shieldcountdown;

    private void Start()
    {
        itemdamage = GetComponent<ItemPickup>().item.itemdamage;
        health = maxhealth;
        shieldhealth = maxshieldhealth;
    }

    private void Update()
    {
        ShieldRestore();
        if (shieldhealth > 0)
        {
            isshield = true;
        }
        else if (shieldhealth <= 0)
        {
            isshield = false;
            shieldhealth = 0;
        }
        if(health <= 0)
        {
            Debug.Log("ä�����ı�");
            Destroy(gameObject);
        }
    }
    //�� �Ʒ��� �浹������ Recrange���� ���� ��ũ��Ʈ ����� �����ؾ� ��. mining���� rigid�ٵ�� boxcollider�� ������ �޴� �浹ó���ؾ���

    public void MachineDamaged(float Enemydamage)
    {
        if (isshield)
        {
            shieldhealth -= Enemydamage;
        }
        else if(!isshield)
        {
            health -= Enemydamage;
        }
        shieldcountdown = 0;
    }
    public void ShieldRestore()
    {
        shieldcountdown += Time.deltaTime;
        if(shieldcountdown > 5.0f)
        {
            isshield = true;
            shieldhealth = maxshieldhealth;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Warning");
        }
    }
}
