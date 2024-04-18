using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUIController : MonoBehaviour
{
    public GameObject[] stageBtns;
    public TextMeshProUGUI[] stageTxts;
    public Image[] clearFailImg;

    public Sprite clear;
    public Sprite fail;

    private void Start()
    {
        CheckStage();
    }

    public void CheckStage()
    {
        // SaveData update
        StageManager.Instance.LoadData();

        for (int idx = 1; idx < StageManager.Instance.dataArr.Length; idx++)
        {
            // 클리어라면 클리어 셋팅 적용
            if (StageManager.Instance.i_dataArr[idx] == 1)
            {
                SetClearStage(idx);
            }
            // 클리어X라면 클리어X 셋팅 적용
            else
            {
                SetFailStage(idx);
            }
        }
    }

    public void SetClearStage(int idx)
    {
        // text.Color = 0 0 0 255
        stageTxts[idx].color = new Color32(0, 0, 0, 255);

        // 좌측 상단 image활성화[왕관]
        clearFailImg[idx].sprite = clear;

        // 버튼 기능 활성화
        stageBtns[idx].GetComponent<Button>().enabled = true;
    }

    public void SetFailStage(int idx)
    {
        // text.Color = 0 0 0 150
        stageTxts[idx].color = new Color32(0, 0, 0, 150);
        // 좌측 상단 image활성화[자물쇠]
        clearFailImg[idx].sprite = fail;
        // 버튼 Image.Color = 255 255 255 150
        stageBtns[idx].GetComponent<Image>().color = new Color32(255, 255, 255, 150);
    }
}
