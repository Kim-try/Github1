using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saber : MonoBehaviour
{
    public static saber instance;
    public AudioSource audioSource;
    public Animator anim;
    private void OnEnable()
    {
        anim.SetBool("isAttack", false);
        instance = this;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        CrystalCheck();
        attack();
    }
    public void CrystalCheck()
    {
        if(Player.instance.Crystal > 1)
        {
            audioSource.Play();
        }
        else audioSource.Stop();
    }
    public void attack()
    {
        if (Input.GetMouseButtonDown(0) && ishand.instance.isAttack)
        {
            anim.SetBool("isAttack", true);
            Invoke("off", 0.4f);
        }
    }
    public void off()
    {
        anim.SetBool("isAttack", false);
    }
}
