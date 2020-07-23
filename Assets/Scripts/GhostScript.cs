using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GhostState
{
    Scatter,
    Chase,
    Frigntened,
    Eaten,
}

public enum GhostType
{
    Oikake,
    Machibuse,
    Kimagure,
    Otoboke
}

public class GhostScript : MonoBehaviour
{
    Vector3 targetPosition;
    Vector3 targetRotation = Vector3.zero;
    Vector3 perviusStep = Vector3.forward;
    public GhostState state = GhostState.Chase;
    public GhostType gtype = GhostType.Oikake;
    public Vector3 scatterTarget = new Vector3(24, 1, 35);
    public LayerMask mask;
    public Transform player;
    public Transform oikake;
    public Vector3 homePos = new Vector3(0, 1, 8);
    public float speed = .3f;
    public Vector3 target = Vector3.zero;
    public bool debugVisual = true;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    void Target()
    {
        //Vector3 Target = transform.position;
        switch (gtype)
        {
            case GhostType.Oikake:
                target = player.position;
                break;

            case GhostType.Machibuse:
                target = player.position + (player.forward * 2 * 3);
                break;

            case GhostType.Kimagure:
                Vector3 PFP = player.position + (player.forward * 2 * 3);
                target = PFP + (PFP - oikake.position);
                break;

            case GhostType.Otoboke:
                target = Vector3.Distance(transform.position, player.position) < 10 ? scatterTarget : player.position;
                break;

            default:
                target = transform.position;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Target();
        FindNextStep();

        if (debugVisual) DebugVisual();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            state = GhostState.Scatter;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            state = GhostState.Chase;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            state = GhostState.Frigntened;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            state = GhostState.Eaten;
        //Debug.Log(name + " / " + state);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
        transform.rotation = Quaternion.Lerp(transform.rotation,
                             Quaternion.Euler(transform.rotation.x, targetRotation.y, transform.rotation.z),
                             speed);
    }


    void FindNextStep()
    {
        if (transform.position == targetPosition)
        {
            Vector3 nextDir = Vector3.zero;
            switch (state)
            {
                case GhostState.Scatter:
                    nextDir = FindShortestDirectionToTarget(PossibleDirections(), scatterTarget);
                    break;

                case GhostState.Chase:
                    nextDir = FindShortestDirectionToTarget(PossibleDirections(), target);
                    break;

                case GhostState.Frigntened:
                    nextDir = RandomDirection(PossibleDirections());
                    break;

                case GhostState.Eaten:
                    nextDir = FindShortestDirectionToTarget(PossibleDirections(), new Vector3(0, 0, 8));
                    break;

                default:
                    break;
            }

            if (nextDir == Vector3.forward)
                targetRotation.y = 0f;
            if (nextDir == Vector3.left)
                targetRotation.y = -90f;
            if (nextDir == Vector3.back)
                targetRotation.y = 180f;
            if (nextDir == Vector3.right)
                targetRotation.y = 90f;

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

    bool DirCheck(Vector3 Direction)
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
        Physics.Raycast(ray, out hitInfu, 100f, mask, QueryTriggerInteraction.Ignore);
        return hitInfu;
    }

    List<Vector3> PossibleDirections()
    {
        var possibleDirections = new List<Vector3>();
        if (DirCheck(Vector3.forward))
            possibleDirections.Add(Vector3.forward);

        if (DirCheck(Vector3.left))
            possibleDirections.Add(Vector3.left);

        if (DirCheck(Vector3.back))
            possibleDirections.Add(Vector3.back);

        if (DirCheck(Vector3.right))
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

    void DebugVisual()
    {
        Color color = Color.white;
        int height = 1;
        switch (gtype)
        {
            case GhostType.Oikake:
                color = Color.red;
                height = 5;
                break;

            case GhostType.Machibuse:
                color = new Color(1f, .5f, 1f);
                height = 3;
                break;

            case GhostType.Kimagure:
                color = Color.cyan;
                height = 2;
                break;

            case GhostType.Otoboke:
                color = new Color(1f, .5f, 0f);
                height = 4;
                break;

            default:
                color = Color.white;
                break;
        }
        Vector3 from1 = target + Vector3.right + (Vector3.up * height);
        Vector3 to1 = target + Vector3.left + (Vector3.up * height);
        Vector3 from2 = target + Vector3.forward + (Vector3.up * height);
        Vector3 to2 = target + Vector3.back + (Vector3.up * height);

        Debug.DrawLine(from1, to1, color);
        Debug.DrawLine(from2, to2, color);

        List<Vector3> Directs = PossibleDirections();

        foreach (Vector3 dir in Directs)
        {
            Debug.DrawLine(transform.position, HitInfu(dir).point, color);
        }

    }

}