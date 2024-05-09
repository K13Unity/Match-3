using UnityEngine;

public class ComboItemManager : MonoBehaviour
{
    [SerializeField] private ItemsSpritsComboBonusConfig _itemsSpritsComboBonusConfig;
    [SerializeField] private ItemCombo _itemCombo;
    [SerializeField] private RectTransform _comboZone;

    public void CreateComboItem()
    {
        var id = Random.Range(0, _itemsSpritsComboBonusConfig.ItemsSprites.Count);
        _itemCombo.Init(id, _itemsSpritsComboBonusConfig.ItemsSprites[id], _comboZone.rect.size);
    }
}
