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

    // ���â�� �����ִ� �ð�
    [Header("���â�� �����ִ� �ð�")]
    public float resultDelay = 0.5f;
    [Header("���н� �پ��� �ð�")]
    public float penaltyTime = 2.0f;
    
    [Header("���� �� ���������� �����̴� �ð�")]
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
            int userIdx = firstCard.idx % 5;
            audioSource.PlayOneShot(clip);
            // 0.5�ʰ� ��� �޼��� ���
            nameText.SetActive(true);
            nameText.GetComponent<TextMeshProUGUI>().text = "���� " + userNames[userIdx];
            nameText.GetComponent<TextMeshProUGUI>().color = Color.white;
            Invoke("CloseNameText", resultDelay);
            // �ı��ض�.
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
            // 0.5�ʰ� ��� �޼��� ���
            nameText.SetActive(true);
            nameText.GetComponent<TextMeshProUGUI>().text = "���Ф�";
            nameText.GetComponent<TextMeshProUGUI>().color = Color.red;
            Invoke("CloseNameText", resultDelay);
            // �ð� ���� (2��)
            time += penaltyTime;
            StartCoroutine(ActiveTimePenalty(penaltyDelay));
            
            // �ݾƶ�.
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
