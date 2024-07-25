using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavangerEnemy : Enemy
{
    private int SCMoney=100;
    private bool isAggresive = false;
    private GameObject Scbullet;
    public int citizongrade;
    // Player�� �κ��丮 �ȿ� �ִ� �������� �ݾ��� ������ ����ؼ� Ư����ġ�� ������ ��� �� �������� �׳� �Ѿ. ������ ������ ������
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
                //player���� �Ѿ˽��
            }
            if(GameManager.instance.CitizonGrade > citizongrade)
            {
                Enemyrb.velocity = -targetpos;
            }
            else return;
        }      
    }

}
