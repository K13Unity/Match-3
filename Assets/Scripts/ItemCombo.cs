using UnityEngine;
using UnityEngine.UI;

public class ItemCombo : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _rectTransform;

    public int Id { get; private set; }

    public void Init(int id, Sprite sprite, Vector2 size)
    {
        Id = id;
        _image.sprite = sprite;
        _image.color = new Color(1, 1, 1, 1f);
       // _rectTransform.sizeDelta = size;
    }
}
