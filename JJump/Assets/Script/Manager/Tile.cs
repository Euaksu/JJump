using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField]
    private Coordinate coor;

    public Coordinate Coor => coor;

    protected virtual void Awake() 
    {
        coor = Coordinate.WorldPointToCoordinate(transform.position);
    }

    public abstract void OnStep(Player player);
}
