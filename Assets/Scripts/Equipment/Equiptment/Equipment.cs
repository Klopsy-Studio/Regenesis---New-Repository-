using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    GreatSword,
    Blowgun,
    Helmet,
    Chest,
    Gloves,
}


public abstract class Equipment : ScriptableObject
{
    [SerializeField] private string equipmentName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private EquipmentType equipmentType;

    public string EquipmentName { get { return equipmentName; } }
    public Sprite Sprite { get { return sprite; } }
    public EquipmentType EquipmentType { get {return equipmentType;}}

    public abstract void EquipItem(Unit c);
  
}
