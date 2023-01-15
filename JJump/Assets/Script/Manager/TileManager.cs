using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : SingletonBehaviour<TileManager>
{
    private Tile[,] tileArray;
    [SerializeField]
    private int worldXSize;
    [SerializeField]
    private int worldYSize;

    public Tile[,] TileArray => tileArray;

    private void Awake()
    {
        tileArray = new Tile[worldXSize, worldYSize];
    }

    public Tile GetTile(Coordinate coor) => tileArray[coor.X, coor.Y];
}
