using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager> 
{
	public bool isPause = false;
	public List<StageInfo> stageList;
	public GameObject playerShip;

    public GameObject SettingDlg;
    public GameObject SettingBtn;
	public GridManager grid;
	public Transform unitRootObject;

    public GameObject[] GameStartBtns;

    public enum GameManagerState
	{
		Opening,
		InGame,
		GameOver,
		InterMission
	}

	GameManagerState GMState;
	
	void Start () 
	{
		Time.timeScale = .8f;
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        // Set the default state to 'Opening'.
        GMState = GameManagerState.Opening;
		
		// Init UI
		OnSettingClose(true);
		grid.cutscene.CloseCutScene();
	}


	// This function sets the game state and updates it.
	public void SetGameManagerState(GameManagerState state)
	{
		GMState = state;
	}
	// Call this function when the "Play" button is pressed.
	public void StartInGame(int i)
	{
        foreach (GameObject obj in GameStartBtns)
        {
            obj.SetActive(false);
        }

        playerShip.GetComponent<PlayerControl>().Init();

        StageManager.Instance.StartStage(stageList[i]);


        GMState = GameManagerState.InGame;
	}
	// This will reload the main scene.
	public void RestartInGame()
	{
		// Reset the scene.
		SceneManager.LoadScene("Game");
	}

    public void OnSettingOpen()
    {
        foreach ( GameObject obj in GameStartBtns)
        {
            obj.SetActive(false);
        }

        SettingBtn.SetActive(false);
        RectTransform image1 = SettingDlg.GetComponent<RectTransform>();
        image1.DOLocalMove(new Vector3(0, 0, 0), 1.0f);
		isPause = true;
		grid.SetGrid(PlayerData.Instance.followerInfo);
    }
    public void OnSettingClose(bool isImediattely = false)
    {
        RectTransform image1 = SettingDlg.GetComponent<RectTransform>();
		if (isImediattely) 
		{
			image1.localPosition = new Vector3(Screen.width, 0, 0);
		}
		else
		{
			image1.DOLocalMove(new Vector3(Screen.width, 0, 0), 1.0f);
		}

        SettingBtn.SetActive(true);
		isPause = false;
        if(GMState!= GameManagerState.InGame)
            foreach (GameObject obj in GameStartBtns)
            {
                obj.SetActive(true);
            }

		PlayerData.Instance.UpdatePlayerInfo(grid.GetOrderedItemList());
		
    }

    public void OnDrop(int idxPre, int idxPost)
    {
        Debug.Log(idxPre + " to " + idxPost);
    }

    public void OnEnding()
    {
        SceneManager.LoadScene("Ending");
    }
}