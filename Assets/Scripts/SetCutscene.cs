    using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class SetCutscene : MonoBehaviour {

    public Text dialogText;
    private string text;
    public Image face;
    private bool isRunning = false;
    // Use this for initialization
    void Start () {
        //test

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowCutscene(ShipInfo info, bool isBoom = false)
    {
        if(!isRunning)
        {
            if (isBoom)
            {
                face.sprite = info.portraitList[1];
                text = info.quoteList[2];                
            }
            else
            {
                face.sprite = info.portraitList[0];
                text = info.quoteList[0];
            }
            StartCoroutine(StartCutscene());
        }
        
    }

    public IEnumerator StartCutscene()
    {
        // relativeText
        // relativeText.DOText("", 2).SetRelative().SetEase(Ease.Linear).SetAutoKill(false).Pause();

        // dialogText

        // Let's move the red cube TO 0,4,0 in 2 seconds
        //this..DOMove(new Vector3(0, 4, 0), 2);

        // Let's move the green cube FROM 0,4,0 in 2 seconds
        //greenCube.DOMove(new Vector3(0, 4, 0), 2).From();
        isRunning = true;
        dialogText.text = "";
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.DOLocalMove(new Vector3(0, 0, 0), 0.5f);

        yield return new WaitForSeconds(0.4f);

        dialogText.DOText(text, 1.5f);//.SetRelative().SetEase(Ease.Linear).SetAutoKill(false).Pause();

        yield return new WaitForSeconds(2);

        //RectTransform rect = this.GetComponent<RectTransform>();
        Tween myTween = rect.DOLocalMove(new Vector3(-393, 0, 0), 0.5f);
        yield return myTween.WaitForCompletion();
        rect.DOLocalMove(new Vector3(393, 0, 0), 0);
        isRunning = false;
    }



}
