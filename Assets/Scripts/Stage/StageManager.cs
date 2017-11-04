using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoSingleton<StageManager>
{
    List<StageSequencer> stageQueue;

    public void StartStage(StageInfo stageInfo)
    {
        stageQueue.Clear();
        stageQueue = stageInfo.sequenceList;
    }

    void Update()
    {
        UpdateQueue(Time.deltaTime);
    }

    void UpdateQueue(float delta)
    {
        if (stageQueue.Count > 0)
        {
            for (int i = 0; i < stageQueue.Count; ++i)
            {
                stageQueue[i].Update(delta);

                if (stageQueue[i].IsDone())
                {
                    stageQueue.RemoveAt(i);
                    break;
                }
                else if (stageQueue[i].IsRunning())
                {
                    break;
                }
                else if (stageQueue[i].NeedClear())
                {
                    for (int j = i-1; j < i; --j)
                    {
                        if (stageQueue[i].IsLoop())
                        {
                            stageQueue.RemoveAt(j);
                        }
                    }
                }
                else if (stageQueue[i].IsLoop())
                {
                    // process
                }
            }
        }
    }
}