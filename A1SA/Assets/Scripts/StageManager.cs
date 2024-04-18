using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    // Save기능 제작을 위해 Singleton으로 제작
    public static StageManager Instance;
    [Header("데이터 체크")]
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
        // PlayerPref에 저장된 데이터 정보 체크

        // PlayerPref에 저장된 Data가 없다면 새로 생성
        if (!PlayerPrefs.HasKey(key))
        {
            //idx 0[다른 Scene에서 접근하기 용이하게 더미 값 입력] 1 2 3 4 => Stage별 참, 거짓 판별
            // 한번에 모든 데이터를 관리하기에 좀 어려움이 있음.
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
