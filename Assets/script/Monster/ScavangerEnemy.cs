using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerEnemy : Enemy
{
    private int SCMoney=100;
    private bool isAggresive = false;
    private GameObject Scbullet;
    public int citizongrade;
    // Player의 인벤토리 안에 있는 아이템의 금액이 얼마인지 계산해서 특정수치를 넘으면 기습 그 전까지는 그냥 넘어감. 오히려 아이템 나눠줌
    private new void Update()
    {
        if (Player.GetComponent<Player>().MazeMoney > SCMoney && GameManager.instance.CitizonGrade<= citizongrade)
        {
            isAggresive=true;
        }
        else isAggresive=false;
    }
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
            if (isAggresive)
            {
                if (targetpos.magnitude <= 1.0f)
                {
                    Instantiate(Scbullet, transform.position, Quaternion.identity);
                }
                //player한테 총알쏘기
            }
            if(GameManager.instance.CitizonGrade > citizongrade)
            {
                Enemyrb.velocity = -targetpos;
            }
            else return;
        }      
    }

}
