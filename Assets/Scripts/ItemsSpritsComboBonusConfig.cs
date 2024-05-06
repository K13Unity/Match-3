using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsSpritsComboBonusConfig", menuName = "Items Sprits Combo Bonus Config")]
public class ItemsSpritsComboBonusConfig : ScriptableObject
{
    [SerializeField] private List<Sprite> _itemsSprites;

    public IReadOnlyList<Sprite> ItemsSprites => _itemsSprites;
}

