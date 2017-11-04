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
	protected bool isBulletInitialized = false;
    Vector4 bulletBoundary;
	
	protected virtual void OnEnable()
    {
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
        bulletBoundary = new Vector4(min.x, max.x, min.y, max.y);
		isBulletInitialized = false;
    }

	public void Initialize(WeaponInfo weaponInfo)
	{
		this.weaponInfo = weaponInfo;
		hitEffect.SetActive(false);
		isBulletInitialized = true;
	}

	protected virtual void Update () 
	{
		if (GameManager.Instance.isPause) return;
		if (isBulletInitialized)
		{
			Vector3 pos = transform.position;
			Vector3 velocity = new Vector3 (0, weaponInfo.shotSpeed * Time.deltaTime, 0);
			pos += transform.rotation * velocity;
			transform.position = pos;
			
			CheckOutofBound();
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the player bullet with an enemy ship or world hazard.
		if ((col.tag == "WorldHazard") || (col.tag == "PlayerShip")) 
		{
			PlayHitEffect();
			ConsumeBullet();
		}
	}

    public void CheckOutofBound()
    {
        if ((transform.position.x < bulletBoundary.x) || (transform.position.x > bulletBoundary.y) ||
            (transform.position.y < bulletBoundary.z) || (transform.position.y > bulletBoundary.w)) {
			ConsumeBullet();
        }        
    }

	protected void ConsumeBullet()
	{
		isBulletInitialized = false;
		pooledObject.ReturnToPool();
	}

	// Function to instantiate a particle effect.
    public virtual void PlayHitEffect()
	{
		hitEffect.SetActive(true);
		
	}
}