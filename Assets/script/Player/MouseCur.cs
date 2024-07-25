using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCur : MonoBehaviour
{
    public static MouseCur instance;
    public SpriteRenderer mousesprite;
    public Sprite sprite1;
    public Sprite sprite2;
    public ishand ishand;
    private void Awake()
    {
        instance = this;
        ishand = GameObject.Find("IsHand").GetComponent<ishand>();
    }

    private void Start()
    {
        mousesprite.sprite = sprite1;
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (ishand.ishanditem!=null && ishand.ishanditem.equiptype == Item.EquipType.Digger)
        {
            mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
            mousesprite.sprite = sprite1;
        }
        else
        {
            mousePosition = new Vector2((mousePosition.x), (mousePosition.y));
            mousesprite.sprite = sprite2;
        }
        transform.position = mousePosition;
    }
}
