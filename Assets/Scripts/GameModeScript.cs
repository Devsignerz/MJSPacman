using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public List<GameObject> Ghosts;
    public int seedScore = 0;

    float ghostSpeed = .3f;

    void Start()
    {
        seedScore = 0;
        GhostInit();
    }

    private void Update()
    {
        debugToggle();
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
}
