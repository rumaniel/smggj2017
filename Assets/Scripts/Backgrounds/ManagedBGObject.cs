using UnityEngine;

public class ManagedBGObject : BaseBGObject
{
    [SerializeField]
    private bool isMoving;

    protected override void Awake()
	{
        isMoving = false;

		max.y = max.y + GetComponent<UnityEngine.UI.Image>().sprite.bounds.extents.y;
		min.y = min.y - GetComponent<UnityEngine.UI.Image>().sprite.bounds.extents.y;
	}

	protected override void Update ()
	{
        if (!isMoving) return;

        base.Update();
	}

    public override void InitializeObject(System.Action<BaseBGObject> endAction)
    {
        base.InitializeObject(endAction);

        isMoving = true;
    }

	protected override void ResetPosition()
	{
        base.ResetPosition();

        isMoving = false;
    }
}