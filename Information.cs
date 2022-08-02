using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Information : MonoBehaviour
{
    public GameObject information;
    public Sprite exit;
    public Sprite exclamation;

    public bool mouseHoverOver;

    private bool isOpen;

    public GameObject defaultInfo;
    public GameObject KarenInfo;
    public GameObject SpringBreakersInfo;
    public GameObject VampireInfo;
    public GameObject VirusBossInfo;
    public GameObject nextButton;

    public bool showingDefaultInfo = true;
    public GameObject bossInfo;

    private void Start()
    {
        if (BattleManager.isBoss)
        {
            nextButton.SetActive(true);
            if (GameManager.lastScene == "Store")
            {
                bossInfo = KarenInfo;
            }
            else if(GameManager.lastScene == "Beach")
            {
                bossInfo  = SpringBreakersInfo;
            }
            else if (GameManager.lastScene == "Mansion_Upstairs")
            {
                bossInfo = VampireInfo;
            }
            else if (GameManager.lastScene == "Outside")
            {
                bossInfo = VirusBossInfo;
            }
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!mouseHoverOver && isOpen)
            {
                InformationClicked();
            }
        }    
    }

    public void InformationClicked()
    {
        if (isOpen)
        {
            gameObject.GetComponent<Image>().sprite = exclamation;
            information.SetActive(false);
            isOpen = false;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = exit;
            information.SetActive(true);
            isOpen = true;
        }
    }

    public void ChangeInformation()
    {
        if (showingDefaultInfo)
        {
            bossInfo.SetActive(true);
            defaultInfo.SetActive(false);
            showingDefaultInfo = false;
        }
        else
        {
            bossInfo.SetActive(false);
            defaultInfo.SetActive(true);
            showingDefaultInfo = true;
        }
    }

    public void OnMouseOver()
    {
        mouseHoverOver = true;
    }

    public void OnMouseExit()
    {
        mouseHoverOver = false;
    }
}
