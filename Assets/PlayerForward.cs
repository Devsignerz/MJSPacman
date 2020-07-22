using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForward : MonoBehaviour
{
    public WallTrigger FrontBoxL;
    public WallTrigger FrontBoxR;
    public float speed = .6f;
    public Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (FrontBoxL.frontIsOpen && FrontBoxR.frontIsOpen)
        {
            if (transform.position == targetPosition)
            {
                if (transform.forward == Vector3.back ||
                     transform.forward == Vector3.forward ||
                     transform.forward == Vector3.left ||
                     transform.forward == Vector3.right)
                {
                    targetPosition += transform.forward * 3;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
    }

}
