using UnityEngine;

public class BaseBGObject : UnitBase
{
    [SerializeField]
    public float moveSpeed;

    protected Vector2 min;
    protected Vector2 max;

    protected virtual void Awake()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    protected override void Update ()
	{
		base.Update();

        if (transform.position.y < min.y) ResetPosition();
        else UpdatePosition();
	}

    protected void UpdatePosition()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);

        transform.position = position;
    }

    protected virtual void ResetPosition()
    {
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }
}