using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossAbilityRange : AbilityRange
{
    public int crossLength;

    int offset = -1;
    public override List<Tile> GetTilesInRange(Board board)
    {
        Point startPos = unit.tile.pos;
        List<Tile> retValue = new List<Tile>();
        for (int i = 0; i < crossLength; i++)
        {
            if(offset >= i)
            {
                startPos += new Point(0, 1);
            }
            else
            {
                if (board.GetTile(startPos + new Point(0, 1)) != null)
                {
                    startPos += new Point(0, 1);
                    retValue.Add(board.GetTile(startPos));
                }

            }
            
        }
        startPos = unit.tile.pos;
        for (int i = 0; i < crossLength; i++)
        {
            if (offset >= i)
            {
                startPos += new Point(1, 0);
            }
            else
            {
                if (board.GetTile(startPos + new Point(1 , 0)) != null)
                {
                    startPos += new Point(1, 0);
                    retValue.Add(board.GetTile(startPos));
                }
            }
        }
        startPos = unit.tile.pos;

        for (int i = 0; i < crossLength; i++)
        {
            if (offset >= i)
            {
                startPos += new Point(0, -1);
            }

            if (board.GetTile(startPos + new Point(0, -1)) != null)
            {
                startPos += new Point(0, -1);
                retValue.Add(board.GetTile(startPos));
            }
        }

        startPos = unit.tile.pos;

        for (int i = 0; i < crossLength; i++)
        {
            if(offset >= i)
            {
                startPos += new Point(-1, 0);
            }

            if (board.GetTile(startPos + new Point(-1, 0)) != null)
            {
                startPos += new Point(-1, 0);
                retValue.Add(board.GetTile(startPos));
            }
        }
        
        return retValue;
    }


    
}
