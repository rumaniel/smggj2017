using UnityEngine;


public class CanvasRoot : MonoBehaviour
{
    private Canvas _rootCanvas;
    public Canvas RootCanvas
    {
        get
        {
            return _rootCanvas ?? GetComponent<Canvas>();
        }
    }
}