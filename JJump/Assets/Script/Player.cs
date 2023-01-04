using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;
    private Coordinate coor;
    [Header("Set Coordinate")]
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;
    
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

    private void OnValidate()
    {
        coor = new Coordinate(x, y);
        transform.position = Coordinate.CoordinatetoWorldPoint(coor);
    }

    private void Move(Coordinate coor) 
    {
        TileManager.Inst.GetTile(coor).OnStep(this);
    }
}
