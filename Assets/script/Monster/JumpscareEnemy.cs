using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class JumpscareEnemy : Enemy
{
    public bool Lighton = false;
    //코일헤드같은 타입. 불이 켜지면 기습

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
            Enemyrb.velocity = targetpos;
        }
    }
    private void LightCheck()
    {
        if (GameObject.Find("Global Light 2D").GetComponent<Light2D>().intensity != 0.1f)
        {
            Lighton = true;
        }
        else Lighton = false;
    }
}
