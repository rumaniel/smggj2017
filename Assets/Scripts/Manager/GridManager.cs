using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoSingleton<GridManager> 
{
    public SetCutscene cutscene;
    public Text text;
    List<ShipInfo> followerList = new List<ShipInfo>();
    List<DragAndDropItem> itemList = new List<DragAndDropItem>();
    
    public void SetGrid(List<ShipInfo> followerInfo)
    {
        this.followerList = followerInfo;
        this.itemList.Clear();

        for (int i = 0; i < transform.childCount; ++i)
        {
            
            Transform child = transform.GetChild(i);
            int index = 0;
            bool result = int.TryParse(child.name, out index);
            Debug.Log(index);
            if (result) 
            {
                DragAndDropItem item = child.GetChild(0).GetComponent<DragAndDropItem>();
                item.SetShipInfo(followerList[index]);
                itemList.Add(item);
            }
        }
    }

    public void ShowCutScene(GameObject obj)
    {
        int index = 0;
        bool result = int.TryParse(obj.name, out index);
        cutscene.ShowCutscene(itemList[index].shipInfo);
        text.text = itemList[index].shipInfo.ToString();
    }

    public List<ShipInfo> GetOrderedItemList()
    {
        List<ShipInfo> infoList = new List<ShipInfo>();
        for (int i = 0 ; i < itemList.Count; ++i)
        {
            infoList.Add(itemList[i].shipInfo);
        }

        return infoList;
    }

}