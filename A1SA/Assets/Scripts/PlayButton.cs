using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public GameObject stopPanel;
    public void PauseGame()
    {
        //�ð��� �����ִٸ�
        if(Time.timeScale == 0f)
        {
            //�ð��� �ٽ� ���
            Time.timeScale = 1f;
            stopPanel.SetActive(false);
        }
    }
}
