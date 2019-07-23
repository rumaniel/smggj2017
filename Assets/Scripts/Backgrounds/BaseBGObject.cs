using UnityEngine;
using UnityEngine.Events;

public class BaseBGObject : UnitBase
{
    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    private bool isMoving;

    public UnityEvent onBGObjectInit;

    protected Vector2 min;
    protected Vector2 max;

    protected System.Action<BaseBGObject> endAction = null;

    protected virtual void Awake()
    {
        isMoving = false;

		max.y = max.y + GetComponent<UnityEngine.UI.Image>().sprite.bounds.extents.y;
		min.y = min.y - GetComponent<UnityEngine.UI.Image>().sprite.bounds.extents.y;
    }

    protected override void Update ()
	{
		base.Update();

        if (!isMoving) return;

        if (transform.position.y < min.y)
        {
            ResetPosition();
        }
        else
        {
            UpdatePosition();
        }
	}

    public virtual void InitializeObject(System.Action<BaseBGObject> endAction)
    {
        this.endAction = endAction;

        onBGObjectInit.Invoke();

        isMoving = true;
    }

    protected void UpdatePosition()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);

        transform.position = position;
    }

    protected virtual void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        if (endAction != null)
        {
            endAction(this);
            endAction = null;
        }

        isMoving = false;
    }
}