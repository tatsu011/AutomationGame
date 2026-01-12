using UnityEngine;

[CreateAssetMenu(menuName = "Factory/Item")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public string displayName;
    public Sprite icon;
}