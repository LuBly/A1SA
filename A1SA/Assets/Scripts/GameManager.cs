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

    public int cardCount = 0;
    public int matchCount = 0;

    AudioSource audioSource;
    public AudioClip clip;
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

        resultText.text = matchCount.ToString();

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
            audioSource.PlayOneShot(clip);
            // 파괴해라.
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
            // 닫아라.
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        matchCount += 1;

        // 카드 초기화
        firstCard = null;
        secondCard = null;
    }
}
