using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    /* To find where the screen shakes are ctrl + f "// Damage is dealt." 
        Long/ important methods are commented with /* while shorter ones have // 
    */

    /* 
     * Positions for showing damage UI:
    Vector3 enemyDamagePos = new Vector3(-320f, 245.4f, 0);
    Vector3 taiDamagePos = new Vector3(284.7f, 290.2f, 0);
    Vector3 annaDamagePos = new Vector3(284.7f, 200.2f, 0);
    */

    public GameObject Tai;
    public GameObject Anna;
    static public BattleManager instance;
    public GameObject[] damageDealtText;

    // Get stats of the player characters.
    private PlayerStats TaiStats;
    private PlayerStats AnnaStats;
    public static bool critHappened;
    public static float winnings;

    public static int turn;

    // Get stats of the enemy / enemies.
    public static string enemy;
    private GameObject currEnemy;
    private EnemyStats currEnemyStats;
    private GameObject otherEnemy;
    static int type;
    private static int numOfEnemies;
    public GameObject provokeStatus;
    public GameObject pointToCurrEnemy;

    // Store the enemies of the game and set whether it's a boss or not.
    public GameObject[] enemies;
    public static bool isBoss;
    private static string mainName;
    private static bool multipleEnemies;

    // The state of the battle. Who's turn, and who won.
    public enum BattleState { TAI_TURN, ANNA_TURN, ENEMY_TURN, WON, LOST}
    private BattleState state;

    // Buttons of player choices.
    public GameObject TaiChoices;
    public GameObject AnnaChoices;
    public Button HealTaiButton;
    public Button HealBothButton;

    public Text TaiHealth;
    public Text AnnaHealth;
    public Text EnemyHealth;
    public Text details; // Shows what is going one. Who's attacking, healing, etc
    public Color textColors;

    // Standing position of the player because it will move when attacking.
    Vector3 defaultpos;

    // The environment
    public MeshRenderer ground;
    public Material[] groundMaterial;
    public Light mainLighting;
    public Camera mainCamera;
    //public static string lastScene;
    public GameObject Beach_Props;
    public GameObject Store_Props;
    public GameObject Mansion_Props;
    public GameObject poisonTerrain;

    private void Awake()
    {
        turn = 0;
        instance = this;
        defaultpos = Tai.transform.position;
        float defaultEnemyX = -3.5f;
        
        currEnemy = Instantiate(enemies[type], new Vector3(defaultEnemyX, (Tai.transform.position.y + Anna.transform.position.y)/2 , 0), Quaternion.identity);

        if (multipleEnemies) // Create another enemy if needed.
        {
            currEnemy.transform.position = new Vector3(defaultEnemyX, Tai.transform.position.y, 0);
            if (isBoss && GameManager.lastScene.Equals("Beach"))
            {
                type += 1;
            }
            otherEnemy = Instantiate(enemies[type], new Vector3(defaultEnemyX, Anna.transform.position.y, 0), Quaternion.identity);
        }
        TaiStats = Tai.GetComponent<PlayerStats>();
        AnnaStats = Anna.GetComponent<PlayerStats>();
        currEnemyStats = currEnemy.GetComponent<EnemyStats>();
        ground = ground.GetComponent<MeshRenderer>();
    }

    // Set what the environment was before the battle started.
    private void Start()
    {
        if (GameManager.lastScene.Equals("Outside") || GameManager.lastScene.Equals("Store_Outside") || GameManager.lastScene.Equals("Mansion_Outside"))
        {
            mainLighting.intensity = 0.5f;
            ground.material = groundMaterial[0];
            mainCamera.backgroundColor = new Color(2f / 255, 46f / 255, 62f / 255);
            textColors = Color.white;
            TaiHealth.color = textColors;
            AnnaHealth.color = textColors;
            EnemyHealth.color = textColors;
        }
        else if (GameManager.lastScene.Equals("Beach"))
        {
            mainLighting.intensity = 1.1f;
            ground.material = groundMaterial[1];
            mainCamera.backgroundColor = new Color(133f / 255, 200f / 255, 224f / 255);
            textColors = Color.black;
            TaiHealth.color = textColors;
            AnnaHealth.color = textColors;
            EnemyHealth.color = textColors;
            Beach_Props.SetActive(true);
        }
        else if (GameManager.lastScene.Equals("Store"))
        {
            mainLighting.intensity = 0.6f;
            ground.material = groundMaterial[2];
            mainCamera.backgroundColor = new Color(161f / 255, 167f / 255, 178f / 255);
            textColors = Color.black;
            TaiHealth.color = textColors;
            AnnaHealth.color = textColors;
            EnemyHealth.color = textColors;
            Store_Props.SetActive(true);
        }
        else if (GameManager.lastScene.Equals("Mansion_Upstairs"))
        {
            mainLighting.intensity = 0.2f;
            ground.material = groundMaterial[3];
            mainCamera.backgroundColor = new Color(32f / 255, 32f / 255, 32f / 255);
            textColors = Color.white;
            TaiHealth.color = textColors;
            AnnaHealth.color = textColors;
            EnemyHealth.color = textColors;
            Mansion_Props.SetActive(true);
        }
        TaiChoices.SetActive(false);
        AnnaChoices.SetActive(false);

        if (multipleEnemies)
        {
            StartCoroutine(ShowIntro(2.5f, "The " + mainName + "s were approached!"));
        }
        else
        {
            if (mainName == "Vampire" || mainName == "Virus-Lrg")
            {
                StartCoroutine(ShowIntro(2.5f, "The " + mainName + " attacked!"));
            }
            else {
                StartCoroutine(ShowIntro(2.5f, "The " + mainName + " was approached!"));
            }
        }
    }

    IEnumerator ShowIntro(float time, string sentence)
    {
        details.text = sentence;
        yield return new WaitForSeconds(time);

        // If fighting Virus Boss, set up a poison terrain and evasion for phase 1.
        if (mainName == "Virus-Lrg" && EnemyStats.phase == 1)
        {
            currEnemyStats.SetUpBattleField();
            details.text = "Poison gas is coming out of the ground and is filling the air!";
            currEnemyStats.SetEvasion(true);
            yield return new WaitForSeconds(3f);
        }

        SetUpBeginningTurn();
    }

    private void Update()
    {
        TaiHealth.text = "Tai\n HP: " + TaiStats.GetHealth()+" /"+ CharacterMovement.taiHealth;
        AnnaHealth.text = "Anna\n HP: " + AnnaStats.GetHealth()+ " /" + CharacterMovement.annaHealth;
        EnemyHealth.text = currEnemyStats.GetName()+"\n HP: " + currEnemyStats.GetCurrHealth() + " /" +currEnemyStats.GetTotalHealth();
        currEnemyStats = currEnemy.GetComponent<EnemyStats>();
        if (mainName != "Virus-Lrg")
        {
            pointToCurrEnemy.transform.position = new Vector3(currEnemy.transform.position.x, currEnemy.transform.position.y + 1.1f, currEnemy.transform.position.z);

        }
        else
        {
            pointToCurrEnemy.transform.position = new Vector3(currEnemy.transform.position.x, currEnemy.transform.position.y + 1.6f, currEnemy.transform.position.z);
        }
        if (numOfEnemies < 1)
        {
            pointToCurrEnemy.SetActive(false);
        }

        if (currEnemyStats.IsProvoked())
        {
            provokeStatus.SetActive(true);
        }
        else
        {
            provokeStatus.SetActive(false);
        }

        if (TaiStats.IsDead())
        {
            TaiHealth.gameObject.SetActive(false);
            HealTaiButton.interactable = false;
            HealBothButton.interactable = false;
        }
        if (AnnaStats.IsDead())
        {
            AnnaHealth.gameObject.SetActive(false);
        }

        poisonTerrain.SetActive(EnemyStats.terrainSet);
    }

    // Called by CharacterMovement -> Interactables -> Door.cs
    public static void SetEnemy(string enemyType)
    {
        mainName = enemyType;
        switch (enemyType) {
            case "Virus-SM":
                isBoss = false;
                numOfEnemies = 1;
                type = 0;
                winnings = 20;
                break;
            case "Sandman":
                isBoss = false;
                numOfEnemies = 1;
                type = 1;
                winnings = 30;
                break;
            case "Bat":
                isBoss = false;
                numOfEnemies = 1;
                type = 2;
                winnings = 40;
                break;
            case "Karen":
                isBoss = true;
                numOfEnemies = 2;
                type = 3;
                winnings = 100;
                SpawnBosses.currBoss = 0;
                break;
            case "Spring Breaker":
                isBoss = true;
                numOfEnemies = 2;
                type = 4;
                winnings = 100;
                SpawnBosses.currBoss = 1;
                break;
            case "Vampire":
                isBoss = true;
                numOfEnemies = 1;
                type = 6;
                winnings = 100;
                SpawnBosses.currBoss = 2;
                break;
            case "Virus-Lrg":
                isBoss = true;
                numOfEnemies = 1;
                type = 7;
                winnings = 0;
                SpawnBosses.currBoss = 3;
                EnemyStats.phase = 1;
                break;

        }
        multipleEnemies = numOfEnemies > 1;
        SceneManager.LoadScene("Battle");
    }

    public static void SetUpBeginningTurn()
    {
        bool isDead = instance.TaiStats.IsDead() && instance.AnnaStats.IsDead();
        if (isDead)
        {
            instance.state = BattleState.LOST;
            instance.EndBattle();
            return;
        }
        turn++;
        instance.currEnemyStats.SetProvoke(false);
        if (instance.otherEnemy)
        {
            instance.otherEnemy.GetComponent<EnemyStats>().SetProvoke(false);
        }
        EnemyStats.canChange = true;
        instance.PlayerTurn();
    }

    // Who's turn is it? Are characters dead from the previous enemy turn/ terrain effect?
    public void PlayerTurn()
    {
        if (!TaiStats.IsDead())
        {
            details.text = "What will Tai do?";
            state = BattleState.TAI_TURN;
            TaiChoices.SetActive(true);
        }
        else if(!AnnaStats.IsDead())
        {
            details.text = "What will Anna do?";
            state = BattleState.ANNA_TURN;
            AnnaChoices.SetActive(true);
            if (TaiStats.IsDead())
            {
                HealTaiButton.interactable = false;
                HealBothButton.interactable = false;
            }

        }
        TaiStats.SetShield(false);
        AnnaStats.SetShield(false);
    }

    public static GameObject GetEnemy()
    {
        return instance.currEnemy;
    }

    // Change the target enemy.
    public static void SetAsCurrEnemy(GameObject enemy)
    {
        if (enemy != instance.currEnemy)
        {
            instance.otherEnemy = instance.currEnemy;
            instance.currEnemy = enemy;
        }
    }

    public void TaiHeal()
    {
       AnnaChoices.SetActive(false);
       SoundEffectsBattle.healStatic.Play();
       float saveHeal = TaiStats.HealTai(1);
       details.text = "Anna HEALS Tai by " + saveHeal + " HP!";
       ShowTheHeal(saveHeal, 1);
       StartCoroutine(DoAttackAnimation("Anna", 2f, 0, false));
    }

    public void AnnaHeal()
    {
        AnnaChoices.SetActive(false);
        SoundEffectsBattle.healStatic.Play();
        float saveHeal = AnnaStats.HealAnna(1);
        details.text = "Anna HEALS herself by " + saveHeal + " HP!";
        ShowTheHeal(saveHeal, 2);
        StartCoroutine(DoAttackAnimation("Anna", 2f, 0, false));
    }

    public void BothHeal()
    {
        AnnaChoices.SetActive(false);
        SoundEffectsBattle.healStatic.Play();
        float saveHeal = AnnaStats.HealAnna(2);
        float saveHealSecond = TaiStats.HealTai(2);
        if (saveHeal == saveHealSecond) {
            details.text = "Anna HEALS both by " + saveHeal + " HP!";
        }
        else
        {
            details.text = "Anna HEALS herself by " + saveHeal + " HP and Tai by "+ saveHealSecond+"!";
        }
        ShowTheHeal(saveHealSecond, 1);
        ShowTheHeal(saveHeal, 2);
        StartCoroutine(DoAttackAnimation("Anna", 2f, 0, false));
    }

    public void TaiPlayerAttack()
    {
        details.text = "Tai Attacks "+ currEnemyStats.GetName()+"!";
        TaiChoices.SetActive(false);
        Tai.transform.position = new Vector3(currEnemy.transform.position.x + 1.3f, currEnemy.transform.position.y, 0);
        Tai.GetComponent<Animator>().SetTrigger("Attack");
        float damage = TaiStats.Attack(currEnemyStats.GetDefense());
        StartCoroutine(DoAttackAnimation("Tai", 1.1f, damage, true));
    }

    public void AnnaPlayerAttack()
    {
        details.text = "Anna Attacks " + currEnemyStats.GetName() + "!";
        AnnaChoices.SetActive(false);
        Anna.GetComponent<Animator>().SetTrigger("Attack");
        float damage = AnnaStats.Attack(currEnemyStats.GetDefense());
        StartCoroutine(DoAttackAnimation("Anna", 2f, damage, true));
    }

    public void TaiShield()
    {
        string howManyEnemies = numOfEnemies > 1 ? "enemies'" : "enemy's";
        details.text = "Tai is protecting himself from the " + howManyEnemies + " next attack!"; ;
        TaiStats.SetShield(true);
        TaiChoices.SetActive(false);
        StartCoroutine(DoAttackAnimation("Tai", 1.5f, 0, false));
    }

    public void AnnaShield()
    {
        string howManyEnemies = numOfEnemies > 1 ? "enemies'" : "enemy's";
        details.text = "Anna is protecting herself from the " + howManyEnemies+ " next attack!";
        AnnaStats.SetShield(true);
        AnnaChoices.SetActive(false);
        StartCoroutine(DoAttackAnimation("Anna", 1.5f, 0, false));
    }

    public void TaiProvoke()
    {
        details.text = "Tai provoked "+currEnemyStats.GetName() + "!" ;
        currEnemyStats.SetProvoke(true);
        TaiChoices.SetActive(false);
        Tai.GetComponent<Animator>().SetTrigger("Provoke");
        StartCoroutine(DoAttackAnimation("Tai", 1.5f, 0, false));
    }

    /*
        Player character attacks. Checks if crit happened, enemy died, missed attack, or won battle.
    */
    IEnumerator DoAttackAnimation(string character, float waitLength, float damageDone, bool isAttack)
    {
        EnemyStats.canChange = false;
        yield return new WaitForSeconds(waitLength);
        float saveDamage = currEnemyStats.TakeDamage(damageDone);
        // Damage is dealt.
        if (saveDamage > 0) {
            ShowTheDamage(saveDamage, 0);
            if (critHappened) {
                CameraShakes.CritShake();
                critHappened = false;
            }
            else
            {
                CameraShakes.Shake();
            }
        }
        else
        {
            if (currEnemyStats.IsEvading() && isAttack) {
                details.text = "Your attack missed "+ currEnemyStats.GetName()+"!";
                yield return new WaitForSeconds(1.5f);
            }
        }

        Tai.transform.position = defaultpos;

        bool isDead = currEnemyStats.health <= 0;
        if (isDead)
        {
            GameObject temp = otherEnemy;
            currEnemy.SetActive(false);
            if (numOfEnemies > 1)
            {
                currEnemy = otherEnemy;
                currEnemyStats = currEnemy.GetComponent<EnemyStats>();
            }
            numOfEnemies--;
        }
        if (otherEnemy !=  null)
        {
            if (isDead) {
                currEnemy = otherEnemy;
                isDead = currEnemy.GetComponent<EnemyStats>().health <= 0;
            }
        }
        yield return new WaitForSeconds(0.5f);
        EnemyStats.canChange = true;
        if (numOfEnemies == 0)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            if (character.Equals("Tai"))
            {
                if (AnnaStats.GetHealth() > 0) {
                    details.text = "What will Anna do?";
                    state = BattleState.ANNA_TURN;
                    AnnaChoices.SetActive(true);
                }
                else
                {
                    state = BattleState.ENEMY_TURN;
                    EnemyAttack();
                }
            }
            else
            {
                state = BattleState.ENEMY_TURN;
                EnemyAttack();
            }
        }
    }

    private void EnemyAttack()
    {
        EnemyStats whosAttacking = currEnemyStats;
        EnemyAttackHelper(whosAttacking, 1);
    }

    /*
        Enemy attacks. Checks who to attack, if boss, use a special if can.
    */
    private void EnemyAttackHelper(EnemyStats whosAttacking, int enemyOrder)
    {
        if (EnemyStats.phase == 2 && instance.currEnemyStats.GetCurrHealth() <= instance.currEnemyStats.GetTotalHealth() / 2 && mainName == "Virus-Lrg")
        {
            EnemyStats.phase = 3;
            turn = 1;
        }

        if (mainName == "Virus-Lrg" && EnemyStats.phase == 1)
        {
            EnemyEndTurnChecks(currEnemyStats, 1);
            return;
        }
        EnemyStats.canChange = false;
        string currTarget = "";
        GameObject currGameObjectTarget;
        Vector3 defaultEnemyPos = whosAttacking.transform.position;
        if (TaiStats.IsDead() && !AnnaStats.IsDead())
        {
            currTarget = "Anna";
            currGameObjectTarget = Anna;
        }
        else if (whosAttacking.IsProvoked() || (!TaiStats.IsDead() && AnnaStats.IsDead()))
        {
            currTarget = "Tai";
            currGameObjectTarget = Tai;
        }
        else
        {
            int randomTarget = Random.Range(0, 2);
            if (randomTarget == 0)
            {
                currTarget = "Tai";
                currGameObjectTarget = Tai;
            }
            else
            {
                currTarget = "Anna";
                currGameObjectTarget = Anna;
            }
        }

        // Choose what the enemy will do.
        if (isBoss && (mainName != "Virus-Lrg" || EnemyStats.phase > 2))
        {
            // Always attack if multiplier is set or if any of the players are cursed
            if (whosAttacking.attackMultiplier != 1 || (TaiStats.IsCursed() || AnnaStats.IsCursed()))
            {
                DefaultEnemyAttack(whosAttacking, currTarget, currGameObjectTarget, enemyOrder, defaultEnemyPos);
                return;
            }
            int rand = Random.Range(0, 3);
            if (turn == 1 || rand == 0) // Start first turn with a special move.
            {
                StartCoroutine(Special(whosAttacking, enemyOrder, currTarget));
            }
            else
            {
                DefaultEnemyAttack(whosAttacking, currTarget, currGameObjectTarget, enemyOrder, defaultEnemyPos);
            }
        }
        else
        {
            DefaultEnemyAttack(whosAttacking, currTarget, currGameObjectTarget, enemyOrder, defaultEnemyPos);
        }
    }

    /*
        Move a certain distance to attack the player.
    */
    private void DefaultEnemyAttack(EnemyStats whosAttacking, string currTarget, GameObject currGameObjectTarget, int enemyOrder, Vector3 defaultEnemyPos)
    {
        details.text = whosAttacking.GetName() + " attacks " + currTarget + "!";
        if (whosAttacking.GetName() == "Vampire")
        {
            whosAttacking.transform.position = new Vector3(currGameObjectTarget.transform.position.x - 2f, currGameObjectTarget.transform.position.y + 0.16f, 0);
        }
        else if(whosAttacking.GetName() == "Karen")
        {
            whosAttacking.transform.position = new Vector3(currGameObjectTarget.transform.position.x - 0.8f, currGameObjectTarget.transform.position.y, 0);
        }
        else if (mainName == "Virus-Lrg")
        {
            whosAttacking.transform.position = new Vector3(currGameObjectTarget.transform.position.x - 3.3f, currGameObjectTarget.transform.position.y, 0);
        }
        else
        {
            whosAttacking.transform.position = new Vector3(currGameObjectTarget.transform.position.x - 1.2f, currGameObjectTarget.transform.position.y, 0);
        }
        whosAttacking.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
        StartCoroutine(DoEnemyAttack(whosAttacking, enemyOrder, currTarget, defaultEnemyPos));
    }

    /*
        Depending on which boss, use their special move.
    */
    IEnumerator Special(EnemyStats whosAttacking, int enemyOrder, string currTarget)
    {
        whosAttacking.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Special");
        float secondsToWait = 0;
        float saveDamage = 0;
        switch (whosAttacking.GetName())
        {
            case "Karen":
                whosAttacking.SetMultiplier(2);
                details.text = "Karen is finding something to be mad about!";
                secondsToWait = 1.5f;
                break;
            case "Stacy":
                whosAttacking.SetEvasion(true);
                details.text = "The Spring Breakers are drinking, it'll be harder to hit them!";
                secondsToWait = 2.5f;
                break;
            case "Chad":
                details.text = "The Spring Breaker's massive quadriceps caused a shockwave!";
                whosAttacking.SetSpecialEffects(true);
                secondsToWait = 2.5f;
                break;
            case "Vampire":
                details.text = "The Vampire is trying to take your life force!";
                float total = 0;
                if (!TaiStats.IsDead()) {
                    saveDamage = whosAttacking.LifeAbsorb(TaiStats);
                    total += saveDamage; // Saves the dmg to be used to heal
                    TaiStats.TakeDamage(saveDamage);
                    GameObject temp = Instantiate(whosAttacking.specialEffectBoost, Tai.transform.position, Quaternion.identity);
                    temp.SetActive(true);
                    whosAttacking.StartCoroutine(whosAttacking.DestroyParticle(temp.GetComponent<ParticleSystem>()));
                    ShowTheDamage(saveDamage, 1);
                }

                if (!AnnaStats.IsDead())
                {
                    saveDamage = whosAttacking.LifeAbsorb(AnnaStats);
                    total += saveDamage;  // Saves the dmg to be used to heal
                    AnnaStats.TakeDamage(saveDamage);
                    GameObject temp = Instantiate(whosAttacking.specialEffectBoost, Anna.transform.position, Quaternion.identity);
                    temp.SetActive(true);
                    whosAttacking.StartCoroutine(whosAttacking.DestroyParticle(temp.GetComponent<ParticleSystem>()));
                    ShowTheDamage(saveDamage, 2);
                }
                ShowTheHeal(Mathf.Round(total/2), 0); // Heals whatever dmg is dealt
                secondsToWait = 2.5f;
                break;
            case "Virus-Lrg":
                details.text = "A curse was inflicted onto " +currTarget + "!";
                EnemyStats.cursedTarget = currTarget;
                yield return new WaitForSeconds(1f);
                if (currTarget == "Tai")
                {
                    saveDamage = EnemyStats.SpreadDamage(TaiStats);
                    TaiStats.TakeDamage(saveDamage);
                    ShowTheDamage(saveDamage, 1);
                    TaiStats.SetCurse(true);
                }
                else
                {
                    saveDamage = EnemyStats.SpreadDamage(AnnaStats);
                    AnnaStats.TakeDamage(saveDamage);
                    ShowTheDamage(saveDamage, 2);
                    AnnaStats.SetCurse(true);
                }
                break;
        }
        yield return new WaitForSeconds(secondsToWait);
        if (whosAttacking.GetName() == "Chad")
        {
            // Damage is dealt.
            CameraShakes.Shake();
            if (!TaiStats.IsDead())
            {
                saveDamage = EnemyStats.SpreadDamage(TaiStats);
                TaiStats.TakeDamage(saveDamage);
                ShowTheDamage(saveDamage, 1);
            }

            if (!AnnaStats.IsDead())
            {
                saveDamage = EnemyStats.SpreadDamage(AnnaStats);
                AnnaStats.TakeDamage(saveDamage);
                ShowTheDamage(saveDamage, 2);
            }
            whosAttacking.SetSpecialEffects(false);
        }
        else if (mainName == "Vampire")
        {
            // Damage is dealt.
            CameraShakes.Shake();
        }
        EnemyEndTurnChecks(whosAttacking, enemyOrder);
    }

    IEnumerator DoEnemyAttack(EnemyStats whosAttacking, int enemyOrder, string currTarget, Vector3 defaultEnemyPos)
    {
        yield return new WaitForSeconds(1.2f);

        if (currTarget.Equals("Tai"))
        {
            AttackThePlayer(whosAttacking, TaiStats);
        }
        else
        {
            AttackThePlayer(whosAttacking, AnnaStats);
        }
        whosAttacking.transform.position = defaultEnemyPos;
        if (EnemyStats.terrainSet) {
            yield return new WaitForSeconds(1f);
        }
        EnemyEndTurnChecks(whosAttacking, enemyOrder);
    }

    private void AttackThePlayer(EnemyStats whosAttacking, PlayerStats player)
    {
        float damageDone = 0;
        damageDone = whosAttacking.Attack(player);
        // Damage is dealt.
        if (damageDone > 0)
        {
            if (player.inspectorName == "Tai")
            {
                ShowTheDamage(damageDone, 1);
            }
            else
            {
                ShowTheDamage(damageDone, 2);
            }
            if (critHappened)
            {
                CameraShakes.CritShake();
                critHappened = false;
            }
            else
            {
                CameraShakes.Shake();
            }
            player.TakeDamage(damageDone);
        }
    }

    // Checks if anything needs to be done before new turn happens. Any terrain effects?
    private void EnemyEndTurnChecks(EnemyStats whosAttacking, int enemyOrder)
    {
        whosAttacking.SetProvoke(false);
        if (enemyOrder == numOfEnemies)
        {
            if (EnemyStats.terrainSet)
            {
                float saveDamage = 0;
                if (!TaiStats.IsDead())
                {
                    saveDamage = EnemyStats.DoTerrainDamage(TaiStats);
                    TaiStats.TakeDamage(saveDamage);
                    ShowTheDamage(saveDamage, 1);
                }

                if (!AnnaStats.IsDead())
                {
                    saveDamage = EnemyStats.DoTerrainDamage(AnnaStats);
                    AnnaStats.TakeDamage(saveDamage);
                    ShowTheDamage(saveDamage, 2);
                }
                // Damage is dealt.
                CameraShakes.Shake();
                StartCoroutine(EnemyStats.ShowTerrainEffect(details));
            }
            else
            {
                SetUpBeginningTurn();
            }
        }
        else
        {
            whosAttacking = otherEnemy.GetComponent<EnemyStats>();
            EnemyAttackHelper(whosAttacking, 2);
        }
    }

    private void ShowTheDamage(float damage, int index)
    {
        GameObject temp = damageDealtText[index];
        temp.gameObject.transform.GetChild(0).GetComponent<Text>().text = "-"+damage;
        temp.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Damage");
    }

    public static void ShowTheHeal(float damage, int index)
    {
        GameObject temp = instance.damageDealtText[index];
        temp.gameObject.transform.GetChild(0).GetComponent<Text>().text = "+" + damage;
        temp.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Heal");
    }

    private void EndBattle()
    {

        string plural = multipleEnemies? "s": "";

        if (state == BattleState.WON)
        {
            EnemyHealth.gameObject.SetActive(false);
            string sentenceOne = "You defeated the " + mainName + plural + "!";
            string sentenceTwo = "You picked up $" + winnings + " for winning!";
            StartCoroutine(ShowResults(2f, sentenceOne, sentenceTwo, null));
            TaiChoices.SetActive(false);
            AnnaChoices.SetActive(false);
            Inventory.ChangeMoney((int)winnings);
            if (isBoss)
            {
                SpawnBosses.BeatBoss();
                if (mainName == "Virus-Lrg")
                {
                    GameManager.OpenNewEnvironment();
                }
            }
        }
        else
        {
            string sentenceOne = "You were defeated by the " + mainName + plural + "...";
            float amountLost = Inventory.currMoney - winnings / 5 < 0 ? Inventory.currMoney : winnings / 5;
            string sentenceTwo = "You barely escaped, but lost $" + amountLost + " in the process...";
            string sentenceThree = "Maybe try buying stuff from the bush shop...";
            StartCoroutine(ShowResults(2f, sentenceOne, sentenceTwo, sentenceThree));
            TaiChoices.SetActive(false);
            AnnaChoices.SetActive(false);
            Inventory.ChangeMoney((int)(-(winnings / 5)));
        }
    }

    IEnumerator ShowResults(float time, string sentence, string sentenceTwo, string sentenceThree)
    {
        details.text = sentence;
        yield return new WaitForSeconds(time);
        details.text = sentenceTwo;
        yield return new WaitForSeconds(time + 0.5f);
        if (sentenceThree != null && ShopManager.TotalInShop() > 0)
        {
            details.text = sentenceThree;
            yield return new WaitForSeconds(time);
        }
        SceneManager.LoadScene(GameManager.lastScene);
    }
}