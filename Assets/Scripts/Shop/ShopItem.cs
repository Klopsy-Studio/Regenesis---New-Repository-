using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "shopMenu", menuName = "New Shop Item")]
public class ShopItem : ScriptableObject
{
    public string title;
    public string description;
    public int baseCost;

}
