using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsSpritesProgressConfig", menuName = "ItemsSpritesProgressConfig")]
public class ItemsSpritesProgressConfig : ScriptableObject
{
    [SerializeField] private List<Sprite> _itemsSprites;

    public IReadOnlyList<Sprite> ItemsSprites => _itemsSprites;
}
