using UnityEngine;

public class ComboSlot : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    public ItemCombo item { get; private set; }

    public void Init(Vector2 size)
    {
        _rectTransform.sizeDelta = size;
    }

    public void SetItem(ItemCombo item)
    {
        this.item = item;
    }
    public ItemCombo GetItem()
    {
        return item;
    }
}
