using UnityEngine;


public class CanvasRoot : MonoSingleton<CanvasRoot>
{
    private Canvas _rootCanvas;
    public Canvas RootCanvas
    {
        get
        {
            if (_rootCanvas.IsNull()) _rootCanvas = GetComponent<Canvas>();

            return _rootCanvas;
        }
    }
}