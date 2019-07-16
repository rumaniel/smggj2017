using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class UnitData : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthGauge;

    public void SetHealth(float health)
    {
        this.maxHealth = health;
        this.health = health;
        UpdateGauge();
    }

    public int GetMaxHealth()
    {
        return (int)maxHealth;
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
        healthGauge.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Max(2, 32f * calc), 8f);
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