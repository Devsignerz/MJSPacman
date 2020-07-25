using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject readyText;
    public GameObject alarmAudio;
    public Text seedBoard;
    public Text scoreBoard;
    public Text highscoreBoard;
    public List<GameObject> Ghosts;
    public AudioSource EatingCookiePlayer;
    public AudioSource MuiscPlayer;
    public int seedScore = 0;
    public int maxScore = 0;
    float EatingCookieTimer = 0f;
    float AlarmAudioTimer = 0f;
    int GhostAlarmTurn = 0;
    bool ghostAudio = false;


    float ghostSpeed = .3f;

    void Start()
    {
        Time.timeScale = 0;
        seedScore = 0;
        GhostInit();
    }

    private void Update()
    {
        {
            if (!MuiscPlayer.isPlaying)
            {
                Destroy(readyText);
                ghostAudio = true;
                Time.timeScale = 1;
            }
            if (EatingCookieTimer <= 0)
                EatingCookiePlayer.Pause();
            else
                EatingCookieTimer -= Time.deltaTime;
        }
        ScoreControler();
        GhostAudioController();
    }

    void GhostInit()
    {
        foreach (GameObject ghost in Ghosts)
        {
            ghost.GetComponent<GhostScript>().speed = ghostSpeed;
        }
    }

    public void EatingCookieSoundPlay()
    {
        EatingCookieTimer = .95f - player.GetComponent<PlayerForward>().speed;
        if (!EatingCookiePlayer.isPlaying)
            EatingCookiePlayer.Play();
    }

    void ScoreControler()
    {
        seedBoard.text = seedScore + " / " + maxScore;
        scoreBoard.text = (seedScore * 10).ToString();
        highscoreBoard.text = PlayerPrefs.GetInt("HighScore").ToString();
        if (seedScore * 10 > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", seedScore * 10);
    }

    void GhostAudioController()
    {
        if (AlarmAudioTimer <= 0)
        {
            ++GhostAlarmTurn;
            GhostAlarmTurn %= Ghosts.Count;
        }
        else
            AlarmAudioTimer -= Time.deltaTime;



    }
}
