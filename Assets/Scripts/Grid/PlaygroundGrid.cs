using UnityEngine;

public class PlaygroundGrid : MonoBehaviour
{
    public static Grid Board;
    public int gridWidth = 7;
    public int gridHeight = 12;
    public bool debugOn = true;
    
    void Start()
    {
        Board = new Grid(gridWidth, gridHeight, 1, new Vector3(-0.5f, 0, -0.5f), debugOn);
    }
}
