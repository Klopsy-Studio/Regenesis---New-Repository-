using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMovement : Movement
{
    public override IEnumerator SimpleTraverse(Tile tile)
    {
        moving = true;
        unit.Place(tile);
        // Build a list of way points from the unit's 
        // starting tile to the destination tile
        //List<Tile> targets = new List<Tile>();
        //while (tile != null)
        //{
        //    targets.Insert(0, tile);
        //    tile = tile.prev;
        //}
        //// Move to each way point in succession
        //for (int i = 1; i < targets.Count; ++i)
        //{
        //    Tile from = targets[i - 1];
        //    Tile to = targets[i];
        //    Directions dir = from.GetDirections(to);
        //    if (unit.dir != dir)
        //        yield return StartCoroutine(Turn(dir));
        //    if (from.height == to.height)
        //        yield return StartCoroutine(Walk(to));
        //    else
        //        yield return StartCoroutine(Jump(to));
        //}


        moving = false;
        unit.transform.position = new Vector3(tile.pos.x, unit.transform.position.y, tile.pos.y);
        yield return null;
    }

    public override IEnumerator Traverse(Tile tile)
    {
        moving = true;
        unit.Place(tile);

        List<Tile> targets = new List<Tile>();
        while (tile != null)
        {
            targets.Insert(0, tile);
            tile = tile.prev;
        }

        // Move to each way point in succession
        for (int i = 1; i < targets.Count; ++i)
        {
            Tile from = targets[i - 1];
            Tile to = targets[i];
            Directions dir = from.GetDirections(to);

            if (unit.dir != dir)
                yield return StartCoroutine(Turn(dir));
            if (from.height == to.height)
                yield return StartCoroutine(Walk(to));
            else
                yield return StartCoroutine(Jump(to));
        }


        moving = false;

        yield return null;
    }


    protected override bool ExpandSearch(Tile from, Tile to) 
    {
        // Skip if the distance in height between the two tiles is more than the unit can jump
        if ((Mathf.Abs(from.height - to.height) > jumpHeight))
            return false;
        if (to.content != null)
        {
            //If filter = null, it will not filter anything
            if(filter == TimeLineTypes.Null)
            {
                return false;
            }

            //If filter = EnemyUnit, the tiles with an enemy in it will be filtered

            if(filter == TimeLineTypes.EnemyUnit)
            {
                if(to.content.GetComponent<EnemyUnit>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            //If filter = PlayerUnit, the tiles with a player in it will be filtered

            if (filter == TimeLineTypes.PlayerUnit)
            {
                if(to.content.GetComponent<PlayerUnit>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        return base.ExpandSearch(from, to);
    }

    IEnumerator Walk(Tile target)
    {
        Tweener tweener = transform.MoveTo(target.center, 0.5f, EasingEquations.Linear);
        while (tweener != null)
            yield return null;
    }

    IEnumerator Jump(Tile to)
    {
        Tweener tweener = transform.MoveTo(to.center, 0.5f, EasingEquations.Linear);
        Tweener t2 = jumper.MoveToLocal(new Vector3(0, Tile.stepHeight * 2f, 0), tweener.easingControl.duration / 2f, EasingEquations.EaseOutQuad);
        t2.easingControl.loopCount = 1;
        t2.easingControl.loopType = EasingControl.LoopType.PingPong;
        while (tweener != null)
            yield return null;
    }

}
