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
            return _rect ?? GetComponent<RectTransform>();
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
        Vector3 position = new Vector3(rectTransform.anchoredPosition3D.x, rectTransform.anchoredPosition3D.y + moveSpeed * Time.deltaTime, rectTransform.anchoredPosition3D.z);

        rectTransform.anchoredPosition3D = position;
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