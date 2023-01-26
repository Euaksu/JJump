using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Distance : MonoBehaviour
{

    public List<GameObject> FoundTile;
    public GameObject CloseTile;
    public string TagName;
    public float shortDis;



    void Update()
    {
        FoundTile = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tile"));
        shortDis = Vector2.Distance(gameObject.transform.position, FoundTile[0].transform.position);

        CloseTile = FoundTile[0];

        foreach (GameObject tile in FoundTile)
        {
            float Dis = Vector2.Distance(gameObject.transform.position, tile.transform.position);
            
            if(Dis < shortDis)
            {
                CloseTile = tile;
                shortDis = Dis;
            }
        }
        Debug.Log(CloseTile.name + " " + CloseTile.layer);
        
    }
       
}
