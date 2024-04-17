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
        // ������ �Ͻ����� ������ ��, �ٽ� Play
        if (GameManager.Instance.isPause)
        {
            GameManager.Instance.isPause = false;
            GetComponent<Image>().sprite = pauseImg;
        }

        // ������ Play ������ ��, �ٽ� Pause
        else
        {
            GameManager.Instance.isPause = true;
            GetComponent<Image>().sprite = playImg;
        }
    }
}
