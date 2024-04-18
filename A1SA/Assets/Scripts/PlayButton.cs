using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public GameObject stopPanel;
    public void PauseGame()
    {
        //시간이 멈춰있다면
        if(Time.timeScale == 0f)
        {
            //시간을 다시 재생
            Time.timeScale = 1f;
            stopPanel.SetActive(false);
        }
    }
}
