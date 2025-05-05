using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Game2 : MonoBehaviour
{
    public GameObject PanelReady;
    public GameObject PanelInGame;
    public GameObject PanelGameOver;
    public Player2 Player2;
    public Enemy enemy;
    public EnemyManager enemyManager;
    public Slider HPBar;

    public int score;
    public Text CurrentScore;
    public Text EndScore;
    public Text BestScore;
    public Text LeaderBoardText;
    public int KillScore
    {
        get { return score; }
        set
        {
            this.score = value;
            this.CurrentScore.text = this.score.ToString();
            this.EndScore.text = this.score.ToString();
        }
    }

    
    enum GAME_STATUS
    {
        Ready,
        InGame,
        GameOver
    }
    GAME_STATUS status;
    private GAME_STATUS Status
    {
        get { return status; }
        set
        {
            this.status = value;
            this.UpdateUI();
        }
    }
    private List<int> leaderboard = new List<int>();
    private bool isGameOver; // 新增标志位
    void Start()
    {
        this.PanelReady.SetActive(true);
        this.Status = GAME_STATUS.Ready;
        this.Player2.OnDeath += Player2_OnDeath;

        isGameOver = false;// 初始化标志位

    }

    public void OnKillScore(int score)
    {
        this.KillScore += score;
    }
    private void Player2_OnDeath()
    {
        if (isGameOver) return;// 如果游戏已经结束，直接返回
        isGameOver = true;// 设置游戏结束标志
        this.Status = GAME_STATUS.GameOver;
        enemyManager.Stop();

        AddScoreToLeaderboard(score);
        UpdateLeaderboardUI();
        UpdateHighScoreUI();
    }
    private void AddScoreToLeaderboard(int score)
    {
        leaderboard.Add(score);
        leaderboard = leaderboard.OrderByDescending(s => s).ToList();
        if (leaderboard.Count > 5)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1);
        }
    }
    private void UpdateLeaderboardUI()
    {
        string leaderboardStr = "排行榜:\n";
        for (int i = 0; i < leaderboard.Count; i++)
        {
            leaderboardStr += $"{i + 1}. {leaderboard[i]}\n";
        }
        LeaderBoardText.text = leaderboardStr;
    }
    private void UpdateHighScoreUI()
    {
        if (leaderboard.Count > 0)
        {
            int highScore = leaderboard[0];
            BestScore.text = $"历史最高分: {highScore}";
        }
        else
        {
            BestScore.text = "历史最高分: 0";
        }
    }
    public void UpdateUI()
    {
        this.PanelReady.SetActive(this.Status == GAME_STATUS.Ready);
        this.PanelInGame.SetActive(this.Status == GAME_STATUS.InGame);
        this.PanelGameOver.SetActive(this.Status == GAME_STATUS.GameOver);
    }

    void Update()
    {
        this.HPBar.value = Mathf.Lerp(this.HPBar.value,this.Player2.HP,0.1f);
    }
    public void StartGame()
    {
        isGameOver = false; // 开始新游戏时重置标志位
        this.Status = GAME_STATUS.InGame;
        this.Player2.HP=Player2.MaxHP; 
        this.KillScore = 0;
        Debug.LogFormat("StartGame : {0}", this.status);
        this.Player2.Death = false;
        enemyManager.Begin();
        Player2.Fly();
        Player2.Fire();
    }
    public void ReStart()
    {
        this.Status = GAME_STATUS.Ready;
        Player2.transform.position = new Vector3(-6, 0, 0);
        Player2.Idle();
    }

}
