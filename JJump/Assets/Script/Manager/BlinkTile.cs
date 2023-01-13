using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkTile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Text timeText;
    private float time = 0;
   
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 5f)
        {

        }
        else if(time <= 10f)
        {
            player.Health = 0;
        }
        else
        {
            time = 0;
        }
    }
}
