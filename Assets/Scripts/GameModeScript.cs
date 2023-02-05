using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeScript : MonoBehaviour{
    // Start is called before the first frame update
    public GameObject player;
    public Text seedBoard;
    public Text scoreBoard;
    public List<GameObject> Ghosts;
    public AudioClip EatingCookiePlayer;
    public AudioClip MusicPlayer;
    public int seedScore;
    public int maxScore;
    float EatingCookieTimer = 0f;

    void Start(){
		//reset everything and initiate ghosts
		GetComponent<AudioSource>().clip = MusicPlayer;
        Time.timeScale = 0;
        seedScore = 0;
        GhostInit();
    }

    private void Update(){{
			//set everything back to normal when start game sound finishes playing
            if (!GetComponent<AudioSource>().isPlaying){
                Time.timeScale = 1;
            }
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
		GetComponent<AudioSource>().clip = EatingCookiePlayer;
		GetComponent<AudioSource>().Play();
    }

	//set collected seed amount, score, and highscore
    void ScoreControler(){
        seedBoard.text = seedScore + " / " + maxScore;
        scoreBoard.text = (seedScore * 10).ToString();
    }
}
