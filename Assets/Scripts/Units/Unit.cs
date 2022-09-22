using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//Right now, this script track if the unit is on the board and what direction is facing
//In the future, we should add stats of the unit
public class Unit : TimelineElements
{


    [Space]
    public Directions dir;
    public Tile tile { get; protected set; }
    public float unitSpeed;
    public override TimelineVelocity TimelineVelocity
    {
        get { return timelineVelocity; }

        set { timelineVelocity = value; SetCurrentVelocity();}
    }


    //When unit uses its action, the turn goes to the next unit
    public bool turnEnded;
    public bool actionDone;
    public Point currentPoint;

    [Header("PLACEHOLDER")]
    public Material disabledMaterial;
    public Material enabledMaterial;
    public MeshRenderer renderer;

    //Variables que se comparten entre unidades del jugador y del enemigo
    public Stats maxHealth;
    public Stats health;
    public Stats damage;
    public int stamina;

    public AnimationClip hurtAnimation;

    public string unitName;

    public GameObject particleHitPrefab;

    public bool isInAction = false;

    [HideInInspector] public bool isDead;

    [Header("Particles")]
    [SerializeField] GameObject healEffect;
    [SerializeField] GameObject hitEffect;

    protected virtual void Start()
    {
        Match();
        SetCurrentVelocity();
        stamina = 100;
        damage.baseValue = 10;
    }

   

    public void Place(Tile target)
    {
        // Make sure old tile location is not still pointing to this unit
        if (tile != null && tile.content == gameObject)
            tile.content = null;
        // Link unit and tile references
        tile = target;

        if (target != null)
            target.content = gameObject;
    }

    public void Match()
    {
        transform.localPosition = tile.center;
        transform.localEulerAngles = dir.ToEuler();
        currentPoint = tile.pos;
    }

    public void DisableUnit()
    {
        renderer.material = disabledMaterial;
    }

    public void EnableUnit()
    {
        turnEnded = false;
        actionDone = false;  
        renderer.material = enabledMaterial;
    }

    public virtual bool ReceiveDamage(float damage)
    {
        health.AddModifier(new StatsModifier(-damage, StatModType.Flat, this));

        if (health.Value <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool ReceiveDamageStun(float damage, float StunDMG)
    {
        health.AddModifier(new StatsModifier(-damage, StatModType.Flat, this));

        if (health.Value <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void Heal(float heal)
    {
        health.AddModifier(new StatsModifier(heal, StatModType.Flat, this));

        if(health.Value >= maxHealth.Value)
        {
            health.baseValue = maxHealth.Value;
        }
    }

   

    public void SetCurrentVelocity()
    {
        switch (timelineVelocity)
        {
            case TimelineVelocity.None:
                fTimelineVelocity = 25f;
                break;
            case TimelineVelocity.Quick:
                fTimelineVelocity = 25f;
                break;
            case TimelineVelocity.Normal:
                fTimelineVelocity = 20;
                break;
            case TimelineVelocity.Slow:
                fTimelineVelocity = 15f;
                break;
            case TimelineVelocity.VerySlow:
                fTimelineVelocity = 10;
                break;
            default:
                break;
        }
        //Debug.Log("CURRENT VELOCITY TYPE " + currentVelocity);
    }

    public override bool UpdateTimeLine()
    {
        if (timelineFill >= timelineFull)
        {
            return true;
        }
        timelineFill += fTimelineVelocity * Time.deltaTime;
        //Debug.Log(gameObject.name + "timelineFill " + timelineFill);

        return false;
    }

    public void DebugThings()
    {
        Debug.Log( this.unitName + "current velocity: " + timelineVelocity);
    }


    public virtual void React()
    {

    }

    public virtual void Default()
    {

    }

    public virtual void Attack()
    {

    }


    public void DamageEffect()
    {
        GameObject temp = Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), hitEffect.transform.rotation);
        Destroy(temp, 0.8f);
    }

    public void HealEffect()
    {
        GameObject temp = Instantiate(healEffect, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), healEffect.transform.rotation);
        Destroy(temp, 0.8f);
    }
}
