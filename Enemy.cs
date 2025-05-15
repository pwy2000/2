using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit
{
    public delegate void DeathNotify();
    public event DeathNotify OnDeath;
    public Animator enemy_ani;
    public Vector2 range;
    public ENEMY_TYPE enemyType;
    private SpriteRenderer enemyBirdSpriteRenderer;
    private SpriteRenderer blueWingSpriteRenderer;
    float initY = 0;
    void Start()
    {
        this.Enemy_Fly();
        this.enemy_ani = GetComponent<Animator>();
        initPos = transform.position;
        initY = Random.Range(range.x, range.y);
        this.transform.localPosition = new Vector3(3, initY, 0);
        Transform enemyBirdTransform = transform.Find("Enemybird");//获取Enemy子项的模块
        enemyBirdSpriteRenderer = enemyBirdTransform.GetComponent<SpriteRenderer>();
        Transform blueWingTransform = enemyBirdTransform.Find("Bluewing");
        blueWingSpriteRenderer = blueWingTransform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (isFlying == true)
        {
            MoveEnemy();
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }
    public void MoveEnemy()
    {
        float y = 0;
        if (this.enemyType == ENEMY_TYPE.SWING_ENEMY)
        {
           y = Mathf.Sin(Time.time * 0.5f)* 2f;
        }
        this.transform.position = new Vector3(this.transform.position.x - Time.deltaTime * speed,initY + y); // 敌人移动方向

        Vector2 viewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (viewportPos.x < 0f)
        {
            Destroy(this.gameObject);
        }
    }
    public void Enemy_Fly()
    {
        this.isFlying = true;
        this.enemy_ani.SetTrigger("Enemy_Fly");
    }
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletTemplate, transform.position, Quaternion.identity);
        bullet.GetComponent<Element>().direction = -1; // 子弹发射方向
    }

    public void Enemy_Die()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;//避免敌人触发死亡后的持续碰撞
        }
        // 改变 Enemybird 的颜色为红色
        enemyBirdSpriteRenderer.color = Color.red;
        // 改变 Bluewing 的颜色为红色
        blueWingSpriteRenderer.color = Color.red;
        this.enemy_ani.SetTrigger("Enemy_Die");
        if (this.OnDeath != null)
        {
            OnDeath();
        }
        Destroy(this.gameObject, 0.3f);
    }

    void OnTriggerEnter2D(Collider2D col) // 被玩家子弹击中以及被撞击
    {
        Player2 player2 = col.gameObject.GetComponent<Player2>();
        Element bullet = col.gameObject.GetComponent<Element>();
        if (bullet == null && player2 == null)
        {
            return;
        }
        Debug.Log("Enemy:OnTriggerEnter2D: " + col.gameObject.name + " : " + gameObject.name);
        if (bullet != null && bullet.side == SIDE.PLAYER || player2 != null)
        {
            this.Enemy_Die();
        }
    }
}