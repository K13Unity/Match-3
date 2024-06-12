using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsSpritsConfig", menuName = "Items Sprits Config")]

public class ItemsSpritsConfig : ScriptableObject
{
    [SerializeField] private List<Sprite> _itemsSprites;

    public IReadOnlyList<Sprite> ItemsSprites => _itemsSprites;
}
