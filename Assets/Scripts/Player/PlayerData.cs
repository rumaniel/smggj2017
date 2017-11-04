using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoSingleton<PlayerData> 
{
    public List<ShipInfo> followerInfo;
    public PlayerControl playerControl;
    public float health;

    public void SetPlayerControl(PlayerControl playerControl)
    {
        this.playerControl = playerControl;
        this.health = playerControl.shipInfo.health;
    }

    public void AddHealth(float damage)
    {
        this.health += damage;
        playerControl.unitData.AddHealth(damage);

        if (playerControl.unitData.IsDie())
        {
            // do gameOver Sequence
            // GameManager.Instance.
        }
    }

    public void UpdatePlayerInfo(List<ShipInfo> infoList)
    {
        followerInfo.Clear();
        
        for (int i = 0 ; i < infoList.Count; ++i)
        {
            followerInfo.Add(infoList[i]);
        }
    }

}