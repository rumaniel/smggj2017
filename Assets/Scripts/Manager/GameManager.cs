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

	public Transform unitRootObject;

    public GameObject[] GameStartBtns;

	Defines.GameState currentState;
	
	void Awake()
	{
		EventManager.Subscribe<SceneChangeEvent>(SceneChange);
	}

	void Start () 
	{
		Time.timeScale = .8f;
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        currentState = Defines.GameState.Intro;
		
		InitializeUI();	
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();

		EventManager.UnSubscribe<SceneChangeEvent>(SceneChange);
	}

	private void InitializeUI()
	{
		OnSettingClose(true);
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


        currentState = Defines.GameState.InGame;
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
		// grid.SetGrid(PlayerData.Instance.followerInfo);
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
        if(currentState!= Defines.GameState.InGame)
            foreach (GameObject obj in GameStartBtns)
            {
                obj.SetActive(true);
            }

		// PlayerData.Instance.UpdatePlayerInfo(grid.GetOrderedItemList());
		
    }

    public void OnDrop(int idxPre, int idxPost)
    {
        Debug.Log(idxPre + " to " + idxPost);
    }

    public void OnEnding()
    {
        SceneManager.LoadScene("Ending");
    }

#region EventManager Functions.
	private void SceneChange(SceneChangeEvent evt)
	{
		gameObject.SendMessage("Leave" + currentState.ToString(), SendMessageOptions.DontRequireReceiver);
		currentState = evt.toState;
		gameObject.SendMessage("Enter" + evt.toState.ToString(), SendMessageOptions.DontRequireReceiver);
	}

#endregion

#region fsm state Enter/Leave.
	private void EnterMenu()
	{
		SceneManager.LoadScene("Game");
	}
	
	private void LeaveMenu()
	{
		
	}
	private void EnterInGame()
	{
		// SceneManager.LoadScene("Game");
	}
	
	private void LeaveInGame()
	{
		
	}
	private void EnterGameOver()
	{

	}
	
	private void LeaveGameOver()
	{
		
	}
#endregion
}