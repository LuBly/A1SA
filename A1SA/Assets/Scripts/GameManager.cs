using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public TextMeshProUGUI timeText;
    public GameObject nameText;
    public GameObject endText;

    // 결과창이 남아있는 시간
    [Header("결과창이 남아있는 시간")]
    public float resultDelay = 0.5f;
    public int cardCount = 0;
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
    }
    private void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");

        if (time >= 30.0f)
        {
            time = 30.0f;
            Time.timeScale = 0.0f;
            endText.SetActive(true);
        }
    }

    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            int userIdx = firstCard.idx % 5;
            audioSource.PlayOneShot(clip);
            // 0.5초간 결과 메세지 출력
            nameText.SetActive(true);
            nameText.GetComponent<TextMeshProUGUI>().text = "나는 " + userNames[userIdx];
            nameText.GetComponent<TextMeshProUGUI>().color = Color.white;
            Invoke("CloseNameText", resultDelay);
            // 파괴해라.
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount == 0)
            {
                Time.timeScale = 0.0f;
                endText.SetActive(true);
            }
        }
        else
        {
            // 0.5초간 결과 메세지 출력
            nameText.SetActive(true);
            nameText.GetComponent<TextMeshProUGUI>().text = "실패ㅋ";
            nameText.GetComponent<TextMeshProUGUI>().color = Color.red;
            Invoke("CloseNameText", resultDelay);
            // 닫아라.
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        // 카드 초기화
        firstCard = null;
        secondCard = null;
    }

    public void CloseNameText()
    {
        nameText.SetActive(false);
    }
}
