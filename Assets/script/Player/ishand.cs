using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ishand : MonoBehaviour
{
    public static ishand instance;
    public Item ishanditem;
    public SpriteRenderer sprite;
    public float attackinterval;
    public float attackspeed = 0.0f;
    public bool isAttack;
    public bool isItem;
    public Vector2 boxSize;
    public Transform attackpoint;
    public GameObject bullteprefab;
    public Vector2 Nextpos;
    public Tilemap Tilemap;
    public GameObject MouseCur;
    public Player Player;
    private void Awake()
    {
        instance = this;
        MouseCur = GameObject.Find("MouseCur").gameObject;
        Player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if (isItem)
        {
            attackspeed=0;
        }
        else attackspeed += Time.deltaTime;
        if (attackspeed >= attackinterval)
        {
            isAttack = true;
        }
        else isAttack = false;
        Attack();
        if (MouseCur.transform.position.x > transform.position.x)
        {
            sprite.flipY = false;
        }
        else sprite.flipY = true;
        SaberBeam();
    }
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && isAttack && ishanditem != null)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(attackpoint.position, boxSize, 0);
            switch (ishanditem.equiptype)
            {
                case Item.EquipType.ShortDistanceWeapon:
                    if (transform.GetChild(1).gameObject.activeSelf==true)
                    {
                        foreach (Collider2D collider in collider2Ds)
                        {
                            if (collider.tag == "Enemy")
                            {
                                collider.gameObject.GetComponent<Enemy>().EnemyTakeDamage(Player.damage);
                            }
                        }
                    }
                    else return;                 
                    break;
                case Item.EquipType.LongDistanceWeapon:
                    if (Player.Crystal >= 5)
                    {
                        Nextpos = new Vector2((Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x), (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y)).normalized;
                        Instantiate(bullteprefab, attackpoint.position, transform.rotation);
                        Player.Crystal -= 5;
                    }
                    else return;
                    break;               
                case Item.EquipType.Pickax:
                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (collider.tag == "MapObject")
                        {
                            if(collider.gameObject.GetComponent<MapObject>().objtype == MapObject.MapObjectType.Rock)
                            {
                                collider.gameObject.GetComponent<MapObject>().MapObjectDameged(ishanditem.itemdamage);
                            }
                        }
                    }
                    break;
                case Item.EquipType.Digger:
                    Vector3Int selectpos = new Vector3Int(Mathf.FloorToInt(MouseCur.transform.position.x), Mathf.FloorToInt(MouseCur.transform.position.y), 0);
                    if (Tilemap.HasTile(selectpos))
                    {
                        if (Player.Crystal >= 10)
                        {
                            Player.Crystal -= 10;
                            Tilemap.SetTile(selectpos, null);
                        }
                        else return;
                    }
                    else return;
                    break;
            }
            attackspeed = 0;
        }
    }
    public void ishandItemCheck()
    {
        if (ishanditem != null)
        {
            bool exist = Inventory.Instance.inven.Exists(x => x.itemcode == ishanditem.itemcode);
            if (!exist)
            {
                ishanditem = null;
                sprite.sprite = null;
            }
        }
    }
    public void SaberBeam()
    {
        if(ishanditem != null)
        {
            if (ishanditem.itemcode == 8)
            {
                if (ishanditem.itemName == "Beam Saber")
                {
                    if (Player.Crystal > 1)
                    {
                        transform.GetChild(1).gameObject.SetActive(true);
                        Player.Crystal -= Time.deltaTime;
                    }
                }
            }
            else transform.GetChild(1).gameObject.SetActive(false);
        }
        else transform.GetChild(1).gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackpoint.position, boxSize);
    }
}
