using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private Coordinate coor;
    
    public int Health
    {
        get { return health; }
        set
        {
            health = Mathf.Max(0, value);
        }
    }
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        health = maxHealth;
        coor = Coordinate.WorldPointToCoordinate(transform.position);
    }

    private void Move(Coordinate coor) 
    {
        TileManager.Inst.GetTile(coor).OnStep(this);
    }
}
