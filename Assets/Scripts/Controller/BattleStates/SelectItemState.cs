using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemState : BattleState
{
    int currentItemIndex;
    public override void Enter()
    {
        base.Enter();
        owner.isTimeLineActive = false;
        owner.moveItemSelector = true;
        if (ActionSelectionUI.gameObject.activeSelf == false)
        {
            ActionSelectionUI.gameObject.SetActive(true);
        }
        ItemSelectionUI.gameObject.SetActive(true);

        //Abilities[] a = owner.currentUnit.weapon.Abilities;
        List<consumableSlot> a = owner.inventory.ConsumableList;

        for (int i = 0; i < owner.itemSelectionUI.options.Length; i++)
        {

            ItemSelectionUI.options[i].GetComponent<Text>().text = a[i].Consumable.ItemName;

            //if (owner.currentUnit.stamina < a[i].staminaCost)
            //{
            //    AbilitySelectionUI.DisableSelectAbilty(i);
            //}
            //else
            //{
            //    AbilitySelectionUI.EnableSelectAbilty(i);
            //}

        }


        owner.itemSelectionUI.ResetSelector();
        //Meter ActivarUI
    }

    protected override void OnEscape(object sender, InfoEventArgs<KeyCode> e)
    {

        owner.ChangeState<SelectActionState>();
    }
    protected override void OnMouseCancelEvent(object sender, InfoEventArgs<KeyCode> e)
    {
        owner.ChangeState<SelectActionState>();
    }
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (e.info.y >= 1)
        {
            ItemSelectionUI.MoveBackwards();

            if (currentItemIndex > 0)
            {
                currentItemIndex--;
            }
            else
            {
                //???
                //currentActionIndex = owner.currentUnit.weapon.Abilities.Length - 1;
                currentItemIndex = owner.inventory.ConsumableList.Count - 1;
            }

        }

        if (e.info.y <= -1)
        {
            ItemSelectionUI.MoveForward();

            if (currentItemIndex < owner.inventory.ConsumableList.Count - 1)
            {
                currentItemIndex++;
            }
            else
            {
                currentItemIndex = 0;
            }
        }
    }

    protected override void OnFire(object sender, InfoEventArgs<KeyCode> e)
    {
        //if (owner.currentUnit.weapon.Abilities[currentActionIndex].CanBeUsed(owner.currentUnit.stamina))
        //{
        //    owner.attackChosen = currentActionIndex;
        //    ActionSelectionUI.gameObject.SetActive(false);
        //    owner.ChangeState<UseAbilityState>();
        //}


        //owner.inventory.UseConsumable(currentActionIndex, owner.currentUnit);
        //owner.ChangeState<FinishPlayerUnitTurnState>();

        owner.itemChosen = currentItemIndex;
        owner.ChangeState<UseItemState>();
    }

    protected override void OnSelectAction(object sender, InfoEventArgs<int> e)
    {
        //owner.inventory.UseConsumable(e.info, owner.currentUnit);
        //owner.ChangeState<FinishPlayerUnitTurnState>();

        owner.itemChosen = e.info;
        owner.ChangeState<UseItemState>();
    }


    public override void Exit()
    {
        base.Exit();
        owner.moveItemSelector = false;
        currentItemIndex = 0;
        ItemSelectionUI.ResetSelector();
        ItemSelectionUI.gameObject.SetActive(false);
    }

   

    //IEnumerator Init()
    //{
    //    Debug.Log("se ha utilizado Potion");
    //    yield return null;
    //    owner.inventory.UseConsumable(0, owner.currentUnit);
    //    owner.ChangeState<SelectActionState>();

    //}


}


