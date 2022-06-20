using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float currHealth;
    private float currAttack;
    private float currDefense;
    private float critDamageMultiplier;

    public string inspectorName;
    private static string myName;
    public GameObject character;

    private bool shield;
    public GameObject shieldEffect;

    public GameObject healEffect;

    private void Start()
    {
        myName = inspectorName;
        if (myName.Equals("Tai"))
        {
            currHealth = CharacterMovement.taiHealth;
            currAttack = CharacterMovement.taiAttack;
            currDefense = CharacterMovement.taiDefense;
            critDamageMultiplier = CharacterMovement.taiCritDamMultiplier;
        }
        else
        {
            currHealth = CharacterMovement.annaHealth;
            currAttack = CharacterMovement.annaAttack;
            currDefense = CharacterMovement.annaDefense;
            critDamageMultiplier = CharacterMovement.annaCritDamMultiplier;
        }
    }

    private void Update()
    {
        if (currHealth <= 0)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public float Attack(float oppDefense)
    {
        float attackAmount = currAttack;
        float rand = Random.Range(1.1f, 1.25f); // Allows a random range of damage
        attackAmount = (1 + (attackAmount-oppDefense)/100) * (attackAmount * rand);
        if (CharacterMovement.critRateCharmBought)
        {
            rand = Random.Range(1, 100);
            Debug.Log("Crit?: "+rand);
            if (rand < 12)
            {
                attackAmount *= critDamageMultiplier;
            }
        }
        Debug.Log("Final Amount: "+Mathf.Round(attackAmount));
        return Mathf.Round(attackAmount);
    }

    public bool TakeDamage(float damage)
    {
        currHealth -= damage;
        return currHealth <= 0;
    }

    public void HealTai()
    {
        float healAmount = 25;
        healAmount = CharacterMovement.healCharmBought ? healAmount + (CharacterMovement.taiHealth / 20) : healAmount;
        currHealth = healAmount + currHealth > CharacterMovement.taiHealth ? CharacterMovement.taiHealth : currHealth + healAmount;
        healEffect.SetActive(true);
        StartCoroutine("HidePS");
    }

    public void HealAnna()
    {
        float healAmount = 25;
        healAmount = CharacterMovement.healCharmBought ? healAmount + (CharacterMovement.annaHealth / 20) : healAmount;
        currHealth = healAmount + currHealth > CharacterMovement.annaHealth ? CharacterMovement.annaHealth : currHealth + healAmount;
        healEffect.SetActive(true);
        StartCoroutine("HidePS");

    }

    IEnumerator HidePS()
    {
        ParticleSystem ps = healEffect.GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(ps.main.duration);
        healEffect.SetActive(false);
    }

    public float GetHealth()
    {
        return currHealth;
    }

    public float GetDefense()
    {
        return currDefense;
    }

    public void SetShield(bool action)
    {
        shield = action;
        Debug.Log(transform.name);
        if (action)
        {
            shieldEffect.SetActive(true);
        }
        else
        {
            shieldEffect.SetActive(false);
        }
    }

    public bool IsShielding()
    {
        return shield;
    }
}
