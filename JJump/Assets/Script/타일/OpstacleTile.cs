using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpstacleTile : MonoBehaviour
{

    BoxCollider2D OpstacleCollider;
    void Awake()
    {
        OpstacleCollider = GetComponent<BoxCollider2D>();
        OpstacleCollider.enabled = true;
        OpstacleCollider.isTrigger= true;
        OpstacleCollider.size = new Vector2(0.5f, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D Rabbit)
    {
       
        if (Rabbit.gameObject.name == "Rabbit" && !Player.isJumping)
        {
            Debug.Log("토끼가 위험한 발판 위에 있습니다.");
            GameManager.GameOver = true;
            BGMManager.instance.SFXPlay();
            
         
            
        }
    }
   
}

