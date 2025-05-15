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
    public Text CurrentScore;//ʵʱ����
    public Text EndScore;//��������
    public Text BestScore;//��߷�
    public Text leaderboardText;//���а����
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
    private bool isGameOver; // ������־λ
    void Start()
    {
        this.PanelReady.SetActive(true);
        this.Status = GAME_STATUS.Ready;
        this.player.OnDeath += Player_OnDeath;
        this.player.OnScore = OnPlayerScore;
        isGameOver = false;// ��ʼ����־λ
    }

    void OnPlayerScore(int score)
    {
        this.Score += score;
    }
    private void Player_OnDeath()
    {
        if (isGameOver) return;// �����Ϸ�Ѿ�������ֱ�ӷ���
        isGameOver =true;// ������Ϸ������־
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
        string leaderboardStr = "���а�:\n";
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
            BestScore.text = $"��ʷ��߷�: {highScore}";
        }
        else
        {
            BestScore.text = "��ʷ��߷�: 0";
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
        isGameOver = false; // ��ʼ����Ϸʱ���ñ�־λ
    }
    public void Restart()
    {
        this.Status = GAME_STATUS.Ready;
        this.pipelineManager.Init();
        this.player.Init();
        this.Score = 0;
        isGameOver = false; // ���¿�ʼ��Ϸʱ���ñ�־λ
    }
}
