using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{

    public bool frontIsOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.tag == "Walls")
        {
            frontIsOpen = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.tag == "Walls")
        {
            frontIsOpen = true;
        }
    }

}
