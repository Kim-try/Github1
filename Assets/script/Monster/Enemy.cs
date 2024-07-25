using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Enemy : MonoBehaviour
{
    public float sense, speed, Maxhealth, health, Enemydamage;
    public bool isRec;
    public float attackspeed;
    public float attackinterval;
    public float randommovetimer;
    public bool isAttack;
    public Vector3 targetpos;
    public GameObject Player;
    public Rigidbody2D Enemyrb;
    public Vector2 randompos;
    public float rancount = 0;
    public Tilemap wall;
    public TileBase wallTile;
    public AudioSource EnemyAudio;
    public AudioClip[] audioClip;
    public bool isDead = false;
    public Animator Enemyanim;

    protected void Awake()
    {
        Enemyanim = GetComponent<Animator>();
        EnemyAudio = GetComponent<AudioSource>();
        Enemyrb = GetComponent<Rigidbody2D>();
        wall = GameObject.Find("TileMap").GetComponent<Tilemap>();
    }
    protected void Start()
    {
        Player = GameObject.Find("Player").gameObject;
        health = Maxhealth;
    }
    protected void Update()
    {
        rancount += Time.deltaTime;
        targetpos = Player.transform.position - transform.position;
        if (targetpos.magnitude <= 3.0f)
        {
            isRec = true;
        }
        else isRec = false;
        AttackCoolTIme();
        if (health <= 0)
        {
            isDead = true;
        }
        StartCoroutine(EnemyDie());
    }
    protected void FixedUpdate()
    {
        Patrol();
    }
    protected virtual void Patrol()
    {     
    }
    protected virtual void AttackCoolTIme()
    {
        attackspeed += Time.deltaTime;
        if (attackspeed >= attackinterval)
        {
            isAttack = true;
        }
        else isAttack = false;
    }//수치를 자율변경
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isRec)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Weapon")
            {
                isRec = true;
            }
        }
        else if (isRec)
        {
            if (collision.gameObject.tag == "Player" && isAttack)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(Enemydamage);
                Debug.Log("Damage넣음");
            }
            else return;
        }
    }
    protected IEnumerator EnemyMovement()
    {
        Invoke("Soundplay", 1.5f);
        if (rancount> randommovetimer)//randommovetimer가 몇초에 한번 움직이는지를 결정
        {
            randompos = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            Vector3Int ranVec = new Vector3Int(Mathf.FloorToInt(transform.position.x+ Random.Range(-1, 2)), Mathf.FloorToInt(transform.position.y+ Random.Range(-1, 2)), Mathf.FloorToInt(transform.position.z));
            if(wall.HasTile(ranVec) && (wall.GetTile(ranVec) == wallTile))
            {
                Enemyrb.velocity = randompos * speed;
                rancount = 0;
            }          
            else rancount = randommovetimer;
        }
        if (randompos.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (randompos.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (randompos.x == 0)
        {
            transform.eulerAngles = transform.eulerAngles;
        }
        if (rancount >= 2)
        {
            Enemyrb.velocity = Vector2.zero;
        }
        yield return null;
    }
    public void EnemyTakeDamage(float damage)
    {
        health -= damage;
    }
    protected void Soundplay()
    {
        EnemyAudio.clip = audioClip[0];
        EnemyAudio.Play();
    }
    protected IEnumerator EnemyDie()
    {
        if(isDead)
        {
            EnemyAudio.clip = audioClip[1];
            EnemyAudio.Play();
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
            yield return null;
        }
    }
}

