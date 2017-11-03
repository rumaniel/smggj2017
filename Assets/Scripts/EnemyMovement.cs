using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public bool chasePlayer;

	public float moveSpeed;
	public float turnSpeed = 45f;

	Transform player;
	Transform movingTarget;

	// Let's vary the movespeed of each spawned ship slightly.
	void Start () {
		moveSpeed += Random.Range (0.1f, 0.9f);
	}

	void Update () 
	{
		if (chasePlayer == true) {
			if (player == null) {
				// Find the player's ship using its tag.
				GameObject go = GameObject.FindGameObjectWithTag ("PlayerShip");

				if (go != null) {
					player = go.transform;
				}
			}
			// At this point we've either found the player or they don't exist.
			if (player == null)
				return;
		} 
		else if (chasePlayer == false) {
			if (movingTarget == null) {
				// Find the moving target using its tag.
				GameObject go = GameObject.FindGameObjectWithTag ("MovingTarget");
				
				if (go != null) {
					movingTarget = go.transform;
				}
			}
			// At this point we've either found the target or they don't exist.
			if (movingTarget == null)
				return;
		}

		if (chasePlayer == true) {
			Invoke ("StartChasingPlayer", 0f);
		} else if (chasePlayer == false) {
			Invoke ("StartChasingTarget", 0f);
		}
	}

	// Function for the enemy ships to face the player.
	void FacePlayer()
	{
		// Turn to face the player.
		Vector2 dir = player.position - transform.position;
		dir.Normalize ();
		
		float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
		
		Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);
		
		transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRot, turnSpeed * Time.deltaTime);
	}

	// Function for the enemy ships to face the target.
	void FaceTarget()
	{
		// Turn to face the target.
		Vector2 dir = movingTarget.position - transform.position;
		dir.Normalize ();
		
		float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
		
		Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);
		
		transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRot, turnSpeed * Time.deltaTime);
	}
	
	// Function for the enemy ships to start chasing the player.
	void StartChasingPlayer()
	{
		FacePlayer();
		
		// Move toward the target.
		Vector3 pos = transform.position;
		
		Vector3 velocity = new Vector3 (0, 1 * moveSpeed * Time.deltaTime, 0);
		pos += transform.rotation * velocity;
		
		transform.position = pos;
	}

	// Function for the enemy ships to start chasing the target.
	void StartChasingTarget()
	{
		FaceTarget();

		// Move toward the target.
		Vector3 pos = transform.position;
		
		Vector3 velocity = new Vector3 (0, 1 * moveSpeed * Time.deltaTime, 0);
		pos += transform.rotation * velocity;
		
		transform.position = pos;
	}
}