using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player2 : Unit
{
    public delegate void DeathNotify();//ί�ж�����һ���޲������޷���ֵ�ķ���ǩ��
    public event DeathNotify OnDeath;//����һ���¼�
    public float HP = 200f;
    public float MaxHP = 200f;
    private bool isAutoFiring = false; // ������־λ����¼�Ƿ��Զ�����
    public Animator ani;
    public void Update()
    {
        if (this.Death)
            return;
        if(isFlying == true)
        {
            MovePlayer();
            if (Input.GetMouseButtonDown(1)) // �������Ҽ�����
            {
                isAutoFiring = !isAutoFiring; // �л��Զ�����״̬
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
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.01f, 0.99f); //x ������ֵ�޶��� 0.01f �� 0.99f ���������
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



    public void Die()//����Ondeath�¼��ķ���
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
