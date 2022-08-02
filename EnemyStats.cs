using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    private EnemyStats instance;
    public float health;
    private float maxHealth;
    public float attack;
    public float defense;
    public static bool canChange = true; // Are you able to change enemies (no if during animations).
    public GameObject specialEffectBoost;

    private bool shielding;
    public float storedDamage;

    private string nameEnemy;
    private static string nameStatic;
    private bool provoked = false;
    private float totalHealth;
    public float attackMultiplier = 1;
    private static bool evasion = false; // static = set for all

    public static bool terrainSet;
    public static string cursedTarget; // name of player with the curse
    public string hitTarget;

    public static int phase = 1;

    private readonly static float defaultSpreadDamage = 10;

    private void Awake()
    {
        storedDamage = 0;
        instance = this;
        maxHealth = health;
        this.nameEnemy = transform.name.Replace("(Clone)", "");
        totalHealth = health;
        SetName(nameEnemy);
        nameStatic = this.nameEnemy;
    }
    public float TakeDamage(float damage)
    {
        if (evasion && !CharacterMovement.critRateCharmBought)
        {
            int rand = Random.Range(0, 4);
            if (rand != 0 || BattleManager.turn == 1  || damage == 0) // 3/4 chance to miss
            {
                return 0;
            }
        }
        if (GetName() == "Virus-Lrg" && phase == 1)
        {
            StopTerrain();
        }
        SetEvasion(false);
        health -= damage;

        return damage;
    }

    public float Attack(PlayerStats opponent)
    {
        SetEvasion(false);
        if (opponent.IsShielding())
        {
            SetMultiplier(1);
            return 0;
        }
        float attackAmount = 0;

        attackAmount = (500f * ((attack) / opponent.GetDefense())) / 10;
        int rand = Random.Range(0, 100);
        if (rand < 10)
        {
            attackAmount *= 1.1f;
            BattleManager.critHappened = true;
        }
        attackAmount *= attackMultiplier;

        SetMultiplier(1);

        if (IsProvoked())
        {
            attackAmount = (attackAmount / 3);
        }

        hitTarget = opponent.inspectorName;
        if (opponent.IsCursed())
        {
            attackAmount *= 1.5f;
            opponent.SetCurse(false);
        }

        if (GetName() == "Vampire" || GetName() == "Virus-Lrg")
        {
            HealthFromDamageDealt(attackAmount);
        }
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

    private void OnMouseDown()
    {
        if (canChange)
        {
            BattleManager.SetAsCurrEnemy(transform.gameObject);
        }
    }

    // Set whether an enemies effect particles should be playing.
    public void SetSpecialEffects(bool val)
    {
        specialEffectBoost.SetActive(val);
    }

    // Name of enemy
    public string GetName()
    {
        return nameEnemy;
    }

    public float GetTotalHealth()
    {
        return totalHealth;
    }

    public void SetProvoke(bool status)
    {
        provoked = status;
    }

    public bool IsProvoked()
    {
        return provoked;
    }

    // Karen
    public void SetMultiplier(float num)
    {
        attackMultiplier = num;
        if (specialEffectBoost != null)
        {
            if (num != 1)
            {
                SetSpecialEffects(true);
            }
            else
            {
                SetSpecialEffects(false);
            }
        }
    }

    // Stacy
    public void SetEvasion(bool val)
    {
        if (GetName() == "Stacy" || (/*phase == 1 &&*/ GetName() == "Virus-Lrg"))
        {
            if (GetName() == "Stacy")
            {
                SetSpecialEffects(val);
            }
            evasion = val;
        }
    }

    public bool IsEvading()
    {
        return evasion;
    }

    // Chad
    public static float SpreadDamage(PlayerStats opponent)
    {
        return opponent.IsShielding() ? defaultSpreadDamage / 2 : defaultSpreadDamage;
    }

    // Vampire (also Virus-Lrg on phase 2 and 3)
    private void HealthFromDamageDealt(float attackAmount)
    {
        float temp = 0;
        if (hitTarget == cursedTarget) {
            temp = Mathf.Round(attackAmount);
            cursedTarget = "";
        }
        else
        {
            temp = Mathf.Round(attackAmount / 2);
        }
        health += temp;
        BattleManager.ShowTheHeal(temp, 0); // Heals whatever dmg is dealt

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public float LifeAbsorb(PlayerStats opponent)
    {
        float damage = opponent.IsShielding() ? defaultSpreadDamage : opponent.GetHealth() / 4;
        health += Mathf.Round(damage / 2);
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        return Mathf.Round(damage);
    }

    public IEnumerator DestroyParticle(ParticleSystem ps)
    {
        yield return new WaitForSeconds(2f);
        Destroy(ps.gameObject);
    }

    // Virus-Lrg
    public void SetUpBattleField()
    {
        terrainSet = true;
        SoundEffectsBattle.poisonGasStatic.Play();
    }

    public void StopTerrain()
    {
        phase = 2;
        terrainSet = false;
    }

    public static float DoTerrainDamage(PlayerStats opponent)
    {
        return Mathf.Round(SpreadDamage(opponent) * 1.2f);
    }

    public static IEnumerator ShowTerrainEffect(Text temp)
    {
        temp.text = "The poisonous terrain is slowly doing damage!";
        yield return new WaitForSeconds(1.8f);

        temp.text = nameStatic + " is waiting for you to hit it...";
        yield return new WaitForSeconds(1.8f);
        BattleManager.SetUpBeginningTurn();
    }
}
