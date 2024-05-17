using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private const int _slotsCount = 8;
        [SerializeField] private Slot _slotPrefab;
        [SerializeField] private Item _itemPrefab;
        [SerializeField] private ItemsSpritsConfig _itemsSpritsConfig;

        public  int SlotsCount => _slotsCount;
        public Slot SlotPrefab => _slotPrefab;
        public Item ItemPrefab => _itemPrefab;
        public ItemsSpritsConfig ItemsSpritsConfig => _itemsSpritsConfig;
    }
}
