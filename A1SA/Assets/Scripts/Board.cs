using UnityEngine;
using System.Linq;
public class Board : MonoBehaviour
{
    public GameObject card;
    float dist = 1.4f;
    private void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f,7f)).ToArray();
        for(int i = 0; i < 16; i++)
        {
            GameObject go = Instantiate(card, transform);
            
            float x = (i % 4) * dist - 2.1f;
            float y = (i / 4) * dist - 3.0f;

            go.transform.position = new Vector3(x, y, 0);
            go.GetComponent<Card>().Setting(arr[i]);
            //각 카드별로 모든 images를 들고 있는것 보다 
            //Board하나에서 images를 가지고 있고, 이를 card Sprite변경에만 사용하는게 더 효율적이지 않을까?
            //-->Resources 폴더를 활용해서 아이템을 가지고 오는 방법을 채택했다. 필요한 item만 가져오니까 더 효율적일 것 같다.
            /*
             * 카드 이미지를 배열이나 List를 이용해서 hierarchy에 추가시키는 방법과 Resources.Load를 사용하는 방법의 차이점과 장단점은 다음과 같습니다.

                1. 배열이나 List를 이용해서 hierarchy에 추가시키는 방법
                    - 장점:
                        - 배열이나 List를 이용하면 미리 로드된 이미지들을 쉽게 관리할 수 있습니다.
                        - 이미지들을 동적으로 추가하거나 제거할 수 있어 유연성이 높습니다.
                        - hierarchy에 추가된 이미지들을 코드로 쉽게 조작할 수 있습니다.

                    - 단점:
                        - 이미지들을 배열이나 List에 미리 저장해야 하므로 메모리 사용량이 증가할 수 있습니다.
                        - 이미지들을 일일히 추가해야 하므로 번거로울 수 있습니다.

                2. Resources.Load를 사용하는 방법
                    - 장점:
                        - Resources.Load를 사용하면 필요한 이미지를 동적으로 로드할 수 있어 메모리를 효율적으로 관리할 수 있습니다.
                        - 이미지를 Resources 폴더에 저장하고 간단한 경로로 로드할 수 있어 편리합니다.

                    - 단점:
                        - Resources.Load를 사용하면 이미지를 로드할 때마다 파일 시스템에 접근해야 하므로 성능에 영향을 줄 수 있습니다.
                        - Resources 폴더에 이미지를 저장해야 하므로 프로젝트 구조가 복잡해질 수 있습니다.

                따라서, 배열이나 List를 이용해서 hierarchy에 추가시키는 방법은 이미지를 관리하고 조작하는 데 유연성이 높지만 메모리 사용량이 증가할 수 있고 번거로울 수 있습니다. 
                반면에 Resources.Load를 사용하는 방법은 메모리를 효율적으로 관리할 수 있지만 파일 시스템에 접근해야 하고 프로젝트 구조가 복잡해질 수 있습니다
             */
            GameManager.Instance.cardCount = arr.Length;
        }
    }
}
