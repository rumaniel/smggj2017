using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class PlayerBaseControl : MonoBehaviour 
{
	private UnitData _unitData;
	public UnitData unitData
	{
		get
		{
			if (_unitData == null)
			{
				_unitData = GetComponent<UnitData>();
			}
			return _unitData;
		}
	}
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
		SetWeapon();
		isInitialize = true;
	}
	
	protected void Awake()
	{
		gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();
		
		DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
	}

    public virtual void SetWeapon()
    {
        currentWeapon = WeaponManager.Instance.GetWeapon(shipInfo.bulletIndex);
        currentWeapon.SetUpWeapon(shipInfo.weaponInfo, firePointList);
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
		if ((col.tag == "PlayerBullet"))
		{
			BulletBase bullet = col.transform.GetComponent<BulletBase>();
			if (bullet != null)
			{
				unitData.AddHealth(-bullet.weaponInfo.damage);
				Debug.Log(unitData.health);
			}
		}
	}

}