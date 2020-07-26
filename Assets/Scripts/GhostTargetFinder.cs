using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GhostType
{
    Oikake,
    Machibuse,
    Kimagure,
    Otoboke
}

public class GhostTargetFinder : MonoBehaviour
{
    public GhostStateSwitcher stateSwitcher;
    public Transform player;
    public Transform oikakeForKimagure;
    public Vector3 scatterTarget = new Vector3(24, 1, 35);
    public Vector3 eatenTarget = new Vector3(0, 0, 8);

    public List<List<int>> OikakeSeedLeft = new List<List<int>>
    {
        new List<int>{ 1,  2,  3,  6,  9,  12,  15,  19 },
        new List<int>{ 20, 30, 40, 50, 60, 80,  100, 120 }
    };



    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            if (debugVisual)
                debugVisual = false;
            else
                debugVisual = true;
        if (debugVisual) DebugVisual();
    }

    Vector3 GhostTypeTarget()
    {
        switch (stateSwitcher.ghostColor.type)
        {
            case GhostType.Oikake:
                return player.position;

            case GhostType.Machibuse:
                return player.position + (player.forward * 2 * 3);

            case GhostType.Kimagure:
                Vector3 PFP = player.position + (player.forward * 2 * 3);
                return PFP + (PFP - oikakeForKimagure.position);

            case GhostType.Otoboke:
                return Vector3.Distance(transform.position, player.position) < 10 ? scatterTarget : player.position;

            default:
                return transform.position;
        }
    }

    public Vector3 Target()
    {
        switch (stateSwitcher.ghostColor.state)
        {
            case GhostState.Scatter:
                return scatterTarget;

            case GhostState.Chase:
                return GhostTypeTarget();

            case GhostState.Frigntened:
                return transform.position;

            case GhostState.Eaten:
                return eatenTarget;

            default:
                return transform.position;
        }
    }

    public bool debugVisual = true;

    void DebugVisual()
    {
        Color color = Color.white;
        int height = 1;
        switch (stateSwitcher.ghostColor.type)
        {
            case GhostType.Oikake:
                color = Color.red;
                height = 2;
                break;

            case GhostType.Machibuse:
                color = new Color(1f, .5f, 1f);
                height = 3;
                break;

            case GhostType.Kimagure:
                color = Color.cyan;
                height = 4;
                break;

            case GhostType.Otoboke:
                color = new Color(1f, .5f, 0f);
                height = 5;
                break;

            default:
                color = Color.white;
                break;
        }
        Vector3 from1 = Target() + Vector3.right + (Vector3.up * height);
        Vector3 to1 = Target() + Vector3.left + (Vector3.up * height);
        Vector3 from2 = Target() + Vector3.forward + (Vector3.up * height);
        Vector3 to2 = Target() + Vector3.back + (Vector3.up * height);

        Debug.DrawLine(from1, to1, color);
        Debug.DrawLine(from2, to2, color);
        Debug.DrawLine(new Vector3(Target().x, 0, Target().z), Target() + (Vector3.up * height), color);

        //List<Vector3> Directs = PossibleDirections();

        //foreach (Vector3 dir in Directs)
        //{
            //Debug.DrawLine(transform.position, HitInfu(dir).point, color);
        //}
    }

}
