using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpassableTile : MonoBehaviour
{
    BoxCollider2D UnpassableCollider;
    void Awake()
    {
        UnpassableCollider = GetComponent<BoxCollider2D>();
        UnpassableCollider.enabled = true;
        UnpassableCollider.isTrigger = false;
        UnpassableCollider.size = new Vector2(0.8f, 0.8f);
    }
    private void OnCollisionStay2D(Collision2D Rabbit)
    {
        if (Rabbit.gameObject.name == "Rabbit")
        {
            Debug.Log("토끼가 충돌하고 있습니다.");
        }
    }
}
