using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    public int buyValue;
    public int sellValue;
    public Sprite sprite;
}