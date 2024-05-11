using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _text;
    public int Id { get; private set; }

    public void Init(int id, Sprite sprite, Vector2 size)
    {
        Id = id;
        _image.sprite = sprite;
        _rectTransform.sizeDelta = size;

    }
    
    public void Move(Slot slot, Action action = null)
    {
        transform.DOMove(slot.transform.position, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.SetParent(slot.transform);
            slot.SetItem(this);
            action?.Invoke();
        });
    }

    public void SetSlotPosition(Slot slot)
    {
        transform.SetParent(slot.transform);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        _text.text = $"{slot.yPos}{slot.xPos}";
    }
}
