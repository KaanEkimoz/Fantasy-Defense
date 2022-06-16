using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBoard : MonoBehaviour
{
    public static Grid Board;
    public int gridWidth = 6;
    public int gridHeight = 9;
    public bool debugOn = true;
    
    void Start()
    {
        Board = new Grid(gridWidth,gridHeight,1,new Vector3(-0.5f,0,-0.5f),debugOn);
    }
}
