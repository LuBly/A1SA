using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject stopPanel;
    public void PauseGame()
    {
        //�ð��� �帣�� �ִٸ�
        if(Time.timeScale == 1f)
        {
            //�ð��� ����
            Time.timeScale = 0f;
            stopPanel.SetActive(true);
        }
    }
}
