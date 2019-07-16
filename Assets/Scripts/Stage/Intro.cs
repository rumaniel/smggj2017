using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class Intro : MonoBehaviour
{
    public ScrollRect scrollRect;

    private void Awake()
    {
        Screen.SetResolution(640, 1136, false);
    }

    void Start ()
    {

        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        DOTween.To(() => scrollRect.verticalScrollbar.value, x => scrollRect.verticalScrollbar.value = x, 0, 15f).SetLoops(-1, LoopType.Restart);
    }

    public void OnStart()
    {
        DOTween.Kill(gameObject);

        EventManager.TriggerEvent(new SceneChangeEvent(Defines.GameState.Menu));
    }

}
