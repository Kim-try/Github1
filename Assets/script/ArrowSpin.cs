using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpin : MonoBehaviour
{
    public GameObject Target;
    public GameObject Player;

    private void OnEnable()
    {
        Player = GameObject.Find("Player").gameObject;
        Target = GameObject.Find("stairs_down(Clone)").gameObject;
    }

    private void Update()
    {
        Vector2 Tar = Target.transform.position - Player.transform.position;
        float ta = Mathf.Atan2(Tar.y, Tar.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, ta - 90);
    }

}
