using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Main()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GoStage1()
    {
        SceneManager.LoadScene("Stage1Scene");
    }

    public void GoStage2()
    {
        SceneManager.LoadScene("Stage2Scene");
    }

    public void GoStage3()
    {
        SceneManager.LoadScene("Stage3Scene");
    }
    public void GoStage4()
    {
        SceneManager.LoadScene("Stage4Scene");
    }
}
