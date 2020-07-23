using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimagureScript : MonoBehaviour
{
    enum State
    {
        Scatter,
        Chase,
        Frigntened,
        Eaten,
    }
    State state = State.Chase;
    Vector3 targetPosition;
    Vector3 perviusStep = Vector3.forward;
    public Vector3 scatterTarget = new Vector3(24, 1, -34);
    public LayerMask mask;
    public Transform player;
    public Transform oikake;
    public Vector3 homePos = new Vector3(0, 1, 8);
    float speed = .6f;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FindNextStep();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            state = State.Scatter;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            state = State.Chase;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            state = State.Frigntened;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            state = State.Eaten;
        //Debug.Log(name + " / " + state);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
    }


    void FindNextStep()
    {
        if (transform.position == targetPosition)
        {
            Vector3 nextDir = Vector3.zero;
            switch (state)
            {
                case State.Scatter:
                    nextDir = FindShortestDirectionToTarget(PossibleDirections(), scatterTarget);
                    break;

                case State.Chase:
                    Vector3 PFP = player.position + (player.forward * 2 * 3);
                    Vector3 T = PFP + (PFP - oikake.position);
                    Debug.Log(T);
                    nextDir = FindShortestDirectionToTarget(PossibleDirections(), T);
                    break;

                case State.Frigntened:
                    nextDir = RandomDirection(PossibleDirections());
                    break;

                case State.Eaten:
                    nextDir = FindShortestDirectionToTarget(PossibleDirections(), new Vector3(0, 0, 8));
                    break;

                default:
                    break;
            }
            targetPosition = PosDirMov(nextDir);
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

    List<Vector3> PossibleDirections()
    {
        var possibleDirections = new List<Vector3>();
        if (dirCheck(Vector3.forward))
            possibleDirections.Add(Vector3.forward);

        if (dirCheck(Vector3.left))
            possibleDirections.Add(Vector3.left);

        if (dirCheck(Vector3.back))
            possibleDirections.Add(Vector3.back);

        if (dirCheck(Vector3.right))
            possibleDirections.Add(Vector3.right);
        return possibleDirections;
    }

    Vector3 FindShortestDirectionToTarget(List<Vector3> possibilisies, Vector3 target)
    {
        Vector3 shortestDirection = new Vector3(100, 100, 100);
        foreach (Vector3 item in possibilisies)
        {
            //if (DisCal(PosDirMov(item), target) < DisCal(PosDirMov(PosDirMov(shortestDirection)), target))
            if (Vector3.Distance(PosDirMov(item), target) < Vector3.Distance(PosDirMov(shortestDirection), target))
            {
                shortestDirection = item;
            }
        }
        return shortestDirection;
    }

    Vector3 PosDirMov(Vector3 dirction)
    {
        return transform.position + (dirction * 3);
    }

    //int DisCal(Vector3 from, Vector3 to)
    //{
    //    return (int)((from.x * from.x) + (to.y * to.y));
    //}

}