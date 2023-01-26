using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkTile : MonoBehaviour
{
    // Start is called before the first frame update
    public float ElapsedTime = 0;
    bool OnOff = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= 5)
        {
            ElapsedTime = 0;
            OnOff = !OnOff;
            if(OnOff == true)
            {
                this.gameObject.layer = 10;
            }
            else
            {
                this.gameObject.layer = 11;
            }
        }
       
    }
}
