using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    public GameObject card;
    float dist = 1.4f;
    float speed = 50.0f;
    Dictionary<GameObject, Vector3> cardMap = new Dictionary<GameObject, Vector3>();
    
    private void Start()
    {
        SetCard();
        StartCoroutine(SuffleCard());
    }
    private void SetCard()
    {
        int[] arr;
        int stageIdx = GameManager.Instance.stageIdx;
        switch (stageIdx)
        {
            case 1:
                float startX = -dist * 1.5f; // 시작 x 위치 
                float startY = 0.0f; // 시작 y 위치

                arr = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
                arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();
                GameManager.Instance.cardCount = 16;
                for (int i = 0; i < 8; i++)
                {
                    GameObject go = Instantiate(card, transform);

                    float x = startX + (i % 4) * dist;
                    float y = startY - (i / 4) * dist;

                    cardMap.Add(go, new Vector3(x, y, 0));
                    go.GetComponent<Card>().Setting(arr[i]);
                }
                break;
            case 2:
                // 여기에 Stage2 카드 생성 코드를 작성해주시면 됩니다.
                // 12장 40초
                break;

            case 3:
                arr = new int[]{ 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
                arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();
                GameManager.Instance.cardCount = 16;
                for (int i = 0; i < 16; i++)
                {
                    GameObject go = Instantiate(card, transform);

                    float x = (i % 4) * dist - 2.1f;
                    float y = (i / 4) * dist - 3.0f;

                    cardMap.Add(go, new Vector3(x, y, 0));
                    go.GetComponent<Card>().Setting(arr[i]);
                }
                break;

            case 4:
                arr = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
                arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();
                GameManager.Instance.cardCount = 20;

                for (int i = 0; i < 20; i++)
                {
                    GameObject go = Instantiate(card, transform);

                    float x = (i % 4) * dist - 2.1f;
                    float y = (i / 4) * dist - 4.2f;

                    cardMap.Add(go, new Vector3(x, y, 0));
                    go.GetComponent<Card>().Setting(arr[i]);
                }
                break;

        }
        
    }
    IEnumerator SuffleCard()
    {
        foreach (KeyValuePair<GameObject, Vector3> card in cardMap)
        {
            GameObject curCard = card.Key;
            Vector3 targetPos = card.Value;
            float dis = Vector3.Distance(curCard.transform.position, targetPos);
            while (dis > 0.01f)
            {
                curCard.transform.position = Vector3.Lerp(curCard.transform.position, targetPos, speed * Time.deltaTime);
                dis = Vector3.Distance(curCard.transform.position, targetPos);
                yield return null;
            }
        }
        GameManager.Instance.isReady = true;

    }
}
