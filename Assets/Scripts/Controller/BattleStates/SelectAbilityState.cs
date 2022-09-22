using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAbilityState : BattleState
{
    public int currentActionIndex;
    bool canShowUI;

    public override void Enter()
    {
        base.Enter();
        owner.actionSelectionUI.gameObject.SetActive(true);
        owner.isTimeLineActive = false;
        owner.moveAbilitySelector = true;

        AbilitySelectionUI.gameObject.SetActive(true);

        Abilities[] a = owner.currentUnit.weapon.Abilities;


        for (int i = 0; i < owner.abilitySelectionUI.options.Length; i++)
        {
            AbilitySelectionUI.options[i].GetComponent<Text>().text = a[i].abilityName;
        }

        
        owner.abilitySelectionUI.ResetSelector();
        //Meter ActivarUI
    }

    protected override void OnEscape(object sender, InfoEventArgs<KeyCode> e)
    {
        owner.ChangeState<SelectActionState>();
    }

    protected override void OnMouseCancelEvent(object sender, InfoEventArgs<KeyCode> e)
    {
        if(!owner.abilitySelectionUI.onOption)
        {
            owner.ChangeState<SelectActionState>();
        }
        else
        {
            owner.attackChosen = AbilitySelectionUI.currentSelection;
            owner.ChangeState<CheckAbilityDetailsState>();
        }
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (e.info.y >= 1)
        {
            AbilitySelectionUI.MoveBackwards();
            
            if(currentActionIndex > 0)
            {
                currentActionIndex--;
            }

            else
            {
                currentActionIndex = owner.currentUnit.weapon.Abilities.Length - 1;
            }

        }

        if (e.info.y <= -1)
        {
            AbilitySelectionUI.MoveForward();

            if(currentActionIndex < owner.currentUnit.weapon.Abilities.Length - 1)
            {
                currentActionIndex++;
            }
            else
            {
                currentActionIndex = 0;
            }
        }
    }

    protected override void OnSelectAction(object sender, InfoEventArgs<int> e)
    {
        owner.attackChosen = e.info;

        ActionSelectionUI.gameObject.SetActive(false);
        owner.ChangeState<UseAbilityState>();
    }

    protected override void OnSelectCancelEvent(object sender, InfoEventArgs<int> e)
    {
        owner.attackChosen = e.info;
    }

    protected override void OnFire(object sender, InfoEventArgs<KeyCode> e)
    {
        owner.attackChosen = currentActionIndex;

        ActionSelectionUI.gameObject.SetActive(false);
        owner.ChangeState<UseAbilityState>();
    }

    public override void Exit()
    {
        base.Exit();
        owner.moveAbilitySelector = false;
        currentActionIndex = 0;
        AbilitySelectionUI.ResetSelector();
        AbilitySelectionUI.gameObject.SetActive(false);
        AbilitySelectionUI.onOption = false;
    }
}
