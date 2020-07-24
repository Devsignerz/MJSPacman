using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Text scoreBoard;
    public List<GameObject> Ghosts;
    public AudioSource EatingCookiePlayer;
    public int seedScore = 0;
    public int maxScore = 0;
    float EatingCookieTimer = 0f;


    float ghostSpeed = .3f;

    void Start()
    {
        seedScore = 0;
        GhostInit();
    }

    private void Update()
    {
        debugToggle();
        Debug.Log(EatingCookieTimer);
        scoreBoard.text = seedScore + " / " + maxScore;
        if (EatingCookieTimer <= 0)
            EatingCookiePlayer.Pause();
        else
            EatingCookieTimer -= Time.deltaTime;
    }

    void debugToggle()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            GhostScript GS = Ghosts[0].GetComponent<GhostScript>();
            if (GS.debugVisual)
                GS.debugVisual = false;
            else
                GS.debugVisual = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            GhostScript GS = Ghosts[1].GetComponent<GhostScript>();
            if (GS.debugVisual)
                GS.debugVisual = false;
            else
                GS.debugVisual = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            GhostScript GS = Ghosts[2].GetComponent<GhostScript>();
            if (GS.debugVisual)
                GS.debugVisual = false;
            else
                GS.debugVisual = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            GhostScript GS = Ghosts[3].GetComponent<GhostScript>();
            if (GS.debugVisual)
                GS.debugVisual = false;
            else
                GS.debugVisual = true;
        }

    }

    void GhostInit()
    {
        foreach (GameObject ghost in Ghosts)
        {
            ghost.GetComponent<GhostScript>().speed = ghostSpeed;
        }
    }

    int clipCount = 0;
    public void EatingCookieSoundPlay()
    {
        EatingCookieTimer = 1f - player.GetComponent<PlayerForward>().speed;
        if (!EatingCookiePlayer.isPlaying)
            EatingCookiePlayer.Play();
    }
}
