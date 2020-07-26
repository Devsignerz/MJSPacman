using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsScript : MonoBehaviour
{
    public GameObject gameMode;


    private void Start()
    {
        ++gameMode.GetComponent<GameModeScript>().maxScore;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameMode.GetComponent<GameModeScript>().EatingCookieSoundPlay();
            ++gameMode.GetComponent<GameModeScript>().seedCounder;
            gameMode.GetComponent<GameModeScript>().Score += 50;
            gameMode.GetComponent<GameModeScript>().FrigntenedActivated();
            Destroy(gameObject);
        }
    }
}
