using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawn : MonoBehaviour
{
    public static Spawn instance;
    public List<int> ranx;
    public List<int> rany;
    public GameObject[] prefabs;
    public int spawncount;
    private void Awake()
    {
        instance = this;
        spawncount = 2;
    }
    private void Update()
    {
        RanSpawn();
    }
    public void Ranpos(RectInt rect)
    {
        ranx.Add((rect.x + rect.width / 2 - MapGenerator.Instance.mapSize.x / 2));
        rany.Add((rect.y + rect.height / 2 - MapGenerator.Instance.mapSize.y / 2));
    }
    public void RanSpawn()
    {
        if(spawncount==2)
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                int Randomint = Random.Range(0, ranx.Count);
                Instantiate(prefabs[i], new Vector3(ranx[Randomint], rany[Randomint], 0), Quaternion.identity);
                ranx.RemoveAt(Randomint);
                rany.RemoveAt(Randomint);
            }
            spawncount = 1;
        }
    }
}
