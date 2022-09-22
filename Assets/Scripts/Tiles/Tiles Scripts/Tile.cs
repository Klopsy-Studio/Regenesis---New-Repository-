using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypesOfTiles
{
    DessertTile1,
    DessertTile2,
    DessertTile3,
    QuicksandTile
}

public enum ObstacleType
{
    Null,
    RegularObstacle,
}
[System.Serializable]
public class Tile : MonoBehaviour
{
    public TypesOfTiles type;
    public const float stepHeight = 0.25f; 
    public Point pos;
    public int height;
    public Vector3 center { get { return new Vector3(pos.x, height * stepHeight, pos.y); } } //Center of the tile. Allow the units to center themselves

    [SerializeField] Renderer model;


    public GameObject content
    {
        get
        {
            return m_content;
        }
        set
        {
            m_content = value;

            if (content == null)
            {
                OnUnitLeave();
            }
            else
            {
                OnUnitArrive();
            }
        }
    }

    public GameObject m_content = null;

    [HideInInspector] public Tile prev; //stores the tile which was traversed to reach it
    [HideInInspector] public int distance; //numbers of tiles which have been crossed to reach the point 

    public bool selected;

    [Header("Materials")]

    public Material defaultMaterial;
    [HideInInspector] public Material prevMaterial;
    public Material attackMaterial;
    public Material movementMaterial;




    public void Match() //Matches the values of the variables with the gameObjects transforms values
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight / 2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }

    public void Grow()
    {
        height++;
        Match();
    }

    public void Shrink()
    {
        height--;
        Match();
    }
    
    public void Load (Point p, int h)
    {
        pos = p;
        height = h;
        Match();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }

    public virtual void OnUnitArrive()
    {
        
    }

    public virtual void OnUnitLeave()
    {
      
    }


    public void ChangeTile(Material m)
    {
        prevMaterial = model.material;

        model.material = m;
    }

    public void ChangeTileToDefault()
    {
        prevMaterial = defaultMaterial;
        model.material = defaultMaterial;
    }
}

