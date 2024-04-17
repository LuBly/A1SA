using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioSource audioSource;
    public AudioClip clip;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.clip;
        audioSource.Play();
    }

    void Update()
    {
        if (GameManager.Instance != null)
        {
            float stageTime = GameManager.Instance.GetStageTime();
            float currentTime = GameManager.Instance.GetCurrentTime();

            // 스테이지 시간의 2/3이 넘으면 배경음악 속도를 2배로 빠르게 설정
            if (currentTime >= stageTime * 2 / 3)
            {
                audioSource.pitch = 2.0f;
            }
            else
            {
                audioSource.pitch = 1.0f; // 기본 속도로 설정
            }
        }
    }

}
