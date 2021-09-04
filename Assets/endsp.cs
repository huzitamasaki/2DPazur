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

    public AudioSource ue;
    public AudioSource sita;

    public void ToGameScene()
    {
        Bird.score = 0;
        SceneManager.LoadScene("Scenes/Game");
        Bird.time = 59;
        
    }

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        ue = audioSources[0];
        sita = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {
        k = Bird.score;

        if (ihsukoa <= k)
        {
            ihsukoa = k;
            ue.PlayOneShot(ue.clip);
        }
        else
        {
            sita.PlayOneShot(sita.clip);
        }

        sukoabodo.text = Bird.score.ToString();
        hisukoa.text = ihsukoa.ToString();
       
    }
}
