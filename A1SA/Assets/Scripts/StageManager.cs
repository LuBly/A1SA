using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    // Save��� ������ ���� Singleton���� ����
    public static StageManager Instance;
    [Header("������ üũ")]
    public int[] i_dataArr = new int[5];
    public string[] dataArr;

    string key = "StageData";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadData()
    {
        // PlayerPref�� ����� ������ ���� üũ

        // PlayerPref�� ����� Data�� ���ٸ� ���� ����
        if (!PlayerPrefs.HasKey(key))
        {
            //idx 0[�ٸ� Scene���� �����ϱ� �����ϰ� ���� �� �Է�] 1 2 3 4 => Stage�� ��, ���� �Ǻ�
            // �ѹ��� ��� �����͸� �����ϱ⿡ �� ������� ����.
            string initData = "0,1,0,0,0";
            PlayerPrefs.SetString(key, initData);
        }

        dataArr = PlayerPrefs.GetString(key).Split(',');
        for (int k = 1; k < dataArr.Length; k++)
        {
            i_dataArr[k] = System.Convert.ToInt32(dataArr[k]);
        }
    }

    public void SaveData(bool isOpen, int idx)
    {
        LoadData();
        string data = "0,";
        for(int k = 1; k < i_dataArr.Length; k++)
        {
            if(k == idx)
            {
                if (isOpen)
                {
                    dataArr[k] = "1";
                }
                else
                {
                    dataArr[k] = "0";
                }
            }

            data += dataArr[k];
                
            if(k < i_dataArr.Length - 1)
            {
                data += ",";
            }
        }

        PlayerPrefs.SetString(key, data);
    }

    public void LoadScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    public void SelectStage()
    {
        this.gameObject.SetActive(true);
    }
}
