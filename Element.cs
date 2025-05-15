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
        // 将子弹的世界坐标转换为视口坐标
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // 检查子弹是否超出屏幕边界
        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            // 如果超出边界，销毁子弹
            Destroy(gameObject);
        }
    }
}
