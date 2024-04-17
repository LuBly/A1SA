using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public void LoadScene(int idx)
    {
        Debug.Log(idx);
        SceneManager.LoadScene(idx);
    }

    public void SelectStage()
    {
        this.gameObject.SetActive(true);
    }
}
