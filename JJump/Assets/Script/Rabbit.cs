using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public float speed = 3;
    float elapsedTime = 0;
    Distance notify_tile;
   

    void Start()
    {
        notify_tile = GameObject.Find("Rabbit").GetComponent<Distance>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)) 
        { 
            transform.Translate(new Vector3(-speed*Time.deltaTime,0,0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0,speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        //발판 종류
       if(notify_tile.CloseTile.layer == 10)
        {

        }
        if (notify_tile.CloseTile.layer == 11 && notify_tile.shortDis < 0.5)
        {
            Destroy(gameObject);
            Debug.Log("GameOver");
        }
//if(notify_tile.CloseTile.layer == 13 && notify_tile.shortDis < 3)
        //{
        //    elapsedTime= 0;
        //    while(elapsedTime < 1) {
        //        if (Input.GetKey(KeyCode.LeftArrow))
        //        {
        //            transform.Translate(new Vector3(5, 0, 0));
        //        }
        //        if (Input.GetKey(KeyCode.RightArrow))
        //        {
        //            transform.Translate(new Vector3(-5, 0, 0));
        //        }
        //        if (Input.GetKey(KeyCode.UpArrow))
        //        {
        //            transform.Translate(new Vector3(0, -5, 0));
        //        }
        //        if (Input.GetKey(KeyCode.DownArrow))
        //        {
        //            transform.Translate(new Vector3(0, 5, 0));
        //        } 
        //    }
        //}
        
    }
 
}
