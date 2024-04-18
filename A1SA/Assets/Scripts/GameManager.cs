using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;


    [Header("메인 화면 관련")]
    public TextMeshProUGUI timeText;

    public GameObject nameText;
    public GameObject penaltyTimeText;

    public GameObject endPanel;

    public Text mainNowScore;
    public Text mainBestScore;

    [Header("엔드 패널 관련")]
    public Text matchTryTxt;
    public Text matchSuccessTxt;
    public Text reminingTxt;
    public Text nowScore;
    public Text bestScore;

    public Animator endAnim;

    // 결과창 
    [Header("결과창이 남아있는 시간")]
    public float resultDelay = 0.5f;
    [Header("실패시 줄어드는 시간")]
    public float penaltyTime = 2.0f;

    [Header("실패 시 빨간색으로 깜빡이는 시간")]
    public float penaltyDelay = 0.2f;

    // 카드 수 계산 변수
    public int cardCount = 0;
    public int matchCount = 0;
    public int matchSuccess = 0;

    public bool isReady = false;

    public string[] userNames = new string[5];
    
    [Header("Success Audio")]
    public AudioClip clip;

    public int stageIdx;
    Scene scene;


    AudioSource audioSource;

    // 최고 점수 배열
    string[] key = { "bestScore","BestScore1", "BestScore2", "BestScore3", "BestScore4" };

    float time = 0.0f;
    float reminingTime = 0.0f;
    float stageTime;
    int score = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        stageIdx = scene.buildIndex;
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;

        switch (stageIdx)
        {
            case 1:
                stageTime = 30f;
                break;
            case 2:
                stageTime = 40f;
                break;
            case 3:
                stageTime = 50f;
                break;
            case 4:
                stageTime = 60f;
                break;
        }

        reminingTime = stageTime;
    }

    private void Update()
    {
        if (isReady)
            time += Time.deltaTime;
        timeText.text = time.ToString("N2");

        // 엔드 패널 텍스트 형변환
        matchTryTxt.text = matchCount.ToString();
        matchSuccessTxt.text = matchSuccess.ToString();
        reminingTxt.text = reminingTime.ToString("N2");

        // 최고 점수 로직
        BestScore();

        //일정시간 경과시 경고
        if (time >= stageTime - 10.0f)
        {
            //Text를 빨간색으로
            timeText.color = Color.red;
        }
        if (time >= stageTime)
        {
            time = stageTime;
            reminingTime = 0.0f;
            endAnim.SetBool("EndPanel", true);
            Invoke("GameEnd", 0.3f);
            TimeScore();
            GameOver();
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            int userIdx = firstCard.idx % 5;
            // 0.5초 동안 성공한 user 이름 노출
            nameText.SetActive(true);
            nameText.GetComponent<TextMeshProUGUI>().text = "나는 " + userNames[userIdx];
            nameText.GetComponent<TextMeshProUGUI>().color = Color.white;
            Invoke("CloseNameText", resultDelay);
            // 카드 삭제
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            score += 5;
            matchSuccess += 1;

            //게임 종료
            if (cardCount == 0)
            {
                reminingTime -= time;
                endAnim.SetBool("EndPanel", true);
                TimeScore();
                GameOver();
            }
        }
        else
        {
            penaltyTimeText.SetActive(true);
            // 0.5초 동안 timer 색 red로 변경
            nameText.SetActive(true);
            nameText.GetComponent<TextMeshProUGUI>().text = "실패ㅋ";
            nameText.GetComponent<TextMeshProUGUI>().color = Color.red;
            Invoke("CloseNameText", resultDelay);
            // 시간 감소 (2초)
            time += penaltyTime;
            StartCoroutine(ActiveTimePenalty(penaltyDelay));

            // 카드 닫기.
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        matchCount += 1;

        firstCard = null;
        secondCard = null;
    }

    public void GameOver()
    {
        Invoke("GameEnd", 0.3f);
        endPanel.SetActive(true);
    }

    public void BestScore()
    {
        if (PlayerPrefs.HasKey(key[stageIdx]))
        {
            int best = PlayerPrefs.GetInt(key[stageIdx]);
            if (best < score)
            {
                PlayerPrefs.SetInt(key[stageIdx], score);
                bestScore.text = score.ToString();
                mainBestScore.text = score.ToString();
            }
            else
            {
                bestScore.text = best.ToString();
                mainBestScore.text = best.ToString();
            }

        }
        else
        {
            PlayerPrefs.SetInt(key[stageIdx], score);
            bestScore.text = score.ToString();
            mainBestScore.text = score.ToString();
        }
        //이번판 점수 저장
        nowScore.text = score.ToString();
        endPanel.SetActive(true);

        // 클리어 여부 저장
        // 클리어
        if(time > 0f)
        {
            if(stageIdx <= 4)
                StageManager.Instance.SaveData(true, stageIdx + 1);
        }
        mainNowScore.text = score.ToString();
    }

    IEnumerator ActiveTimePenalty(float penaltyDelay)
    {
        timeText.color = Color.red;
        yield return new WaitForSeconds(penaltyDelay);
        timeText.color = Color.white;
    }

    public float GetStageTime()
    {
        return stageTime;
    }

    public float GetCurrentTime()
    {
        return time;
    }

    public void CloseNameText()
    {
        nameText.SetActive(false);
        penaltyTimeText.SetActive(false);
    }

    public void GameEnd()
    {
        Time.timeScale = 0.0f;
    }

    public void TimeScore()
    {
        if (reminingTime >= 30f)
        {
            score += 60;
        }
        else if (reminingTime >= 25f)
        {
            score += 50;
        }
        else if (reminingTime >= 20f)
        {
            score += 40;
        }
        else if (reminingTime >= 15f)
        {
            score += 30;
        }
        else if (reminingTime >= 10f)
        {
            score += 20;
        }
        else if (reminingTime >= 5f)
        {
            score += 10;
        }
    }
}