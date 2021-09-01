using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Bird : MonoBehaviour
{
    private int time=1;
    private float a = 60;
    private float endt;
    [SerializeField]
    Text zikan;

    [SerializeField]
    Text endui;

    public static int score;
    private float b;

    // 鳥のプレハブを格納する配列
    public GameObject[] BirdPrefabs;

    // 連鎖を消す最小数
    [SerializeField]
    private float removeBirdMinCount = 3;

    // 連鎖判定用の距離
    [SerializeField]
    private float birdDistance = 1.6f;

    [SerializeField]
    Text sukoa;

    // クリックされた鳥を格納
    private GameObject firstBird;
    private GameObject lastBird;
    private string currentName;
    List<GameObject> removableBirdList = new List<GameObject>();

    private AudioSource sound01;
    private AudioSource sound02;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            sound01 = audioSources[0];
            sound02 = audioSources[1];
            TouchManager.Began += (info) =>
            {
                if (time != 0)
                {
                    // クリック地点でヒットしているオブジェクトを取得
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint),
                            Vector2.zero);
                    if (hit.collider)
                    {
                        GameObject hitObj = hit.collider.gameObject;
                        // ヒットしたオブジェクトのtagを判別し初期化
                        if (hitObj.tag == "Bird")
                        {
                            firstBird = hitObj;
                            lastBird = hitObj;
                            currentName = hitObj.name;
                            removableBirdList = new List<GameObject>();
                            PushToBirdList(hitObj);
                        }
                    }
                }
            };
            TouchManager.Moved += (info) =>
            {
                if (!firstBird)
                {
                    return;
                }
                // クリック地点でヒットしているオブジェクトを取得
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(info.screenPoint),
                        Vector2.zero);
                if (hit.collider)
                {
                    GameObject hitObj = hit.collider.gameObject;

                    // ヒットしたオブジェクトのtagが鳥、尚且名前が一緒、
                    // 尚且最後にhitしたオブジェクトと違う、尚且リストに格納されていない
                    if (hitObj.tag == "Bird" && hitObj.name == currentName
                        && hitObj != lastBird && 0 > removableBirdList.IndexOf(hitObj))
                    {
                        // 距離を見る
                        float distance = Vector2.Distance(hitObj.transform.position,
                                lastBird.transform.position);
                        if (distance > birdDistance)
                        {
                            return;
                        }
                        lastBird = hitObj;
                        PushToBirdList(hitObj);
                    }
                }
            };
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
                        sound02.PlayOneShot(sound02.clip);
                    }
                    // 補充
                    StartCoroutine(DropBirds(removeCount));
                    score = score + removeCount;
                    if (removeCount >= 5)
                    {
                        a = a + 3;
                    }

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
    }
    private void PushToBirdList(GameObject obj)
    {
        removableBirdList.Add(obj);
        ChangeColor(obj, 0.5f);
        sound01.PlayOneShot(sound01.clip);
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
    void Update()
    {
        if (time != 0)
        {
            a = a - Time.deltaTime;
            time = (int)a;
        }
        if (time == 0)
        {
            endui.text = "しゅうりょ～";
            endt = endt + Time.deltaTime;
        }
        if (endt >= 5)
        {
            SceneManager.LoadScene("End");
        }
        zikan.text = time.ToString();

        sukoa.text = score.ToString();

    }
}
