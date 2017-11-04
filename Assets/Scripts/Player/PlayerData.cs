using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoSingleton<PlayerData> 
{
    public List<UnitData> followerInfo;
    public PlayerControl playerObject;

    public void UpdatePlayerInfo()
    {

    }

}