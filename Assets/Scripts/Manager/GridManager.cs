using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoSingleton<GridManager> 
{
    public SetCutscene cutscene;
    List<ShipInfo> followerList;
    List<DragAndDropItem> itemList;
    
    public void SetGrid(List<ShipInfo> followerInfo)
    {
        this.followerList = followerInfo;

        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            int index = 0;
            bool result = int.TryParse(child.name, out index);
            if (result) 
            {
                itemList[index].SetShipInfo(followerList[index]);
            }
        }
    }

    public void ShowCutScene(GameObject obj)
    {
        
    }

}