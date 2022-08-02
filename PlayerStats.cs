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
    public GameObject character;

    private bool shield;
    public GameObject shieldEffect;
    public GameObject healEffect;
    public GameObject curseEffect;

    // Inflicted onto enemies
    public ParticleSystem fireDamage;
    public ParticleSystem provokeEffect;

    // Enemy inflicted status
    public bool cursed;


    private void Start()
    {
        if (inspectorName.Equals("Tai"))
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
        attackAmount = (500f * ((attackAmount * rand) / oppDefense)) / 10;
        if (CharacterMovement.critRateCharmBought)
        {
            rand = Random.Range(0, 100);
            if (rand < 25)
            {
                attackAmount *= critDamageMultiplier;
                BattleManager.critHappened = true;
            }
        }
        return Mathf.Round(attackAmount);
    }

    public void CreateFireDamage()
    {
        ParticleSystem fire = Instantiate(fireDamage, BattleManager.GetEnemy().transform.position, Quaternion.identity);
        SoundEffectsBattle.fireExplosionStatic.Play();
        StartCoroutine("DestroyParticle", fire);
    }

    public void FireChargeSound()
    {
        SoundEffectsBattle.fireChargeStatic.Play();
    }

    public void SwordAttackSound()
    {
        SoundEffectsBattle.swordSlashStatic.Play();
    }

    public void ProvokeSound()
    {
        SoundEffectsBattle.provokeStatic.Play();
    }

    public void WhooshSound()
    {
        SoundEffectsBattle.whooshStatic.Play();
    }

    public void CreateProvokeEffect()
    {
        Vector3 enemyPos = BattleManager.GetEnemy().transform.position;
        Vector3 newEnemyPos = new Vector3(enemyPos.x + 0.5f, enemyPos.y + 0.5f, enemyPos.z);
        ParticleSystem provoke = Instantiate(provokeEffect, newEnemyPos, Quaternion.identity);
        StartCoroutine("DestroyParticle", provoke);
    }

    IEnumerator DestroyParticle(ParticleSystem ps)
    {
        yield return new WaitForSeconds(2f);
        Destroy(ps.gameObject);
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
    }

    public float HealTai(int numOfChar)
    {
        float healAmount = 25;
        healAmount = CharacterMovement.healCharmBought ? healAmount + Mathf.Floor(CharacterMovement.annaHealth * 0.2f) : healAmount;
        if (numOfChar == 2)
        {
            healAmount = Mathf.Ceil(healAmount / 2);
        }
        float temp1 = currHealth;
        currHealth = healAmount + currHealth > CharacterMovement.taiHealth ? CharacterMovement.taiHealth : currHealth + healAmount;
        float temp2 = currHealth;
        SetCurse(false);
        healEffect.SetActive(true);
        StartCoroutine("HideEffect");
        return temp2 - temp1; //Return how much was healed
    }

    public float HealAnna(int numOfChar)
    {
        float healAmount = 25;
        healAmount = CharacterMovement.healCharmBought ? healAmount + Mathf.Floor(CharacterMovement.annaHealth * 0.2f) : healAmount;
        if (numOfChar == 2)
        {
            healAmount = Mathf.Ceil(healAmount/2);
        }
        float temp1 = currHealth;
        currHealth = healAmount + currHealth > CharacterMovement.annaHealth ? CharacterMovement.annaHealth : currHealth + healAmount;
        float temp2 = currHealth;
        SetCurse(false);
        healEffect.SetActive(true);
        StartCoroutine("HideEffect");
        return temp2 - temp1; //Return how much was healed
    }

    IEnumerator HideEffect()
    {
        ParticleSystem ps = healEffect.GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(ps.main.duration);
        healEffect.SetActive(false);
    }

    public float GetHealth()
    {
        return currHealth;
    }

    public bool IsDead()
    {
        return currHealth <= 0;
    }

    public float GetDefense()
    {
        return currDefense;
    }

    public float GetAttack()
    {
        return currAttack;
    }

    public void SetShield(bool action)
    {
        shield = action;
        if (action)
        {
            SoundEffectsBattle.shieldStatic.Play();
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

    public void SetCurse(bool action)
    {
        cursed = action;
        if (action)
        {
            curseEffect.SetActive(true);
            SoundEffectsBattle.curseStatic.Play();
        }
        else
        {
            curseEffect.SetActive(false);
        }
    }

    public bool IsCursed()
    {
        return cursed;
    }
}
