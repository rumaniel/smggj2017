using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName="PatternInfo", menuName="ScriptableObject/PatternInfo")]
public class PatternInfo : ScriptableObject 
{
    public Defines.EnemyAppearPattern appearPattern;
    public List<Vector2> customAppearList;
    public Defines.EnemyMovingPattern movingPattern;
    public List<Vector2> customMoveSpotList;
    public Defines.EnemyLeavePattern leavePattern;
    public float stayTime;
    public List<Vector2> leaveDirectionList;
}