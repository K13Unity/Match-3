using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProgressSlot : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    public int xPos { get; private set; }
    public int yPos { get; private set; }
    public ItemProgress item { get; private set; }

    public void Init(int x, int y, Vector2 size)
    {
        xPos = x;
        yPos = y;
        _rectTransform.sizeDelta = size;
    }
    public void SetItem(ItemProgress item)
    {
        this.item = item;
    }
}