using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Element
{
    public Transform target;

    //public override void OnUpdate()
    //{
    //    if(target != null)
    //    {
    //        Vector3 dir = (target.position - this.transform.position).normalized;
    //        this.transform.position += new Vector3(speed * Time.deltaTime * direction, 0, 0); ;
    //    }
    //}
}
