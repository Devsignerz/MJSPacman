using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OikakeScript : MonoBehaviour
{
    float speed = .6f;
    enum State
    {
        Scatter,
        Chase,
        Frigntened,
        Eaten,
    } State state = State.Frigntened;
    Vector3 targetPosition;
    Vector3 perviusStep = Vector3.forward;
    public Vector3 scatterPosition = new Vector3(24, 1, 35);
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FindNextStep();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
    }


    void FindNextStep()
    {
        var possibleDirctions = new List<Vector3>();

        if (transform.position == targetPosition)
        {
            if (dirCheck(Vector3.forward))
                possibleDirctions.Add(Vector3.forward);

            if (dirCheck(Vector3.left))
                possibleDirctions.Add(Vector3.left);

            if (dirCheck(Vector3.back))
                possibleDirctions.Add(Vector3.back);

            if (dirCheck(Vector3.right))
                possibleDirctions.Add(Vector3.right);

            Vector3 nextDir = RandomDirection(possibleDirctions);
            targetPosition += nextDir * 3;
            perviusStep = nextDir;
        }


        //Debug.Log(nextDir + " / " + ray.direction + " / " + perviusStep + " / " + (nextDir * -1) + " / " + targetPosition);
    }

    Vector3 RandomDirection(List<Vector3> possibilities)
    {
        int randomNumber = Random.Range(0, possibilities.Count);
        return possibilities[randomNumber];
    }

    bool dirCheck(Vector3 Direction)
    {
        if (perviusStep != Direction * -1)
            if (HitInfu(Direction).distance >= 3f)
                return true;
        return false;
    }

    RaycastHit HitInfu(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hitInfu;
        if (Physics.Raycast(ray, out hitInfu, 100f, mask, QueryTriggerInteraction.Ignore))
            Debug.DrawLine(ray.origin, hitInfu.point, Color.red);
        else
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
        return hitInfu;
    }
}
