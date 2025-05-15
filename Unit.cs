using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public Rigidbody2D Rigidbodyenemy;

    protected bool isFlying = false; // 控制是否开始飞行
    public float speed;
    public bool Death = false;
    protected Vector3 initPos;
    public GameObject bulletTemplate;
    public float nextFireTime = 0f;
    public float fireRate; // 射速，每秒发射的子弹数
}
