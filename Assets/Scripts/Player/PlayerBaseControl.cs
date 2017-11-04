using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerBaseControl : MonoBehaviour 
{
	public ShipInfo shipInfo;
	protected Weapon currentWeapon;

	public List<Transform> firePointList;
    public GameObject explosionObject;

	protected GameManager gameManger;
	protected bool isInitialize = false;
	public virtual void Init()
	{
		// Activate the gameObject.
		gameObject.SetActive (true);
		SetWeapon(shipInfo.weaponType);
		isInitialize = true;
	}
	
	protected void Awake()
	{
		gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    public virtual void SetWeapon(Defines.WeaponType weaponType)
    {
        currentWeapon = WeaponManager.Instance.GetWeapon(weaponType);
        currentWeapon.SetUpWeapon(firePointList);
    }

	public virtual void FireWeapon()
	{
        currentWeapon.Fire();
	}

	public virtual void PlayExplosion()
	{
        explosionObject.SetActive(true);
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{

	}

}