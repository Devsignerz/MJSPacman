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

public class GhostStateSwitcher : MonoBehaviour
{
    public GhostColorScript ghostColor;
    public List<float> stateTimes1 = new List<float> { 7f, 20f, 7f, 20f, 5f, 20f, 5.00f };
    public List<float> stateTimes2 = new List<float> { 7f, 20f, 7f, 20f, 5f, 1033.14f, .01f };
    public List<float> stateTimes5 = new List<float> { 5f, 20f, 5f, 20f, 5f, 1037.14f, .01f };
    int stateRound = 0;
    float stateTimer = 0f;
    bool stateTimerRun = true;


    void Update()
    {
        if (ghostColor.state == GhostState.Frigntened || ghostColor.state == GhostState.Eaten)
            FrigntenedEatenMode();
        else
            StateScatterChaseTimerSwitcher();

        Debuging();
    }

    void StateScatterChaseTimerSwitcher()
    {
        if (stateTimerRun)
        {
            stateTimer -= Time.deltaTime;
            if (stateTimer <= 0)
            {
                if (stateRound % 2 == 0)
                    ghostColor.state = GhostState.Scatter;
                else
                    ghostColor.state = GhostState.Chase;
                stateTimer = stateTimes1[stateRound];
                ++stateRound;

                if (stateRound >= 7)
                {
                    ghostColor.state = GhostState.Chase;
                    stateTimerRun = false;
                }
            }
        }
    }

    void FrigntenedEatenMode()
    {

    }
    
    public void FrigntenedMode()
    {
        ghostColor.state = GhostState.Frigntened;
        ghostColor.colorSetter();
    }

    void Debuging()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ghostColor.state = GhostState.Scatter;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ghostColor.state = GhostState.Chase;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ghostColor.state = GhostState.Frigntened;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ghostColor.state = GhostState.Eaten;
    }
}
