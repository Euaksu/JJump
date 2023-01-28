using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public float speed = 3;

    BoxCollider2D RabbitCollider;
    Rigidbody2D RabbitRigid;
    void Awake()
    {
        RabbitCollider = GetComponent<BoxCollider2D>();


        RabbitRigid= GetComponent<Rigidbody2D>();
        RabbitRigid.gravityScale = 0;
        RabbitRigid.freezeRotation = true;
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
       
       
    }
    
}
