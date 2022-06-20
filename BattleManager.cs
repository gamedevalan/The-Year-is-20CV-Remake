using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public GameObject Tai;
    public GameObject Anna;

    private PlayerStats TaiStats;
    private PlayerStats AnnaStats;

    public static string enemy;
    private GameObject currEnemy;
    private EnemyStats currEnemyStats;
    static int num;

    static public BattleManager instance;

    public GameObject[] enemies;

    public enum BattleState { TAI_TURN, ANNA_TURN,ENEMY_TURN, WON, LOST}
    private BattleState state;

    public GameObject TaiChoices;
    public GameObject AnnaChoices;

    public GameObject TaiAttackMoveObject;
    public GameObject AnnaAttackMoveObject;

    public Text TaiHealth;
    public Text AnnaHealth;
    public Text EnemyHealth;

    Vector3 defaultpos = new Vector3(4,1,0);

    public static string lastScene;

    private void Awake()
    {
        instance = this;
        currEnemy = Instantiate(enemies[num]);
        TaiStats = Tai.GetComponent<PlayerStats>();
        AnnaStats = Anna.GetComponent<PlayerStats>();
        currEnemyStats = currEnemy.GetComponent<EnemyStats>();
    }

    private void Start()
    {
        state = BattleState.TAI_TURN;
        TaiChoices.SetActive(true);
        AnnaChoices.SetActive(false);
        PlayerTurn();
    }

    private void Update()
    {
        TaiHealth.text = "Tai Health: "+TaiStats.GetHealth()+" /"+ CharacterMovement.taiHealth;
        AnnaHealth.text = "Anna Health: " + AnnaStats.GetHealth()+ " /" + CharacterMovement.annaHealth;
        EnemyHealth.text = currEnemyStats.GetName()+" Health: " + currEnemyStats.GetCurrHealth() + " /" +currEnemyStats.GetTotalHealth();
    }

    public static void SetScene(string scene)
    {
        lastScene = scene;
    }

    public static void SetEnemy(string enemyType)
    {
        switch (enemyType) {
            case "Virus-SM":
                num = 0;
                break;
            case "Bat":
                num = 1;
                break;
        }

        SceneManager.LoadScene("Battle");
    }
    
    private void PlayerTurn()
    {
        Debug.Log("Choose what to do");
        TaiStats.SetShield(false);
        AnnaStats.SetShield(false);
    }

    public void TaiAttackButton()
    {
        //if(state != BattleState.TAI_TURN)
        //{
        //    return;
        //}

        TaiPlayerAttack();

    }

    public void AnnaAttackButton()
    {
        //if (state != BattleState.ANNA_TURN)
        //{
        //    return;
        //}

        AnnaPlayerAttack();
    }

    public void TaiHealButton()
    {
        TaiStats.HealTai();
        AnnaChoices.SetActive(false);
        StartCoroutine("DoAnimation", "Anna");
    }

    public void AnnaHealButton()
    {
        AnnaStats.HealAnna();
        AnnaChoices.SetActive(false);
        StartCoroutine("DoAnimation", "Anna");
    }

    private void TaiPlayerAttack()
    {
        Debug.Log("Tai Attacks");
        TaiChoices.SetActive(false);
        Tai.transform.position = new Vector3(currEnemy.transform.position.x + 2, currEnemy.transform.position.y, currEnemy.transform.position.z);
        Tai.GetComponent<Animator>().SetTrigger("Attack");
        float dam = TaiStats.Attack(currEnemyStats.GetDefense());
        Debug.Log("Attack Tai Amount: " + dam);
        currEnemyStats.health -= dam;
        StartCoroutine("DoAnimation", "Tai");
    }

    private void AnnaPlayerAttack()
    {
        Debug.Log("Anna Attacks");
        AnnaChoices.SetActive(false);
        Anna.GetComponent<Animator>().SetTrigger("Attack");
        float dam = AnnaStats.Attack(currEnemyStats.GetDefense());
        Debug.Log("Attack Anna Amount: " + dam);
        currEnemyStats.health -= dam;
        StartCoroutine("DoAnimation", "Anna");
    }

    public void TaiShield()
    {
        TaiStats.SetShield(true);
        TaiChoices.SetActive(false);
        StartCoroutine("DoAnimation", "Tai");
    }

    public void AnnaShield()
    {
        AnnaStats.SetShield(true);
        AnnaChoices.SetActive(false);
        StartCoroutine("DoAnimation", "Anna");
    }

    IEnumerator DoAnimation(string character)
    {
        yield return new WaitForSeconds(2f);
        Tai.transform.position = defaultpos;
        bool isDead = currEnemyStats.health <= 0;
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            if (character.Equals("Tai"))
            {
                if (AnnaStats.GetHealth() > 0) {
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
        Debug.Log("Enemy Attacks");
        bool taiIsDead = false;
        if (!TaiStats.IsShielding())
        {
            taiIsDead = TaiStats.GetHealth() > 0 ? TaiStats.TakeDamage(currEnemyStats.Attack(TaiStats.GetDefense())) : true;

        }
        bool annaIsDead = false;
        if (!AnnaStats.IsShielding())
        {
            annaIsDead = AnnaStats.GetHealth() > 0 ? AnnaStats.TakeDamage(currEnemyStats.Attack(AnnaStats.GetDefense())) : true;

        }
        bool isDead = taiIsDead && annaIsDead;
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            if (!taiIsDead) {
                state = BattleState.TAI_TURN;
                TaiChoices.SetActive(true);
            }
            else
            {
                state = BattleState.ANNA_TURN;
                AnnaChoices.SetActive(true);
            }
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        if (state == BattleState.WON)
        {
            Debug.Log("Winner");
            TaiChoices.SetActive(false);
            AnnaChoices.SetActive(false);
            Inventory.currMoney += 100;
        }
        else
        {
            Debug.Log("Loser");
            TaiChoices.SetActive(false);
            AnnaChoices.SetActive(false);
        }
        SceneManager.LoadScene(lastScene);
    }

}
