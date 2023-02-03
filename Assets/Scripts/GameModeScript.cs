using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeScript : MonoBehaviour{
    // Start is called before the first frame update
    public GameObject player;
    public Text seedBoard;
    public Text scoreBoard;
    public List<GameObject> Ghosts;
    public AudioSource EatingCookiePlayer;
    public AudioSource MusicPlayer;
    public int seedScore;
    public int maxScore;
    float EatingCookieTimer = 0f;

    void Start(){
		//reset everything and initiate ghosts
        Time.timeScale = 0;
        seedScore = 0;
        GhostInit();
    }

    private void Update(){{
			//set everything back to normal when start game sound finishes playing
            if (!MusicPlayer.isPlaying){
                Time.timeScale = 1;
            }
			//pause the sound if some kind of timer hits zero
            if (EatingCookieTimer <= 0)
                EatingCookiePlayer.Pause();
            else
                EatingCookieTimer -= Time.deltaTime;
        }
        ScoreControler();
        //GhostAudioController();

		//quit game/ changes to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void GhostInit(){
        foreach (GameObject ghost in Ghosts){
            //ghost.GetComponent<GhostScript>().speed = ghostSpeed;
        }
    }

    public void EatingCookieSoundPlay(){
        EatingCookieTimer = .95f - player.GetComponent<PlayerController>().speed;
        if (!EatingCookiePlayer.isPlaying)
            EatingCookiePlayer.Play();
    }

	//set collected seed amount, score, and highscore
    void ScoreControler(){
        seedBoard.text = seedScore + " / " + maxScore;
        scoreBoard.text = (seedScore * 10).ToString();
    }
}
