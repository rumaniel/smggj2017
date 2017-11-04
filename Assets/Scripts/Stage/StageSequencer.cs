using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum SequenceState
{
    Idle,
    Running,
    NeedClear,
    Loop,
    Done,
}

[System.Serializable]
public class StageSequencer
{
    public float wait;
    public bool isLoop;
    public bool previousLoopClear;
    public ShipInfo appearShip;
    public PatternInfo patternInfo;

    SequenceState state = SequenceState.Idle;
    private float accumulatedTime = 0f;


    public void Update(float delta)
    {
        if (wait > accumulatedTime)
        {
            accumulatedTime += delta;
            if (!IsLoop())
            {
                state = SequenceState.Running;
            }
            return;
        }

        if (previousLoopClear)
        {
            if (NeedClear())
            {
                previousLoopClear = false;
                state = SequenceState.Running;
            }
            else
            {
                state = SequenceState.NeedClear;
            }
        }
        // do ships
        UnitControl unit = UnitManager.Instance.GetUnit().GetComponent<UnitControl>();
        unit.SetUnitInfo(patternInfo, appearShip);
        unit.Init();

        if (isLoop)
        {
            accumulatedTime = 0f;
            state = SequenceState.Loop;
        }
        else
        {
            state = SequenceState.Done;   
        }
    }

    public bool IsLoop()
    {
        return state == SequenceState.Loop;
    }

    public bool IsDone()
    {
        return state == SequenceState.Done;
    }    
    
    public bool IsRunning()
    {
        return state == SequenceState.Running;
    }

    public bool NeedClear()
    {
        return state == SequenceState.NeedClear;
    }
}

