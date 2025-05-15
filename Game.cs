using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject PanelReady;
    public GameObject PanelIngame;
    public GameObject PanelGameover;
    public Player player;

    public int score;
    public Text CurrentScore;//实时分数
    public Text EndScore;//结束分数
    public Text BestScore;//最高分
    public Text leaderboardText;//排行榜分数
    public int Score
    {
        get { return score; }
        set 
        { 
            this.score = value;
            this.CurrentScore.text = this.score.ToString();
            this.EndScore.text = this.score.ToString();
        }
    }  
    

    public PipelineManager pipelineManager;
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
        set { 
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
        this.player.OnDeath += Player_OnDeath;
        this.player.OnScore = OnPlayerScore;
        isGameOver = false;// 初始化标志位
    }

    void OnPlayerScore(int score)
    {
        this.Score += score;
    }
    private void Player_OnDeath()
    {
        if (isGameOver) return;// 如果游戏已经结束，直接返回
        isGameOver =true;// 设置游戏结束标志
        this.Status = GAME_STATUS.GameOver;
        this.pipelineManager.Stop();
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
        leaderboardText.text = leaderboardStr;
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
        this.PanelIngame.SetActive(this.Status == GAME_STATUS.InGame);
        this.PanelGameover.SetActive(this.Status == GAME_STATUS.GameOver);
    }

    void Update()
    {

    }
    public void StartGame()
    {
        this.Status = GAME_STATUS.InGame;
        Debug.LogFormat("StartGame : {0}",this.status);
        
        pipelineManager.StartRun();
        player.Fly();
        isGameOver = false; // 开始新游戏时重置标志位
    }
    public void Restart()
    {
        this.Status = GAME_STATUS.Ready;
        this.pipelineManager.Init();
        this.player.Init();
        this.Score = 0;
        isGameOver = false; // 重新开始游戏时重置标志位
    }
}
