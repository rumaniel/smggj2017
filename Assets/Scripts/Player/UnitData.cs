using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitData : MonoBehaviour 
{
	public float health;

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public float AddHealth(float health)
    {
        this.health += health;
        return this.health;
    }

    public bool IsDie()
    {
        if (health < 1f)
        {
            return true;
        }
        return false;
    }
}