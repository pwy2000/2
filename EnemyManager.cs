using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject BOSS;

    public float interval;
    public float interval_2 = 3;
    public float interval_3 = 5;
    private float interval_4 = 10;
    public Game2 game2; // 新增对 Game2 的引用
    List<Enemy> Enemies = new List<Enemy>();
    void Start()
    {

    }

    Coroutine runner = null;

    public void Begin()
    {
        if (runner != null)
        {
            StopCoroutine(runner);
            runner = null;
        }
        runner = StartCoroutine(GenetateEnemies());
    }
    public void Stop()
    {
        StopCoroutine(runner);
        runner = null;
        ClearEnemies();
    }

    int timer1 = 0;
    int timer2 = 0;
    int timer3 = 0;
    int timer4 = 0;
    

    IEnumerator GenetateEnemies()
    {
        while (true)
        {
            if (timer1 > interval)
            {
                CreateEnemies(enemy);
                timer1 = 0;
            }
            if (timer2 > interval_2)
            {
                CreateEnemies(enemy2);
                timer2 = 0;
            }
            if (timer3 > interval_3)
            {
                CreateEnemies(enemy3);
                timer3 = 0;
            }
            if (timer4 > interval_4)
            {
                CreateEnemies(BOSS);
                timer4 = 0;
            }
            timer1++;
            timer2++;
            timer3++;
            timer4++;
            yield return new WaitForSeconds(1f);
        }
    }
    void ClearEnemies()
    {
        foreach (Enemy enemyInstance in Enemies)
        {
            if (enemyInstance != null)
            {
                Destroy(enemyInstance.gameObject);
            }
        }
        Enemies.Clear();
    }
    void CreateEnemies(GameObject Enemies)
    {
        if (Enemies == null) {
            return;
        }
        GameObject obj = Instantiate(Enemies,this.transform);
        Enemy E = obj.GetComponent<Enemy>();
        this.Enemies.Add(E);

//每当一个 Enemy 对象死亡时，OnDeath 事件就会被触发，进而调用 Game2 类的 OnKillScore 方法，让分数增加 1
         E.OnDeath += () =>
         {
            if (game2 != null)
            {
              game2.OnKillScore(1);
            }
         };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
