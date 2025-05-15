using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject missileTemplate;
    public Transform firepoint1;
    public Transform firepoint2;

    GameObject missile = null;
    void Start()
    {
        this.enemy_ani = GetComponent<Animator>();
        this.Boss_Fly();
        StartCoroutine(FireMissile());
    }

    private void Boss_Fly()
    {
        this.enemy_ani.SetTrigger("Boss_Fly");
    }

    IEnumerator FireMissile()
    {
        yield return new WaitForSeconds(5f);
        enemy_ani.SetTrigger("Boss_Fire");
    }
    //public void OnMissleLoad()
    //{
    //    Debug.Log("OnMissileLoad");
    //    GameObject go = Instantiate(missileTemplate,firepoint1);
    //    missile = go.GetComponent<Missile>();
    //    missile.target = this.target.transform;
    //}
    //public void OnMissilelaunch()
    //{
    //    Debug.Log("OnMissileLaunch");
    //    if (missile != null)
    //    {
    //        return;
    //    }
    //    missile.transform.SetParent(null);
    //    missile.Launch();
    //}
    //public override void OnUpdate()
    //{
    //    if(target!= null)
    //    {
    //        Vector3 dir = (TargetJoint2D.transform.position- BatteryStatus.position).normalized;
    //        BatteryStatus.transform.rotation = Quaternion.FromToRotation(Vector3.left,dir);
    //    }
    //}
}
