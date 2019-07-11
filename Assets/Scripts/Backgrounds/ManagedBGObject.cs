using UnityEngine;

public class ManagedBGObject : BaseBGObject
{
    [SerializeField]
    private bool isMoving;

    private System.Action<BaseBGObject> endAction = null;
    
    protected override void Awake()
	{
        base.Awake();

        isMoving = false;

		max.y = max.y + GetComponent<UnityEngine.UI.Image>().sprite.bounds.extents.y;
		min.y = min.y - GetComponent<UnityEngine.UI.Image>().sprite.bounds.extents.y;
	}

	protected override void Update () 
	{
        if (!isMoving) return;

        base.Update();
	}

    public void InitializeObject(System.Action<BaseBGObject> endAction)
    {
        this.endAction = endAction;

        isMoving = true;
    }

	protected override void ResetPosition()
	{
        isMoving = false;

        base.ResetPosition();

        if (endAction != null)
        {
            endAction(this);
            endAction = null;
        }
    }
}