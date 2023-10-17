using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GroundSystem : AbstractSystem
{
    private class GroundCell
    {
        public Vector3 worldPosition;
        public Vector2 groundPosition;
        public bool isHavingPlant;
        public BoxCollider2D collider;
        public IPlant plant;
        public GroundCell() { }
    }
    private BoxCollider2D PlantArea;
    private GroundCell[,] GroundCells = new GroundCell[9,5];
    public GroundSystem()
    {
        RestartGame();
    }
    public Vector3 WorldPositionToGroundWorldPosition(Vector3 position)
    {
        Vector3 result = Vector3.zero;
        float distance = 1000f;
        foreach (GroundCell ground in GroundCells)
        {
            if (ground == null) continue;
            if (Vector3.Distance(position, ground.worldPosition) < distance)
            {
                distance = Vector3.Distance(position, ground.worldPosition);
                result = ground.worldPosition;
            }
        }
        return result;
    }
    public bool isWorldPositionInPlantArea(Vector3 position)
    {
        return PlantArea.bounds.Contains(position);
    }
    public Vector2 WorldPositionToGroundPosition(Vector3 position)
    {
        Vector2 result = Vector3.zero;
        float distance = 1000f;
        foreach (GroundCell ground in GroundCells)
        {
            if (ground == null) continue;
            if (Vector3.Distance(position, ground.worldPosition) < distance)
            {
                distance = Vector3.Distance(position, ground.worldPosition);
                result = ground.groundPosition;
            }
        }
        return result;
    }
    public float GetWorldOffsetYByRowIndex(int index)
    {
        return GroundCells[0, index].worldPosition.y;
    }
    public int GetColumeByWorldOffsetX(float x)
    {
        float sum = 1000;
        int result = 0;
        for(int colume=0;colume<9;colume++)
        {
            float val = Mathf.Abs(GroundCells[colume, 2].worldPosition.x - x);
            if (val<sum)
            {
                sum = val;
                result= colume;
            }
        }
        return result;
    }
    public void Plant(Vector3 position, IPlant plant)
    {
        GroundCell result = GroundCells[0,0];
        float distance = 1000f;
        foreach (GroundCell ground in GroundCells)
        {
            if (ground == null) continue;
            if (Vector3.Distance(position, ground.worldPosition) < distance)
            {
                distance = Vector3.Distance(position, ground.worldPosition);
                result = ground;
            }
        }
        result.isHavingPlant = true;
        result.plant = plant;
    }
    public bool GetIsHavingPlant(Vector3 position)
    {
        GroundCell result = GroundCells[0,0];
        float distance = 1000f;
        foreach (GroundCell ground in GroundCells)
        {   
            if (ground == null) continue;
            if (Vector3.Distance(position, ground.worldPosition) < distance)
            {
                distance = Vector3.Distance(position, ground.worldPosition);
                result = ground;
            }
        }
        return result.isHavingPlant;
    }
    public IPlant GetPlant(Vector3 position)
    {
        GroundCell result = GroundCells[0,0];
        float distance = 1000f;
        foreach (GroundCell ground in GroundCells)
        {   
            if (ground == null) continue;
            if (Vector3.Distance(position, ground.worldPosition) < distance)
            {
                distance = Vector3.Distance(position, ground.worldPosition);
                result = ground;
            }
        }
        if (result.plant == null)
        {
            Debug.Log("UnityTool GetPlant返回null");
        }
        return result.plant;
    }
    public List<IPlant> GetPlantByRow(int row)
    {
        List<IPlant> result = new List<IPlant>();
        for(int i=0;i<9;i++)
        {
            if (GroundCells[i,row].plant!=null)
            {
                result.Add(GroundCells[i, row].plant);
            }
        }
        return result;
    }
    public void RemovePlant(Vector3 position)
    {
        GroundCell result = GroundCells[0,0];
        float distance = 1000f;
        foreach (GroundCell ground in GroundCells)
        {
            if (ground == null) continue;
            if (Vector3.Distance(position, ground.worldPosition) < distance)
            {
                distance = Vector3.Distance(position, ground.worldPosition);
                result = ground;
            }
        }
        result.isHavingPlant = false;
        result.plant = null;
    }
    public void RestartGame()
    {
        //GroundCells.Clear();
        int StartRow = 0;
        int EndRow = 5;
        PlantArea = GameObject.Find("PlantArea").GetComponent<BoxCollider2D>();
        int stageId = ArchiveCommand.Instance.StageId;
        switch (stageId)
        {
            case 1:
                StartRow = 2;
                EndRow = 3;
                PlantArea.size = new Vector2(PlantArea.size.x, PlantArea.size.y / 5f);
                break;
            case 2:
                StartRow = 1;
                EndRow = 4;
                PlantArea.size = new Vector2(PlantArea.size.x, PlantArea.size.y / 5f*3);
                break;
            case 3:
                StartRow = 1;
                EndRow = 4;
                PlantArea.size = new Vector2(PlantArea.size.x, PlantArea.size.y / 5f*3);
                break;
            default:
                StartRow = 0;
                EndRow = 5;
                break;
        }

        GameObject GroundGrid = GameObject.Find("GroundGrid").gameObject;

        for (int row = StartRow; row < EndRow; row++)
        {
            for (int colume = 0; colume < 9; colume++)
            {
                BoxCollider2D collider= GroundGrid.transform.Find("Cell" + colume + "x"+row).GetComponent<BoxCollider2D>();
                GroundCell cell = new GroundCell();
                cell.worldPosition = collider.transform.position;
                cell.groundPosition = new Vector2(colume, row);
                cell.collider = collider;
                GroundCells[colume, row]=cell;
            }
        }

    }
#if UNITY_EDITOR
    [UnityEditor.MenuItem("MyCommand/InitializeGround")]
#endif
    public static void InitializeGround()
    {
        GameObject GroundGrid = GameObject.Find("GroundGrid").gameObject;
        BoxCollider2D cell = GroundGrid.transform.Find("Cell").GetComponent<BoxCollider2D>();

        BoxCollider2D[] cellsx=new BoxCollider2D[9];
        BoxCollider2D[] cellsy=new BoxCollider2D[5];

        for(int colume=0;colume<9;colume++)
        {
            cellsx[colume] = GroundGrid.transform.Find("Cell" + colume + "x0").GetComponent<BoxCollider2D>();
        }

        for(int row=0;row<5;row++)
        {
            cellsy[row] = GroundGrid.transform.Find("Cell0x" + row).GetComponent<BoxCollider2D>();
        }
        for (int row = 0; row < 5; row++)
        {
            for (int colume = 0; colume < 9; colume++)
            {
                GameObject obj = Object.Instantiate(cell.gameObject, cell.transform.parent);
                obj.transform.position = new Vector2(cellsx[colume].transform.position.x, cellsy[row].transform.position.y);
                obj.name = "Cell" + colume + "x" + row;
                obj.SetActive(true);
            }
        }
        
    }
}