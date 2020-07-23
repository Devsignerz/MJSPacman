using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForward : MonoBehaviour
{
    float speed = .6f;
    public Vector3 targetPosition;

    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfu;

        if ( Physics.Raycast(ray, out hitInfu, 100f, mask, QueryTriggerInteraction.Ignore) )
        {
            Debug.DrawLine(ray.origin, hitInfu.point, Color.red);
        } else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
        }
        if (hitInfu.distance >= 3f)
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
