using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject stopPanel;
    public void PauseGame()
    {
        //Ω√∞£¿Ã »Â∏£∞Ì ¿÷¥Ÿ∏È
        if(Time.timeScale == 1f)
        {
            //Ω√∞£¿ª ∏ÿ√„
            Time.timeScale = 0f;
            stopPanel.SetActive(true);
        }
    }
}
