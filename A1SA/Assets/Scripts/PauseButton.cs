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
        //일시정지 상태인 경우
        if(Time.timeScale == 0f)
        {
            //일시정지 상태를 해제하고 시간을 다시 진행
            Time.timeScale = 1f;
            //pauseImg를 불러온다
            GetComponent<Image>().sprite = pauseImg;
        }
        //일시정지 상태가 아닌경우
        else
        {
            //일시정지 상태로 만들고 시간을 멈춤
            Time.timeScale = 0f;
            //playImg를 불러온다
            GetComponent<Image>().sprite = playImg;
        }
    }
}
