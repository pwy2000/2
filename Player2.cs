using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player2 : Unit
{
    public delegate void DeathNotify();//委托定义了一个无参数、无返回值的方法签名
    public event DeathNotify OnDeath;//声明一个事件
    public float HP = 200f;
    public float MaxHP = 200f;
    private bool isAutoFiring = false; // 新增标志位，记录是否自动开火
    public Animator ani;
    public void Update()
    {
        if (this.Death)
            return;
        if(isFlying == true)
        {
            MovePlayer();
            if (Input.GetMouseButtonDown(1)) // 检测鼠标右键按下
            {
                isAutoFiring = !isAutoFiring; // 切换自动开火状态
            }
            if (isAutoFiring && Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    public void MovePlayer()
    {
        Vector2 pos = this.transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(pos);
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.01f, 0.99f); //x 分量的值限定在 0.01f 到 0.99f 这个区间内
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.01f, 0.99f);
        pos = Camera.main.ViewportToWorldPoint(viewportPos);
        this.transform.position = pos;
    }
    public void Idle()
    {
        this.ani.SetTrigger("Idle");
    }
    public void Fly()
    {
        this.isFlying = true;
        this.ani.SetTrigger("Fly");
    }
    public void Fire()
    {
        Instantiate(bulletTemplate, transform.position, Quaternion.identity);
    }



    public void Die()//触发Ondeath事件的方法
    {
        this.Death = true;
        if (this.OnDeath != null)
        {
            this.OnDeath();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Element bullet = col.gameObject.GetComponent<Element>();
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if(bullet == null && enemy == null)
        {
            return;
        }
        Debug.Log("Player:OnTriggerEnter2D: "+ col.gameObject.name + " : "+ gameObject.name);
        if (bullet != null && bullet.side == SIDE.ENEMY)
        {
            this.HP-=bullet.damage;
            if (this.HP <= 0)
            {
                this.Die();
            }
        }
        if (enemy != null)
        {
            this.HP -= 50;
            if (this.HP <= 0)
            {
                this.Die();
            }
        }
    }

}
