using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public static MapObject instance;
    public enum MapObjectType
    {
        Wood, Rock, Forage, Etc
    } 
    public MapObjectType objtype;
    public int ObjectHealth;
    public int PrefabCount;
    public GameObject[] IngredientPrefab;

    private void Update()
    {
        if (ObjectHealth <= 0)
        {
            RockManage.instance.SoundPlay();
            StartCoroutine(ObjectDestroy());
        }
    }
    public void MapObjectDameged(int itemdamage)
    {
        switch (objtype)
        {
            case MapObjectType.Wood:
                if (GameObject.Find("IsHand").GetComponent<ishand>().ishanditem.equiptype == Item.EquipType.Pickax)
                {
                    ObjectHealth -= itemdamage;
                }
                break;
            case MapObjectType.Rock:
                if (GameObject.Find("IsHand").GetComponent<ishand>().ishanditem.equiptype == Item.EquipType.Pickax)
                {
                    RockManage.instance.RockAttacked();
                    ObjectHealth -= itemdamage;
                }
                break;
            case MapObjectType.Forage:
                if (GameObject.Find("IsHand").GetComponent<ishand>().ishanditem.equiptype == Item.EquipType.Pickax)
                {
                    ObjectHealth -= itemdamage;
                }
                break;
            case MapObjectType.Etc:
                if (GameObject.Find("IsHand").GetComponent<ishand>().ishanditem.equiptype == Item.EquipType.Pickax)
                {
                    ObjectHealth -= itemdamage;
                }
                break;
        }
    }
    IEnumerator ObjectDestroy()
    {
        if (PrefabCount > 0)
        {
            int RandomPrefabs = Random.Range(1, PrefabCount + 1);
            for (int i = 0; i < RandomPrefabs; i++)
            {
                Instantiate(IngredientPrefab[Random.Range(0, IngredientPrefab.Length)], new Vector3(transform.position.x + Random.Range(-1.0f, 2.0f), transform.position.y + Random.Range(-1.0f, 2.0f), transform.position.z), Quaternion.identity);
                PrefabCount -= RandomPrefabs;
            }
        }
        Destroy(gameObject);
        yield return null;
    }
}
