using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Element :MonoBehaviour
{
    public int speed;
    public int direction = 1;
    public SIDE side;
    public float damage;
    public void Start()
    {

    }
    void Update()
    {
        this.transform.position += new Vector3(speed * Time.deltaTime*direction,0,0);
        CheckIfOffScreen();       
    }
    void CheckIfOffScreen()
    {
        // ���ӵ�����������ת��Ϊ�ӿ�����
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // ����ӵ��Ƿ񳬳���Ļ�߽�
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            // ��������߽磬�����ӵ�
            Destroy(gameObject);
        }
    }
}
