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
            Debug.Log("채굴기파괴");
            Destroy(gameObject);
        }
    }
    //이 아래의 충돌판정은 Recrange에서 새로 스크립트 만들고 적용해야 함. mining에선 rigid바디랑 boxcollider로 데미지 받는 충돌처리해야함

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
