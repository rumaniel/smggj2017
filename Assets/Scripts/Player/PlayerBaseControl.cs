using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerBaseControl : MonoBehaviour 
{
	protected Weapon currentWeapon;

	public List<Transform> firePointList;
    public GameObject explosionObject;

	public float moveSpeed;

	protected GameManager gameManger;
	public virtual void Init()
	{
		// Activate the gameObject.
		gameObject.SetActive (true);
	}
	
	protected void Awake()
	{
		gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    public virtual void SetWeapon(Defines.WeaponType weaponType)
    {
        currentWeapon = WeaponManager.Instance.GetWeapon(weaponType);
    }

	public virtual void FireWeapon()
	{

	}

	public virtual void PlayExplosion()
	{
        explosionObject.SetActive(true);
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{

	}

}