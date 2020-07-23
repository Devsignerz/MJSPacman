using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public List<GameObject> Ghosts;
    public int seedScore = 0;

    float ghostSpeed = 3f;

    void Start()
    {
        seedScore = 0;
        GhostInit();
    }

    void GhostInit()
    {
        foreach (GameObject ghost in Ghosts)
        {

        }
    }
}
