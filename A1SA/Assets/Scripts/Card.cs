using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    
    public GameObject front;
    public GameObject back;

    public Animator anim;

    public SpriteRenderer frontImage;
    public SpriteRenderer backImage;
    public int idx;

    AudioSource audioSource;
    public AudioClip clip;

    public float CloseTime = 0f; // 5초 동안 아무것도 하지 않은 후 카드를 닫는다.
    private bool isOpened = false; // 카드가 열렸는지 여부를 확인.

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isOpened)
        {
            //시간 측정
            CloseTime += Time.deltaTime;
            // 5초가 지나면 카드를 닫는다.
            if (CloseTime >= 5.0f)
            {
                CloseCard();
                CloseTime = 0f; // CloseTime 초기화.
                GameManager.Instance.firstCard = null; //첫번째 카드를 뽑은 데이터 초기화
            }
        }
    }

    public void Setting(int num)
    {
        idx = num;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }


    public void OpenCard()
    {
        if (GameManager.Instance.secondCard != null) return;
        
        audioSource.PlayOneShot(clip);
        //소리들이 겹치지 않음
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        isOpened = true;


        // firstCard가 비었다면
        if (GameManager.Instance.firstCard == null)
        {
            // firstCard에 내 정보를 넘겨준다.
            GameManager.Instance.firstCard = this;
        }
        
        // firstCard가 비어있지 않다면
        else
        {
            // secondCard에 내 정보를 넘겨준다.
            GameManager.Instance.secondCard = this;
            // Matched 함수를 호출한다.
            GameManager.Instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    public void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
        backImage.color = Color.gray;
        isOpened = false;
    }

    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
        
    }
}
