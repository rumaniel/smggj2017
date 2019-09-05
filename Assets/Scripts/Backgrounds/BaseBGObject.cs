using UnityEngine;
using UnityEngine.Events;

public class BaseBGObject : UnitBase
{
    [SerializeField]
    public float moveSpeed;

    [SerializeField]
    private bool isMoving;

    public UnityEvent onBGObjectInit;

    public Vector2 min;
    public Vector2 max;

    protected BaseBGObjectEvent endAction = null;

    private RectTransform _rect;
    protected RectTransform rectTransform
    {
        get
        {
            if (_rect == null) _rect = GetComponent<RectTransform>();

            return _rect;
        }
    }

    protected virtual void Awake()
    {
        isMoving = false;

		min.y = min.y - (rectTransform.rect.height * rectTransform.localScale.y);
    }

    protected override void Update ()
	{
		base.Update();

        if (!isMoving) return;

        if (rectTransform.anchoredPosition3D.y < min.y)
        {
            ResetPosition();
        }
        else
        {
            UpdatePosition();
        }
	}

    public virtual void InitializeObject(BaseBGObjectEvent endAction)
    {
        ResetPosition();

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
        rectTransform.anchoredPosition3D = new Vector3(Random.Range(min.x, max.x), max.y, 0f);

        if (endAction != null)
        {
            endAction.Invoke(this);
            endAction = null;
        }

        isMoving = false;
    }
}