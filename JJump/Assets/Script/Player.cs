using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
    
    public int Health => health;
    public int MaxHealth => maxHealth;

    private void Awake() {
        health = maxHealth;
    }
}
