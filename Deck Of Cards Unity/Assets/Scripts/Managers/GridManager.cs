using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class GridManager : MonoBehaviour
{
    public int width = 8;
    public int height = 4;
    public GameObject gridCellPrefab; // Assign a prefab for the grid outline in the inspector
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[,] gridCells;
    public static GridManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    [Button("Test")]
    public void Test()
    {
        foreach (var grid in gridCells)
        {
            print(grid.gameObject.name);
        }
       
    }
    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        gridCells = new GameObject[width, height];
        Vector2 centerOffset = new Vector2(width / 2.0f - 0.5f, height / 2.0f - 0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 gridPosition = new Vector2(x, y);
                // Calculate the position for each cell based on the grid size
                Vector2 spawnPosition = gridPosition - centerOffset;

                // Instantiate the GridOutline prefab at this position
                GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);

                gridCell.transform.SetParent(transform);

                gridCell.GetComponent<GridCell>().gridIndex = gridPosition;

                gridCells[x, y] = gridCell;
            }
        }
    }

    public bool AddObjectToGrid(GameObject obj, Vector2 gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height)
        {
            GridCell cell = gridCells[(int)gridPosition.x, (int)gridPosition.y].GetComponent<GridCell>();

            if (cell.cellFull) return false;
            else
            {
                GameObject newObj = Instantiate(obj, cell.GetComponent<Transform>().position, Quaternion.identity);
                newObj.transform.SetParent(transform);
                gridObjects.Add(newObj);
                cell.objectInCell = newObj;
                cell.cellFull = true;
                return true;
            }
        }
        else return false;
    }

    public void ClearGrid(GridCell gridCell)
    {
        gridCell.objectInCell = null;
        gridCell.cellFull = false;
    }
}
