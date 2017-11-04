using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName="StageInfo", menuName="ScriptableObject/StageInfo")]
public class StageInfo : ScriptableObject 
{
    public List<StageSequencer> sequenceList;
}