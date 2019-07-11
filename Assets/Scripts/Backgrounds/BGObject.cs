using UnityEngine;

public class BGObject : UnitBase
{
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private float moveSpeed;

    private Vector2 min;
	private Vector2 max;

    private System.Action<BGObject> endAction = null;
    
    void Awake()
	{
		isMoving = false;

        // TODO
		min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		max.y = max.y + GetComponent<UnityEngine.UI.Image> ().sprite.bounds.extents.y;
		min.y = min.y - GetComponent<UnityEngine.UI.Image> ().sprite.bounds.extents.y;
	}


	protected override void Update () 
	{
        base.Update();
		
		if (!isMoving) return;

        if (transform.position.y < min.y) ResetPosition();
        else                              UpdatePosition();

	}

    public void InitializeObject(System.Action<BGObject> endAction)
    {
        this.endAction = endAction;

        isMoving = true;
    }

    private void UpdatePosition()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);

        transform.position = position;
    }

	private void ResetPosition()
	{
        isMoving = false;

        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        if (endAction != null)
        {
            endAction(this);
            endAction = null;
        }
    }
}