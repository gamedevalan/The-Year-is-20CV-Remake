using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSign : MonoBehaviour
{

    public GameObject inspectorCameraLeft;
    public GameObject inspectorCameraDown;
    public GameObject inspectorMainCamera;
    public GameObject inspectorPlayer;
    public GameObject inspectorCurrCamera;

    private static GameObject cameraLeft;
    private static GameObject cameraDown;
    private static GameObject mainCamera;
    private static GameObject player;
    private static GameObject currCamera;

    public GameObject inspectorPrompt;
    private static GameObject prompt;

    public static ShowSign instance;

    private void Awake()
    {
        instance = this;    
    }

    private void Start()
    {
        cameraLeft = inspectorCameraLeft;
        cameraDown = inspectorCameraDown;
        mainCamera = inspectorMainCamera;
        player = inspectorPlayer;
        currCamera = inspectorMainCamera;
        prompt = inspectorPrompt;
    }

    public static void ShowSigns(string whichSign)
    {
        player.SetActive(false);
        mainCamera.SetActive(false);
        prompt.SetActive(false);
        if (whichSign.Equals("SignL"))
        {
            ShowLeftSign();
            instance.StartCoroutine("HideCamera");
        }
        else
        {
            ShowDownSign();
            instance.StartCoroutine("HideCamera");
        }
    }

    private static void ShowLeftSign()
    {
        cameraLeft.SetActive(true);
        cameraDown.SetActive(false);
        currCamera = cameraLeft;
    }

    private static void ShowDownSign()
    {
        cameraDown.SetActive(true);
        cameraLeft.SetActive(false);
        currCamera = cameraDown;
    }

    IEnumerator HideCamera()
    {
        yield return new WaitForSeconds(2f);
        currCamera.SetActive(false);
        mainCamera.SetActive(true);
        player.SetActive(true);
        prompt.SetActive(true);
    }
}
