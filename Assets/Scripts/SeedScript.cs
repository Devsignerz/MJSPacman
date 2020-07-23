using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour
{
    public GameObject gameMode;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ++gameMode.GetComponent<GameModeScript>().seedScore;
            Destroy(gameObject);
        }
    }
}
