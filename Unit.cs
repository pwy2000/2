using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public Rigidbody2D Rigidbodyenemy;

    protected bool isFlying = false; // �����Ƿ�ʼ����
    public float speed;
    public bool Death = false;
    protected Vector3 initPos;
    public GameObject bulletTemplate;
    public float nextFireTime = 0f;
    public float fireRate; // ���٣�ÿ�뷢����ӵ���
}
