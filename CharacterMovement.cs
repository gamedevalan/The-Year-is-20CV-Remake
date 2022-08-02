using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class CharacterMovement : MonoBehaviour
{
    readonly float moveSpeed = 90f;
    bool faceLeft = true;
    private static string facingDirection = "left";
    private GameObject character;
    private Animator animator;

    public Rigidbody rb;

    // Animations for player
    public Animator taiAnimation;
    public Animator annaAnimation;

    // Access the indidual sprites
    public GameObject tai;
    public GameObject anna;
    public int num;

    Color mansionRoom; //8E6767;
    Color bar; //FFBF91c
    Color outside; //A9A9A9

    // Imported sprites are different sizes, so treat them differently
    private readonly float taiScale = 0.7f;
    private readonly float annaScale = 0.9f;

    // Attach prompt canvas
    public GameObject interactPrompt;
    public Text prompt;

    public static Vector3 currPos;

    public static int costume;

    public static float taiHealth = 100;
    public static float annaHealth = 100;

    public static float taiAttack = 50;
    public static float annaAttack = 50;

    public static float taiDefense = 40;
    public static float annaDefense = 40;

    public static float taiCritDamMultiplier = 1.2f;
    public static float annaCritDamMultiplier = 1.2f;

    public static bool critRateCharmBought;
    public static bool healCharmBought;

    // Starting position when game starts
    public static Vector3 setPos = new Vector3(-3f, 0.5f, 2.4f);

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#8E6767", out mansionRoom);
        ColorUtility.TryParseHtmlString("#FFBF91", out bar);
        ColorUtility.TryParseHtmlString("#A9A9A9", out outside);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.position = setPos;
        num = GameManager.costume;

        interactPrompt.SetActive(false);
        taiAnimation.keepAnimatorControllerStateOnDisable = true;
        annaAnimation.keepAnimatorControllerStateOnDisable = true;
        if (num == 0)
        {
            character = tai;
            animator = taiAnimation;
        }
        else
        {
            character = anna;
            animator = annaAnimation;
        }

        if (SceneManager.GetActiveScene().name.IndexOf("stairs") != -1)
        {
            tai.GetComponent<Renderer>().material.color = mansionRoom;
            anna.GetComponent<Renderer>().material.color = mansionRoom;
        }
        else if (SceneManager.GetActiveScene().name == "Bar")
        {
            tai.GetComponent<Renderer>().material.color = bar;
            anna.GetComponent<Renderer>().material.color = bar;
        }
        else if (SceneManager.GetActiveScene().name.IndexOf("Outside") != -1)
        {
            tai.GetComponent<Renderer>().material.color = outside;
            anna.GetComponent<Renderer>().material.color = outside;
        }
        else
        {
            tai.GetComponent<Renderer>().material.color = Color.white;
            anna.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        currPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameManager.costume = num;
        ChangeCharacters();
        FaceDirection();
    }

    // Change appearance of player based on input
    private void ChangeCharacters()
    {
        if (num == 0)
        {
            tai.SetActive(true);
            anna.SetActive(false);
            animator = taiAnimation;
            character = tai;
        }
        else
        {
            tai.SetActive(false);
            anna.SetActive(true);
            animator = annaAnimation;
            character = anna;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (num != 0) {
                num = 0;
                float temp = character.transform.localScale.x / Mathf.Abs(character.transform.localScale.x);
                character = tai;
                ResetLookingDirection(character, temp, taiScale);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (num != 1) {
                num = 1;
                float temp = character.transform.localScale.x / Mathf.Abs(character.transform.localScale.x);
                character = anna;
                ResetLookingDirection(character, temp, annaScale);
            }
        }
    }

    // Keep the same facing direction
    private void ResetLookingDirection(GameObject character, float num, float scale)
    {
        character.transform.localScale = new Vector3(scale * num, scale, scale);
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(transform.right * moveSpeed);
            facingDirection = "right";
            animator.SetFloat("Speed", 1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-transform.right * moveSpeed);
            facingDirection = "left";
            animator.SetFloat("Speed", 1);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.forward * moveSpeed);
            animator.SetFloat("Speed", 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-transform.forward * moveSpeed);
            animator.SetFloat("Speed", 1);
        }
        else
        {
            // Set back to idle animation
            if ((animator != null && animator.isActiveAndEnabled))
            {
                animator.SetFloat("Speed", 0);
            }
        }
    }

    private void FaceDirection()
    {
        if (facingDirection.Equals("right") && faceLeft)
        {
            faceLeft = !faceLeft;
            if (num == 0)
            {
                character.transform.localScale = new Vector3(-taiScale, taiScale, taiScale);
            }
            else
            {
                character.transform.localScale = new Vector3(-annaScale, annaScale, annaScale);
            }
        }

        else if (facingDirection.Equals("left") && !faceLeft)
        {
            faceLeft = !faceLeft;
            if (num == 0)
            {
                character.transform.localScale = new Vector3(taiScale, taiScale, taiScale);
            }
            else
            {
                character.transform.localScale = new Vector3(annaScale, annaScale, annaScale);
            }
        }
    }

    // Check which object to interact with
    private void OnTriggerStay(Collider other)
    {
        interactPrompt.SetActive(true);
        prompt.text = "Press Space";
        prompt.fontSize = 30;
        switch (other.gameObject.tag)
        {
            case "SignL":
            case "SignD":
                if (Input.GetKey(KeyCode.Space))
                {
                    Interactables.ShowDialogue(other.gameObject.tag, null);
                }
                break;
            case "Chair":
                if (Input.GetKey(KeyCode.Space))
                {
                    Interactables.ShowDialogue(other.gameObject.tag, null);
                }
                break;
            case "Bottle":
            case "KeyItem":
                prompt.text += " to pick up";
                prompt.fontSize = 25;
                if (Input.GetKey(KeyCode.Space))
                {
                    Interactables.ShowDialogue(other.gameObject.tag, other.name);
                    other.gameObject.GetComponent<PickupItem>().SetPickedUp();
                    interactPrompt.SetActive(false);
                }
                break;
            case "Enemy":
                if (Input.GetKey(KeyCode.Space))
                {
                    Interactables.ShowDialogue(other.gameObject.name, null);
                }
                break;
            case "Shop":
                if (Input.GetKey(KeyCode.Space))
                {
                    Interactables.ShowDialogue(other.gameObject.tag, null);
                }
                break;
            case "End":
                if (Input.GetKey(KeyCode.Space))
                {
                    Interactables.ShowDialogue(other.gameObject.tag, null);
                }
                break;
            default:
                prompt.text += " to go";
                if (Input.GetKey(KeyCode.Space))
                {
                    Interactables.ShowDialogue("Door", other.gameObject.tag);
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactPrompt.SetActive(false);
        prompt.text = "";
    }

    public static void ChangeHealth()
    {
        taiHealth += 10;
        annaHealth += 10;
    }
}