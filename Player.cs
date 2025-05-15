using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodybird;
    public float force;
    public Animator ani;
    private bool isFlying = false; //控制是否开始飞行

    public float speed;
    private bool Death = false;

    public delegate void DeathNotify();

    public event DeathNotify OnDeath;
    private Vector3 initPos;
    public UnityAction<int> OnScore;
    private bool hasScored = false; // 新增状态标记
    void Start()
    {
        this.ani = this.GetComponent<Animator>();
        this.Idle();
        initPos = this.transform.position;
    }

    public void Init()
    {
        this.transform.position = initPos;
        this.Idle();
        this.Death = false;
        this.hasScored = false; // 重置得分标记
    }
    // Update is called once per frame
    void Update()
    {
       if(this.Death)
            return;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            rigidbodybird.velocity = Vector2.zero;
            rigidbodybird.AddForce(new Vector2(0, force), ForceMode2D.Force);
       }
    }

        public void Idle()
        {
          this.rigidbodybird.Sleep();
          this.ani.SetTrigger("Idle");
          this.rigidbodybird.simulated = false;
        }
        public void Fly()
        {
          this.rigidbodybird.WakeUp();
          this.ani.SetTrigger("Fly");
          this.rigidbodybird.simulated = true;
    }

    public void Die()
    {
        this.Death = true;
        if(this.OnDeath != null)
        {
            this.OnDeath();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D:" + col.gameObject.name + " : " + gameObject.name + " : " + Time.time); ;
        if(col.gameObject.name.Equals("Square"))
        {

        }
        else
        this.Die();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2D: " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if(col.gameObject.name.Equals("ScoreArea"))
        {
            hasScored = false; // 进入得分区域，重置得分标记
        }
        else
            this.Die();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("OnTriggerExit2D: " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.name.Equals("ScoreArea") && !hasScored)
        {
            if (this.OnScore != null)
                this.OnScore(1);
        }
        hasScored = true; // 标记为已得分
    }
}

