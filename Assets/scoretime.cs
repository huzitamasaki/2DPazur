using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoretime : MonoBehaviour
{
    private int time;
    private float a=60;
    [SerializeField]
    Text zikan;

    // Update is called once per frame
    void Update()
    {
         a = a - Time.deltaTime;
        time = (int)a;
        zikan.text = time.ToString();

    }
}
