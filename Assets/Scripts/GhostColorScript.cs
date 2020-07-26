using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostColorScript : MonoBehaviour
{
    public GhostState state = GhostState.Chase;
    public GhostType type = GhostType.Oikake;
    public List<GameObject> Eyes;
    public GameObject ghostLeft;
    public GameObject ghostRight;


    public Color ColorChooser()
    {
        if (state == GhostState.Frigntened)
            return Color.blue;

        switch (type)
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

    public void colorSetter()
    {
        Color color = Color.white;
        Color eyeColor = Color.black;
        if (state == GhostState.Frigntened)
        {
            color = Color.blue;
            eyeColor = Color.white;
        }
        else
        {
            color = ColorChooser();
            eyeColor = Color.blue;
        }

        GetComponent<Renderer>().material.SetColor("_Color", color);
        ghostLeft.GetComponent<Renderer>().material.SetColor("_Color", color);
        ghostRight.GetComponent<Renderer>().material.SetColor("_Color", color);
        foreach (GameObject eye in Eyes)
            eye.GetComponent<Renderer>().material.SetColor("_Color", eyeColor);
    }

}
