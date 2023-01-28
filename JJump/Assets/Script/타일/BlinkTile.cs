using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkTile : MonoBehaviour
{
    BoxCollider2D BlinkCollider;
    void Awake()
    {
        BlinkCollider = GetComponent<BoxCollider2D>();
        BlinkCollider.enabled = true;
        BlinkCollider.isTrigger= true;
        BlinkCollider.size = new Vector2(0.3f, 0.3f);
    }
    public float ElapsedTime = 0;
    public float ChangeTime = 5;
    bool OnOff = true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= ChangeTime)
        {
            ElapsedTime = 0;
            OnOff = !OnOff;
            
        }
        
    }
   void OnTriggerStay2D(Collider2D Rabbit)
    {   if (!Player.isJumping)
        {
            if (OnOff)
            {
                Debug.Log("지금은 안전합니다");
            }
            else
            {
                Debug.Log("지금은 지나갈수 없습니다.");
                GameManager.GameOver = true;
                BGMManager.instance.SFXPlay();
            }
        }
    }
}
