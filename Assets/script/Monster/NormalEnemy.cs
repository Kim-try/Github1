using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    protected override void Patrol()
    {
        base.Patrol();
        if (!isRec)
        {
            StartCoroutine(EnemyMovement());
        }
        else if (isRec)
        {
            StopCoroutine(EnemyMovement());
            Enemyrb.velocity = targetpos*2;
        }
    }
}
