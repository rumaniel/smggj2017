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
        UpdateGauge();
    }

    public float AddHealth(float health)
    {
        this.health += health;

        if (healthGauge != null)
        {
            UpdateGauge();
        }

        return this.health;
    }

    public void UpdateGauge()
    {
        float calc = this.health / maxHealth;
        healthGauge.size = new Vector2(Mathf.Max(0.02f,(0.32f) * calc),0.08f);
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