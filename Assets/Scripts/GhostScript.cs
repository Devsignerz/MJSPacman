using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GhostScript : MonoBehaviour
{
    public GameObject ghostLeft;
    public GameObject ghostRight;
    public GhostDirection direction;
    public float speed = .1f;
    Vector3 nextPosition;
    Vector3 nextDirecton;

    // Start is called before the first frame update
    void Start()
    {
        Color color = ColorChooser();
        GetComponent<Renderer>().material.SetColor("_Color", color);
        ghostLeft.GetComponent<Renderer>().material.SetColor("_Color", color);
        ghostRight.GetComponent<Renderer>().material.SetColor("_Color", color);

        nextPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.y == nextDirecton.y)
            nextDirecton = direction.FindNextDirection();
        if (transform.position == nextPosition)
            nextPosition = direction.MoveInDirection(nextDirecton);

        if (transform.position.x <= -30)
        {
            transform.position += new Vector3(54f, 0f, 0f);
            nextPosition.x += 54f;
        }

        if (transform.position.x >= 30)
        {
            transform.position += new Vector3(-54f, 0f, 0f);
            nextPosition.x += -54f;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, nextPosition, speed);
        transform.rotation = Quaternion.Lerp(transform.rotation,
                             Quaternion.Euler(transform.rotation.x, nextDirecton.y, transform.rotation.z),
                             speed);
    }

    Color ColorChooser()
    {
        if (direction.targetFinder.stateSwitcher.state == GhostState.Frigntened)
            return Color.blue;

        switch (direction.targetFinder.gtype)
        {
            case GhostType.Oikake:
                return Color.red;

            case GhostType.Machibuse:
                return new Color(1f, .5f, 1f);

            case GhostType.Kimagure:
                return Color.cyan;

            case GhostType.Otoboke:
                return new Color(1f, .5f, 0f);

            default:
                return Color.white;
        }

    }

}