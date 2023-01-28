using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SafeTile : MonoBehaviour
{
    BoxCollider2D SafeCollider;
    void Awake()
    {
        SafeCollider = GetComponent<BoxCollider2D>();
        SafeCollider.isTrigger= true;
        SafeCollider.enabled = true;
    }
    void OnTriggerEnter2D(Collider2D Rabbit)
    {
        if(Rabbit.gameObject.name == "Rabbit")
        {
            Debug.Log("토끼가 안전한 발판 위에 있습니다.");
        }
    }
}
