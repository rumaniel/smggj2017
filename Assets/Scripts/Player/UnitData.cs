using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitData : MonoBehaviour 
{
    public float health;
    public float maxHealth;
    public SpriteRenderer healthGauge;

    public void SetHealth(float health)
    {
        this.maxHealth = health;
        this.health = health;

    }

    public float AddHealth(float health)
    {
        this.health += health;

        if (healthGauge != null)
        {
            float calc = this.health / maxHealth;
            Debug.Log("ca " + calc);
            healthGauge.size = new Vector2(Mathf.Max(0.02f,(0.32f) * calc),0.08f);
        }

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