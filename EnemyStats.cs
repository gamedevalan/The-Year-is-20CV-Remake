using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float attack;
    public float defense;
    private string nameEnemy;

    private float totalHealth;

    private void Awake()
    {
        this.nameEnemy = transform.name.Replace("(Clone)", "");
        totalHealth = health;
        SetName(nameEnemy);
    }
    public bool TakeDamage(int damage)
    {
        health -= damage;
        return health <= 0;
    }

    public float Attack(float oppDefense)
    {
        float attackAmount = attack;
        attackAmount = (1 + (attackAmount - oppDefense) / 100) * attackAmount;
        int rand = Random.Range(1, 100);
        Debug.Log("Crit?: " + rand);
        if (rand < 12)
        {
            attackAmount *= 1.2f;
        }
        Debug.Log("Final Amount: " + Mathf.Round(attackAmount));
        return Mathf.Round(attackAmount);
    }

    public float GetDefense()
    {
        return defense;
    }

    public float GetCurrHealth()
    {
        return health;
    }

    public void SetName(string enemy)
    {
        nameEnemy = enemy;
    }

    public string GetName()
    {
        return nameEnemy;
    }

    public float GetTotalHealth()
    {
        return totalHealth;
    }
}
