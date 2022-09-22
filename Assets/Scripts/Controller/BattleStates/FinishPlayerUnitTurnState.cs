using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlayerUnitTurnState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(FinishTurnCoroutine());
    }

    public override void Exit()
    {
        base.Exit();
        owner.isTimeLineActive = true;
    }

    IEnumerator FinishTurnCoroutine()
    {
        owner.currentUnit.SetCurrentVelocity();
        owner.currentUnit.didNotMove = true;
        owner.currentUnit.timelineFill = 0;
        
        yield return null;
        owner.ChangeState<TimeLineState>();
    }
}
