using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public int seedScore = 0;

    void Start()
    {
    seedScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(seedScore);
    }
}
