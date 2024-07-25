using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float sense, speed, Maxhealth, health, damage, stamina, defense;
    public float MaxWeight = 100.0f;
    public float CurWeight;
    public bool isHide;
    public float isRIght = 1;
    Animator anim;
    Rigidbody2D rb;
    CapsuleCollider2D capcol;
    public int MazeMoney;
    public bool isDead = false;
    public GameObject MouseCur;
    public float Crystal;   
    private void Awake()
    {
        CurWeight = 0.0f;
        var objs = FindObjectsOfType<Player>();
        if(objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        capcol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        Crystal = 10;
        health = Maxhealth;
        stamina = 100f; 
        isHide = false;
    }
    private void Update()
    {
        if (MouseCur.transform.position.x > transform.position.x)
        {
            isRIght = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (MouseCur.transform.position.x < transform.position.x)
        {
            isRIght = -1;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        PlayerDead();
        if(SceneManager.GetActiveScene().name == "MazeScene")
        {
            RanSp();
        }
    }
    private void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(hor, ver) * speed;
        if (rb.velocity.normalized.x == 0 && rb.velocity.normalized.y==0)
        {
            anim.SetBool("isWalking", false);
        }
        else anim.SetBool("isWalking", true);
    }
    public void TakeDamage(float Enemydamage)
    {
        health -= Enemydamage;
    }
    public void MoneyCount()
    {
        MazeMoney = 0;
        for (int i = 0; i < Inventory.Instance.inven.Count; i++)
        {
            MazeMoney += Inventory.Instance.inven[i].itemmoney * Inventory.Instance.slots[i].slotitemcount;
        }
    }
    public void WeightCheck()
    {
        CurWeight = 0;
        for (int i = 0; i< Inventory.Instance.inven.Count; i++)
        {
            CurWeight += Inventory.Instance.inven[i].itemweight * Inventory.Instance.slots[i].slotitemcount;
        }
        if (CurWeight / MaxWeight >= 0.2f && CurWeight / MaxWeight < 0.5f)
        {
            speed = speed * 0.8f;
        }
        else if (CurWeight / MaxWeight >= 0.5f && CurWeight / MaxWeight < 0.8f)
        {
            speed = speed * 0.6f;
        }
        else if (CurWeight / MaxWeight >= 0.8f)
        {
            speed = speed * 0.2f;
        }
        else speed = 5;
    }
    public void PlayerDead()
    {
        if (SceneManager.GetActiveScene().name == "MazeScene")
        {
            if (health <= 0)
            {
                isDead = true;
                GameObject.Find("MazeCanvas").transform.Find("Game Over Panel").gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                isDead = false;
                GameObject.Find("MazeCanvas").transform.Find("Game Over Panel").gameObject.SetActive(false);
            }
        }
        else return;
    }      
    public void RanSp()
    {
        if (Spawn.instance.spawncount==1)
        {
            transform.position = new Vector3(Spawn.instance.ranx[0], Spawn.instance.rany[0],0);
            Spawn.instance.spawncount = 0;
        }
    }
}
