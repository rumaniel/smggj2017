using UnityEngine;

public class MovingTarget : UnitBase
{
	private int dirRight = 1;
	public float moveSpeed = 2.5f;

	protected override void Update ()
	{
		base.Update();

        transform.Translate (dirRight * Vector2.right * moveSpeed * Time.deltaTime);

		if (transform.position.x >= 3.0f)
			dirRight = -1;
		else if (transform.position.x <= -3)
			dirRight = 1;
	}
}