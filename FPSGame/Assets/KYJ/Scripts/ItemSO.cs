using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemIcon;
}
