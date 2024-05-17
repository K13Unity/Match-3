using UnityEngine;

public class ComboItemManager : MonoBehaviour
{
    [SerializeField] private ItemsSpritsComboBonusConfig _itemsSpritsComboBonusConfig;
    [SerializeField] private ItemCombo _itemCombo;
    [SerializeField] private RectTransform _comboZone;

    private int _id = 5;
    private int _currentNumber = 1;
    private int _maxNumber = 2;

    public void CreateComboItem(int Id)
    {
        Debug.Log(Id);
        if (_id == Id)
        {
            Debug.Log("Add");
            _currentNumber++;
            if (_currentNumber >= _maxNumber)
            {
                Debug.Log("Combo");
            }
        }
        else
        {
            Debug.Log("Reset");
            _id = Id;
            _itemCombo.Init(_id, _itemsSpritsComboBonusConfig.ItemsSprites[_id], _comboZone.rect.size);
            _currentNumber = 1;
        }
    }
}
