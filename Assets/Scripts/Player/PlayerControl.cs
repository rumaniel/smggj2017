using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerControl : PlayerBaseControl 
{
    public GameObject gameoverObject;

	public List<FollowerControl> followerList;
	float timeToFire = 0f;

	public override void Init()
	{
		base.Init();
		PlayerData.Instance.SetPlayerControl(this);
		unitData.SetHealth(this.shipInfo.health);
		transform.position = new Vector2 (0, -2.5f);
		PatchFollwerInfo();
	}
	
	public void PatchFollwerInfo()
	{
		for (int i = 0; i < followerList.Count; ++i)
		{
			followerList[i].UpdateFollower(PlayerData.Instance.followerInfo[i]);
		}
	}

	void Update () 
	{
		if (GameManager.Instance.isPause) return;
		MovePlayer();

		if (isInitialize && Input.GetButton("Fire"))
		{
			if (currentWeapon.GetFireRate() == 0f) 
			{
				FireWeapon ();
			} 
			else if (Time.time > timeToFire)  
			{
				timeToFire = Time.time + 1f / currentWeapon.GetFireRate();
				FireWeapon ();
			}
		}
	}

	void MovePlayer()
	{
		float horizontalRatio = Input.GetAxis("Horizontal") * shipInfo.moveSpeed * Time.deltaTime;
		float verticalRatio   = Input.GetAxis("Vertical") * shipInfo.moveSpeed * Time.deltaTime;

		transform.position = new Vector3(transform.position.x + horizontalRatio, transform.position.y + verticalRatio, transform.position.z);
	}

	protected override void OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag == "EnemyBullet"))
		{
			BulletBase bullet = col.transform.GetComponent<BulletBase>();
			if (bullet != null)
			{
				PlayerData.Instance.AddHealth(-bullet.weaponInfo.damage);
                if (unitData.IsDie())
                {
                    StartCoroutine("Die");
                }
            }
        }
    }

    IEnumerator Die()
    {
        explosionObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        explosionObject.SetActive(false);
        this.gameObject.SetActive(false);
        gameoverObject.SetActive(true);
       // yield return new WaitForSeconds(3f);

        //GetComponent<MonoPooledObject>().ReturnToPool();
    }

    public void OnGameOver()
    {
        StageManager.Instance.ClearStage();
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }
}