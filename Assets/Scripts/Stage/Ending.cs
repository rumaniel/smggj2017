using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ending : MonoBehaviour {

    public ScrollRect scrollRect;

    // Use this for initialization
    void Start()
    {

        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        // scrollRect.DOVerticalPos( 0,  10f,false);

        DOTween.To(() => scrollRect.verticalScrollbar.value, x => scrollRect.verticalScrollbar.value = x, 0, 15f).SetLoops(-1, LoopType.Restart);
        //slider.DOValue(0.48f, 0f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).Pause();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
