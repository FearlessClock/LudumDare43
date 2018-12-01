using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {
    public GameObject[,] grid;
    public GameObject floorSprite;

    public Vector2 gridSize;
    // Use this for initialization
	void Start () {
        //Generate the grid with basic tile 
        grid = new GameObject[(int)gridSize.x, (int)gridSize.y];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = Instantiate<GameObject>(floorSprite, new Vector3(i, j, 0), Quaternion.identity);
                grid[i, j].transform.parent = this.transform;
            }
        }
	}

    public GameObject GetTileAt(Vector2 pos)
    {
        return grid[(int)pos.x, (int)pos.y];
    }

    public void SetTileAt(Vector2 pos, GameObject tile)
    {
        grid[(int)pos.x, (int)pos.y] = tile;
    }
}
