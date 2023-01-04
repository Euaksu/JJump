using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : SingletonBehavior<TileManager>
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
        // 타일 불러오는 함수 만들기 (해야할일)
    }

    public Tile GetTile(Coordinate coor) => tileArray[coor.X, coor.Y];
}
