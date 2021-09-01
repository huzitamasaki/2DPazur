using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBird : MonoBehaviour
{
    // 鳥のプレハブを格納する配列
    public GameObject[] BirdPrefabs;

    // 連鎖を消す最小数
    [SerializeField]
    private float removeBirdMinCount = 1;

    // 連鎖判定用の距離
    [SerializeField]
    private float birdDistance = 1.6f;

    // クリックされた鳥を格納
    private GameObject firstBird;
    private GameObject lastBird;
    private string currentName;
    List<GameObject> removableBirdList = new List<GameObject>();

    void Start()
    {
        
        TouchManager.Ended += (info) =>
        {
            // リストの格納数を取り出し最小数と比較する
            int removeCount = removableBirdList.Count;
            if (removeCount >= removeBirdMinCount)
            {
                // 消す
                foreach (GameObject obj in removableBirdList)
                {
                    Destroy(obj);
                }
                // 補充
                StartCoroutine(DropBirds(removeCount));
            }

            foreach (GameObject obj in removableBirdList)
            {
                ChangeColor(obj, 1.0f);
            }
            removableBirdList = new List<GameObject>();
            firstBird = null;
            lastBird = null;
        };
        StartCoroutine(DropBirds(50));
    }
    private void PushToBirdList(GameObject obj)
    {
        removableBirdList.Add(obj);
        ChangeColor(obj, 0.5f);
    }
    private void ChangeColor(GameObject obj, float transparency)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r,
            renderer.color.g,
            renderer.color.b,
            transparency);
    }

    IEnumerator DropBirds(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // ランダムで出現位置を作成
            Vector2 pos = new Vector2(Random.Range(-4.20f, 4.20f), 8.16f);
            // ランダムで鳥を出現させてIDを格納
            int id = Random.Range(0, BirdPrefabs.Length);
            // 鳥を発生させる
            GameObject bird = (GameObject)Instantiate(BirdPrefabs[id],
                pos,
                Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward));
            // 作成した鳥の名前を変更します
            bird.name = "Bird" + id;
            // 0.05秒待って次の処理へ
            yield return new WaitForSeconds(0.05f);
        }

    }
}
