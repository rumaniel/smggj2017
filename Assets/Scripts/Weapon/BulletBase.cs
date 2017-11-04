using UnityEngine;
using System.Collections;

public class BulletBase : MonoBehaviour 
{
	public WeaponInfo weaponInfo;
	public GameObject hitEffect;
	public AudioSource sound;
	private MonoPooledObject _pooledObject;
    protected MonoPooledObject pooledObject
    {
        get
        {
            if (_pooledObject == null)
            {
                _pooledObject = GetComponent<MonoPooledObject>();
            }
            return _pooledObject;
        }
    }
    Vector4 bulletBoundary;
	

	protected virtual void OnEnable()
    {
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
        bulletBoundary = new Vector4(min.x, max.x, min.y, max.y);
    }

	protected virtual void Update () 
	{
		Vector3 pos = transform.position;
		Vector3 velocity = new Vector3 (0, weaponInfo.shotSpeed * Time.deltaTime, 0);
		pos += transform.rotation * velocity;
		transform.position = pos;
		
        CheckOutofBound();
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the player bullet with an enemy ship or world hazard.
		if ((col.tag == "EnemyShip") || (col.tag == "WorldHazard") || (col.tag == "PlayerShip")) 
		{
			// Play the bullet spark effect.
			PlayHitEffect();
			// Destroy the player bullet..
            pooledObject.ReturnToPool();
		}
	}

    public void CheckOutofBound()
    {
        if ((transform.position.x < bulletBoundary.x) || (transform.position.x > bulletBoundary.y) ||
            (transform.position.y < bulletBoundary.z) || (transform.position.y > bulletBoundary.w)) {
            pooledObject.ReturnToPool();
        }        
    }

	// Function to instantiate a particle effect.
    public virtual void PlayHitEffect()
	{
		GameObject hitSpark = (GameObject)Instantiate (hitEffect);
		hitSpark.transform.position = transform.position;
	}
}