using UnityEngine;

public class UnitBase : MonoBehaviour
{
    protected virtual void Update()
    {
        if (GameManager.Instance.isPause) return;
    }
}