using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public TextMeshProUGUI timeText;
    public GameObject endPanel;

    public Text resultText;
    public Text resultText2;
    public Text reminingTxt;
    public Text scoreTxt;
    public Text nowScore;
    public Text bestScore;
    public GameObject nameText;

    public Animator endAnim;

    // 결과창 
    [Header("결과창이 남아있는 시간")]
    public float resultDelay = 0.5f;
    [Header("실패시 줄어드는 시간")]
    public float penaltyTime = 2.0f;

    [Header("실패 시 빨간색으로 깜빡이는 시간")]
    public float penaltyDelay = 0.2f;

    public int cardCount = 0;
    public int matchCount = 0;
    public int matchSuccess = 0;

    public AudioClip clip;

    public bool isReady = false;

    public string[] userNames = new string[5];

    AudioSource audioSource;
    
    int score = 0;
    string key = "bestScore";
    float time = 0.0f;
    float reminingTime = 30.0f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isReady)
            time += Time.deltaTime;
        timeText.text = time.ToString("N2");

        //텍스트 형변환
        resultText.text = matchCount.ToString();
        resultText2.text = matchSuccess.ToString();
        reminingTxt.text = reminingTime.ToString("N2");
        scoreTxt.text = score.ToString();
        //일정시간 경과시 경고
        if (time >= 20.0f)
        {
            //Text를 빨간색으로
            timeText.color = Color.red;
        }
        if (time >= 30.0f)
        {
            time = 30.0f;
            reminingTime = 0.0f;
            Invoke("EndGame", 0.3f);
            endPanel.SetActive(true);
            endAnim.SetBool("EndPanel", true);
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            int userIdx = firstCard.idx % 5;
            audioSource.PlayOneShot(clip);
            // 0.5초 동안 성공한 user 이름 노출
            nameText.SetActive(true);
            nameText.GetComponent<TextMeshProUGUI>().text = "나는 " + userNames[userIdx];
            nameText.GetComponent<TextMeshProUGUI>().color = Color.white;
            Invoke("CloseNameText", resultDelay);
            // 카드 삭제
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            matchSuccess += 1;
            score += 5;
            if (cardCount == 0)
            {
                Invoke("EndGame", 0.3f);
                reminingTime -= time;
                endPanel.SetActive(true);
                endAnim.SetBool("EndPanel", true);
                GameOver();
                TimeScore();
            }
        }
        else
        {
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

        // ī�� �ʱ�ȭ
        firstCard = null;
        secondCard = null;
    }

    public void GameOver()
    {
        //최소 점수 저장
        if (PlayerPrefs.HasKey(key))
        {
            float best = PlayerPrefs.GetFloat(key);
            if (best > time)
            {
                PlayerPrefs.SetFloat(key, time);
                bestScore.text = time.ToString("N2");
            }
            else
            {
                bestScore.text = best.ToString("N2");

            }

        }
        else
        {
            PlayerPrefs.SetFloat(key, time);
            bestScore.text = time.ToString("N2");
        }
        Time.timeScale = 0.0f;
        nowScore.text = time.ToString("N2");
        endPanel.SetActive(true);
    }

    IEnumerator ActiveTimePenalty(float penaltyDelay)
    {
        timeText.color = Color.red;
        yield return new WaitForSeconds(penaltyDelay);
        timeText.color = Color.white;
    }

    public void CloseNameText()
    {
        nameText.SetActive(false);
    }

    public void EndGame()
    {
        Time.timeScale = 0.0f;
    }

    public void TimeScore()
    {
        //남은 시간 대비 점수
        if (reminingTime >= 15.0f)
        {
            score += 30;
        }
        else if (reminingTime >= 10.0f)
        {
            score += 20;
        }
        else if (reminingTime >= 5.0f)
        {
            score += 10;
        }
    }
}

