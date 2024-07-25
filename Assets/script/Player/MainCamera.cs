using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject target;
    public Vector2 center, size;
    float height, width;
    private void Awake()
    {
        size.x = 140;
        size.y = 140;
        var objs = FindObjectsOfType<MainCamera>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        target = GameObject.Find("Player").gameObject;
    }
    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height *Screen.width/Screen.height;
    }
    private void LateUpdate()
    {
        transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -10f);
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);
        transform.position = new Vector3(clampX, clampY , -10f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
