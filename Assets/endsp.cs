using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class endsp : MonoBehaviour
{
    [SerializeField]
    Text sukoabodo;

    [SerializeField]
    Text hisukoa;

    public static int ihsukoa;
    public int k;

    public void ToGameScene()
    {
        Bird.score = 0;
        SceneManager.LoadScene("Scenes/Game");
        
    }

    // Update is called once per frame
    void Update()
    {
        k = Bird.score;

        if (ihsukoa <= k)
        {
            ihsukoa = k;
        }

        sukoabodo.text = Bird.score.ToString();
        hisukoa.text = ihsukoa.ToString();
       
    }
}
