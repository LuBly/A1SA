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
        //�Ͻ����� ������ ���
        if(Time.timeScale == 0f)
        {
            //�Ͻ����� ���¸� �����ϰ� �ð��� �ٽ� ����
            Time.timeScale = 1f;
            //pauseImg�� �ҷ��´�
            GetComponent<Image>().sprite = pauseImg;
        }
        //�Ͻ����� ���°� �ƴѰ��
        else
        {
            //�Ͻ����� ���·� ����� �ð��� ����
            Time.timeScale = 0f;
            //playImg�� �ҷ��´�
            GetComponent<Image>().sprite = playImg;
        }
    }
}
