using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    public ScrollRect scrollRect;

    private void Awake()
    {
        Screen.SetResolution(640, 1136, false);
    }
    // Use this for initialization
    void Start () {

        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
       // scrollRect.DOVerticalPos( 0,  10f,false);
        
        DOTween.To(() => scrollRect.verticalScrollbar.value, x => scrollRect.verticalScrollbar.value = x, 0, 15f).SetLoops(-1, LoopType.Restart);
        //slider.DOValue(0.48f, 0f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).Pause();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnStart()
    {
        DOTween.Kill(gameObject);
        
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

}
