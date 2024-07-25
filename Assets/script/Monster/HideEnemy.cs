using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HideEnemy : Enemy
{
    [SerializeField] private SpriteRenderer hidesp;
    [SerializeField] private Sprite[] images;
    private new void Awake()
    {
        hidesp = GetComponent<SpriteRenderer>();
    }
    protected override void Patrol()
    {
        base.Patrol();
        if (!isRec)
        {
            StartCoroutine(EnemyMovement());
            hidesp.sprite = images[0];
        }
        else if (isRec)
        {
            StopCoroutine(EnemyMovement());
            hidesp.sprite = images[Random.Range(1, images.Length)];
            if(targetpos.magnitude <= 0.5f)
            {
                Enemyrb.velocity = targetpos;
            }
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
