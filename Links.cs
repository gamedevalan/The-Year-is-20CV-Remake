using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Links : MonoBehaviour
{
    public void OpenLinkMyYoutube()
    {
#if !UNITY_EDITOR 
        OpenTab("https://www.youtube.com/channel/UCvhl4lva5m_kgnWMlr05-rw/featured");
#endif
    }

    public void OpenLinkMyInstagram()
    {
#if !UNITY_EDITOR 
        OpenTab("https://www.instagram.com/gamedev_alan/");
#endif
    }

    public void OpenMusicYoutube()
    {
#if !UNITY_EDITOR 
        OpenTab("https://www.youtube.com/c/HeatleyBros/featured");
#endif
    }

    [DllImport("__Internal")]
    private static extern void OpenTab(string url);

}