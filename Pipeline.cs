using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
   public Vector3 speed;
   public Vector2 range;

    void Start()
    {
        this.Init();
    }

    float t = 0;
    public void Init()
    {
        float y = Random.Range(range.x, range.y);
        this.transform.localPosition = new Vector3(3, y, 0);
    }



    void Update()
    {
        this.transform.position += speed * Time.deltaTime;
        t += Time.deltaTime;
        if (t > 9f)
        {
            t = 0;
            this.Init();
        }
        
    }
}
