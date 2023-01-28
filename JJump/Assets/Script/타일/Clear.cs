using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    Collider2D ClearCollider;
    void Awake()
    {
        ClearCollider = GetComponent<Collider2D>();
        ClearCollider.enabled = true;
        ClearCollider.isTrigger = true;
        ClearCollider.enabled = true;

    }
    void OnTriggerEnter2D(Collider2D Rabbit)
    {
        GameManager.GameClear = true;
        BGMManager.instance.SFXPlay();
    }
}
