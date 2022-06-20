using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    readonly float moveSpeed = 5f;
    bool faceLeft = true;
    string facingDirection = "left";
    private GameObject character;
    private Animator animator;

    // Animations for player
    public Animator taiAnimation;
    public Animator annaAnimation;

    // Access the indidual sprites
    public GameObject tai;
    public GameObject anna;
    public int num;

    // Imported sprites are different sizes, so treat them differently
    private Vector3 taiScale;
    private Vector3 annaScale;

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
    public static Vector3 setPos= new Vector3(0.07f, 0.5f, 1.5f);

    private void Start()
    {
        transform.position = setPos;
        interactPrompt.SetActive(false);
        taiScale = new Vector3(tai.transform.localScale.x, tai.transform.localScale.y, tai.transform.localScale.z);
        annaScale = new Vector3(anna.transform.localScale.x, anna.transform.localScale.y, anna.transform.localScale.z);
        taiAnimation.keepAnimatorControllerStateOnDisable = true;
        annaAnimation.keepAnimatorControllerStateOnDisable = true;
        character = tai;
        num = Door.costume;
    }

    // Update is called once per frame
    void Update()
    {
        currPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Door.costume = num;
        ChangeCharacters();
        Movement();
        FaceDirection();

        // Delete Later
        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log("TAI STATS:");
            Debug.Log("Health: "+taiHealth);
            Debug.Log("Attack: " + taiAttack);
            Debug.Log("Defense: " + taiDefense);
            Debug.Log("CD M: " + taiCritDamMultiplier);
            Debug.Log("Charm Bought: "+ critRateCharmBought);
            Debug.Log("ANNA STATS:");
            Debug.Log("Health: " + annaHealth);
            Debug.Log("Attack: " + annaAttack);
            Debug.Log("Defense: " + annaDefense);
            Debug.Log("CD M: " + annaCritDamMultiplier);
            Debug.Log("Charm Bought: " + critRateCharmBought);
        }
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
                character = tai;
                ResetLookingDirection(taiScale);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (num != 1) {
                num = 1;
                character = anna;
                ResetLookingDirection(annaScale);
            }
        }
    }

    private void ResetLookingDirection(Vector3 scale)
    {
        character.transform.localScale = scale;
        faceLeft = true;
        facingDirection = "left";
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            facingDirection = "right";
            animator.SetFloat("Speed", 1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            facingDirection = "left";
            animator.SetFloat("Speed", 1);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0,0, moveSpeed * Time.deltaTime);
            animator.SetFloat("Speed", 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0,0 ,-moveSpeed * Time.deltaTime);
            animator.SetFloat("Speed", 1);
        }
        else
        {
            // Set back to idle animation
            animator.SetFloat("Speed", 0);
        }
    }

    private void FaceDirection()
    {
        if (facingDirection.Equals("right") && faceLeft)
        {
            faceLeft = !faceLeft;
            if (num == 0)
            {
                character.transform.localScale = new Vector3(-taiScale.x, taiScale.y, taiScale.z);
            }
            else
            {
                character.transform.localScale = new Vector3(-annaScale.x, annaScale.y, annaScale.z);
            }
        }

        else if (facingDirection.Equals("left") && !faceLeft)
        {
            faceLeft = !faceLeft;
            if (num == 0)
            {
                character.transform.localScale = new Vector3(taiScale.x, taiScale.y, taiScale.z);
            }
            else
            {
                character.transform.localScale = new Vector3(annaScale.x, annaScale.y, annaScale.z);
            }
        }
    }

    // Check which object to interact with
    private void OnTriggerStay(Collider other)
    {
        interactPrompt.SetActive(true);
        prompt.text = "Press Space";
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
                if (Input.GetKey(KeyCode.Space))
                {
                    Interactables.ShowDialogue(other.gameObject.tag, other.name);
                    other.gameObject.GetComponent<PickupItem>().SetPickedUp();
                    //Destroy(other.transform.parent.gameObject);
                    interactPrompt.SetActive(false);
                }
                break;
            case "Virus-SM":
            case "Bat":
                if (Input.GetKey(KeyCode.Space))
                {
                    Interactables.ShowDialogue(other.gameObject.tag, null);
                    Destroy(other.transform.gameObject);
                }
                break;
            case "Shop":
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


/*
 
      private void ChangeCharacters()
    {
        if (num == 0)
        {
            tai.SetActive(true);
            anna.SetActive(false);
            animator = taiAnimation;
        }
        else
        {
            tai.SetActive(false);
            anna.SetActive(true);
            animator = annaAnimation;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (num != 0) {
                num = 0;
                //character = tai;
                ResetLookingDirection(character.transform.localScale.x);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (num != 1) {
                num = 1;
                //character = anna;
                ResetLookingDirection(character.transform.localScale.x);
            }
        }
    }

    private void ResetLookingDirection(float scale)
    {
        Debug.Log(character);
        if (scale < 0)
        {
            if (num == 0)
            {
                character = tai;
            }
            else
            {
                character = anna;
            }
            Debug.Log("Scale is negative"+character);
            character.transform.localScale = new Vector3(-character.transform.localScale.x, character.transform.localScale.y, character.transform.localScale.z);

        }
        else
        {
            if (num == 0)
            {
                character = tai;
            }
            else
            {
                character = anna;
            }
            Debug.Log("Scale is positive" + character);
            character.transform.localScale = new Vector3(character.transform.localScale.x, character.transform.localScale.y, character.transform.localScale.z);
        }
        //character.transform.localScale = scale;
        //faceLeft = true;
        //facingDirection = "left";
    }
     */
