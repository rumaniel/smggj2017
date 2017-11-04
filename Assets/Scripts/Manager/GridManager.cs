using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoSingleton<GridManager> 
{
    List<ShipInfo> followerList;
    public void SetGrid(List<ShipInfo> followerInfo)
    {
        this.followerList = followerInfo;
    }

}