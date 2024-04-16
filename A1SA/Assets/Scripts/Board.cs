using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class Board : MonoBehaviour
{
    public GameObject card;
    float dist = 1.4f;
    float speed = 50.0f;
    Dictionary<GameObject, Vector3> cardMap = new Dictionary<GameObject, Vector3>();
    private void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        GameManager.Instance.cardCount = arr.Length;
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();
        for (int i = 0; i < 16; i++)
        {
            GameObject go = Instantiate(card, transform);

            float x = (i % 4) * dist - 2.1f;
            float y = (i / 4) * dist - 3.0f;

            cardMap.Add(go, new Vector3(x, y, 0));
            go.GetComponent<Card>().Setting(arr[i]);
        }

        StartCoroutine(SuffleCard());
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
