public enum SequenceState
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
    private float wait;
    private bool isLoop;
    private bool previousLoopClear;
    private ShipInfo appearShip;
    private PatternInfo patternInfo;

    private SequenceState state = SequenceState.Idle;
    private float accumulatedTime = 0f;

    public StageSequencer(StageSequencer sequencer)
    {
        this.wait = sequencer.wait;
        this.isLoop = sequencer.isLoop;
        this.previousLoopClear = sequencer.previousLoopClear;
        this.appearShip = sequencer.appearShip;
        this.patternInfo = sequencer.patternInfo;
        this.state = sequencer.state;
        this.accumulatedTime = 0f;
    }

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
        var unit = UnitManager.Instance.GetUnit().GetComponent<UnitControl>();
        unit.SetUnitInfo(patternInfo, appearShip, true);
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

    public void EndLoop() => isLoop = false;
    public void EndSequence() => state = SequenceState.Done;
    public bool IsLoop() => state == SequenceState.Loop;
    public bool IsDone() => state == SequenceState.Done;
    public bool IsRunning() => state == SequenceState.Running;
    public bool NeedClear() => state == SequenceState.NeedClear;
}

