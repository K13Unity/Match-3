using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _rectTransform;
    
    private Vector3 _startDragPoint;
    private Vector3 _endDragPoint;

    public Item item;
    public int xPos { get; private set; }
    public int yPos { get; private set; }

    public bool isBusy;
    public bool isBurning;

    public event Action <Slot, int> onHorisontal;
    public event Action <Slot, int> onVertical;


    public void Init(int x, int y, Vector2 size)
    {
        xPos = x;
        yPos = y;
        _rectTransform.sizeDelta = size;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startDragPoint = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _endDragPoint = Input.mousePosition;
        if (_startDragPoint == _endDragPoint) return;
        var direction = Vector3.Normalize(_endDragPoint - _startDragPoint);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0.0f) onHorisontal?.Invoke(this, 1);
            else onHorisontal?.Invoke(this, -1);
        }
        else if (direction.y > 0.0f) onVertical?.Invoke(this, 1);
        else onVertical?.Invoke(this, -1);
    }

    public void SetItem(Item item)
    {
        this.item = item;
        if (item != null) item.SetSlotPosition(this);

    }
}
