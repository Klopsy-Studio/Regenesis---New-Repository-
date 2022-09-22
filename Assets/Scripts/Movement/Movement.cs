using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public int range = 5;
    int originalRange;
    public int OriginalRange { get { return originalRange; } }
    public int jumpHeight;
    protected Unit unit;
    protected Transform jumper;

    public bool moving;

    protected TimeLineTypes filter;
    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
        //if(unit.UnitType == UnitType.PlayerUnit)
        //{
        //    originalRange = 5;
        //}
        //else 
        //{
        //    originalRange = 10;
        //}

        jumper = transform.Find("Jumper");
    }

    private void Start()
    {
        originalRange = range;
    }

    public virtual List<Tile> GetTilesInRange(Board board, bool filterEnemies)
    {
        List<Tile> retValue = board.Search(unit.tile, ExpandSearch);
        Filter(retValue, filterEnemies);
        return retValue;
    }

    public virtual List<Tile> GetTilesInRangeWithEnemy(Board board, bool filterEnemies)
    {
        List<Tile> retValue = board.Search(unit.tile, ExpandSearchWithEnemies);
        Filter(retValue, filterEnemies);
        return retValue;
    }
    protected virtual bool ExpandSearch(Tile from, Tile to) //Conditions for units to traverse tiles
    {
        return (from.distance + 1) <= range;
    }
    protected virtual bool ExpandSearchWithEnemies(Tile from, Tile to)
    {
        return (from.distance + 1) <= range;
    }
    protected virtual void Filter(List<Tile> tiles, bool filterEnemies)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            if (!filterEnemies)
            {
                if (tiles[i].content != null)
                {
                    if (tiles[i].content.GetComponent<EnemyUnit>() == null)
                    {
                        tiles.RemoveAt(i);
                    }
                }
            }
            else
            {
                if (tiles[i].content != null)
                {
                    tiles.RemoveAt(i);
                }
            }
        }

    }

    public void ChangeFilter(TimeLineTypes t)
    {
        filter = t;
    }
    public virtual List<Tile> GetTilesInRangeForEnemy(Board board, bool isAttack)
    {
        List<Tile> retValue = board.Search(unit.tile, ExpandSearch);
        FilterForEnemies(retValue, isAttack);
        return retValue;
    }

    protected virtual void FilterForEnemies(List<Tile> tiles, bool isAttack)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            if (isAttack)
            {
                if (tiles[i].content != null)
                {
                    if (tiles[i].content.GetComponent<PlayerUnit>() == null)
                    {
                        tiles.RemoveAt(i);
                    }
                }
            }
            else
            {
                if (tiles[i].content != null)
                {
                    tiles.RemoveAt(i);
                }
            }
        }

    }

    public abstract IEnumerator SimpleTraverse(Tile tile); //Unit just teleports.

    public abstract IEnumerator Traverse(Tile tile); //Traverse animation
    protected virtual IEnumerator Turn(Directions dir)
    {
        if(unit.dir == Directions.North || unit.dir == Directions.East)
        {
            if(dir == Directions.South || dir == Directions.West)
            {
                unit.gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            else
            {
                unit.gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;

            }
        }
        unit.dir = dir;

        
        yield return null;
    }

    public void ResetRange()
    {
        range = OriginalRange;
    }

    public void PushUnit(Directions pushDir, int pushStrength, Board board)
    {
        range = pushStrength;
        List<Tile> t = GetTilesInRange(board, true);
        Tile desiredTile = null;
        foreach(Tile dirTile in t)
        {
            if (dirTile.GetDirections(unit.tile) == pushDir)
            {
                desiredTile = dirTile;
            }
        }

        if(desiredTile != null && desiredTile.content == null)
        {
            StartCoroutine(Traverse(desiredTile));
        }
    }

    public void ChangeRange(int newRange)
    {
        range = newRange;
    }
}
