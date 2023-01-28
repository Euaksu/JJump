using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    private float GameTime;
    public Text GameTimeText;
    private int GameTime_Min;
    void Start()
    {
        GameTime = 0;
        GameTime_Min = 0;
    }
    void Update()
    {
        if (GameManager.GameClear || GameManager.GameOver)
        {
            GameTimeText.text = "Time: " + GameTime_Min.ToString() + ":" + GameTime.ToString("F1");

        }
        else
        {
            GameTime += Time.deltaTime;

            if (GameTime > 60f && !GameManager.GameOver && !GameManager.GameClear)
            {
                GameTime = 0;
                GameTime_Min += 1;
            }
            if (GameManager.GameClear && GameManager.GameOver)
            {

            }

            GameTimeText.text = "Time: " + GameTime_Min.ToString() + ":" + GameTime.ToString("F1");
        }

    }
}
