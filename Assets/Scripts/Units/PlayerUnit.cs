using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit  
{
    public UnitProfile profile;
    public SpriteRenderer unitSprite;
    [HideInInspector] public Sprite unitPortrait;
    public bool didNotMove;
    public Weapons weapon;
    public Armor armor;


    public bool changing;

    [HideInInspector] public UnitStatus status;
    protected override void Start()
    {
        base.Start();
        didNotMove = true;
        timelineFill = Random.Range(0, 50);
        //ESTO DEBER�A DE ESTAR EN UNIT

        timelineTypes = TimeLineTypes.PlayerUnit;
        EquipAllItems();
    }


    //ESTA FUNCION HAY QUE REVISARLA
    public void EquipAllItems()
    {
        if (weapon == null || armor == null) { return; }
        health.baseValue = 100;

        
        weapon.EquipItem(this);
        armor.EquipItem(this);
        maxHealth.baseValue = health.Value;

    }

    public bool CanMove()
    {
       
        return didNotMove;
        
    }


    public bool CanDoAbility()
    {
        foreach(Abilities a in weapon.Abilities)
        {
            if(stamina >= a.staminaCost)
            {
                return true;
            }
        }

        return false;
    }

    public override void React()
    {
        base.React();
        unitSprite.sprite = profile.unitReaction;
    }
    public override void Attack()
    {
        unitSprite.sprite = profile.unitAction;
    }

    public override void Default()
    {
        unitSprite.sprite = profile.unitSprite;
    }




}
