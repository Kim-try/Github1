using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bullet : MonoBehaviour
{
    public Vector3 Nextpos;
    public Rigidbody2D rb;
    public float bullettime;
    public ishand ishand;
    public AudioSource bulletaudioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ishand = GameObject.Find("Hand").transform.GetChild(0).GetComponent<ishand>();
        bulletaudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        bullettime += Time.deltaTime;
        if (bullettime >+ 2.0f)
        {
            Destroy(gameObject);
        }
        rb.velocity = ishand.Nextpos * 8;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().EnemyTakeDamage(Player.instance.damage);
        }
    }
}
