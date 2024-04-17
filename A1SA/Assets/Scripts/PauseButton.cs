using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Sprite pauseImg;
    public Sprite playImg;

    public void PauseGame()
    {
        // 게임이 일시정지 상태일 때, 다시 Play
        if (GameManager.Instance.isPause)
        {
            GameManager.Instance.isPause = false;
            GetComponent<Image>().sprite = pauseImg;
        }

        // 게임이 Play 상태일 때, 다시 Pause
        else
        {
            GameManager.Instance.isPause = true;
            GetComponent<Image>().sprite = playImg;
        }
    }
}
