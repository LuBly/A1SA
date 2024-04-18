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
            // Ŭ������ Ŭ���� ���� ����
            if (StageManager.Instance.i_dataArr[idx] == 1)
            {
                SetClearStage(idx);
            }
            // Ŭ����X��� Ŭ����X ���� ����
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

        // ���� ��� imageȰ��ȭ[�հ�]
        clearFailImg[idx].sprite = clear;

        // ��ư ��� Ȱ��ȭ
        stageBtns[idx].GetComponent<Button>().enabled = true;
    }

    public void SetFailStage(int idx)
    {
        // text.Color = 0 0 0 150
        stageTxts[idx].color = new Color32(0, 0, 0, 150);
        // ���� ��� imageȰ��ȭ[�ڹ���]
        clearFailImg[idx].sprite = fail;
        // ��ư Image.Color = 255 255 255 150
        stageBtns[idx].GetComponent<Image>().color = new Color32(255, 255, 255, 150);
    }
}
