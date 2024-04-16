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
    public GameObject nameText;
    public GameObject endText;

    // 결과창 
    [Header("결과창이 남아있는 시간")]
    public float resultDelay = 0.5f;
    [Header("실패시 줄어드는 시간")]
    public float penaltyTime = 2.0f;
    
    [Header("실패 시 빨간색으로 깜빡이는 시간")]
    public float penaltyDelay = 0.2f;

    public int cardCount = 0;
    public int matchCount = 0;
    public AudioClip clip;

    public string[] userNames = new string[5];

    AudioSource audioSource;
    float time = 0.0f;
    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.clip;
        audioSource.Play();
        //시작할때 BGM의 속도 정상화
        audioSource.pitch = 1.0f;
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");

        resultText.text = matchCount.ToString();
        //일정시간 경과시 경고
        if (time >= 20.0f)
        {
            //Text를 빨간색으로
            timeText.color = Color.red;
            //BGM Pitch(재생속도)를 1.3로 변경
            audioSource.pitch = 1.3f;
        }
        if (time >= 30.0f)
        {
            time = 30.0f;
            Time.timeScale = 0.0f;
            endPanel.SetActive(true);
        }
    }

    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
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
            if(cardCount == 0)
            {
                Time.timeScale = 0.0f;
                endPanel.SetActive(true);
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
}
