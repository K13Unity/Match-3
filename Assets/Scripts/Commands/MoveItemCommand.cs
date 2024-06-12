using System;
using DG.Tweening;

namespace Assets.Scripts
{
    public class MoveItemCommand
    {
        public void Execute(Slot fromSlot, Slot toSlot, Action onComplete = default)
        {
            fromSlot.isBusy = true;
            toSlot.isBusy = true;
            
            Item fromItem = fromSlot.item;
            Item toItem = toSlot.item;
            
            fromSlot.item = toItem;
            toSlot.item = fromItem;

            fromItem.transform.SetParent(MoveParentComponent.instance.transform);
            if (toItem != default)
                toItem.transform.SetParent(MoveParentComponent.instance.transform);
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(fromItem.transform
                .DOMove(toSlot.transform.position, 0.9f).SetEase(Ease.Linear));
            
            if (toItem != default)
            {
                sequence.Join(toItem.transform
                    .DOMove(fromSlot.transform.position, 0.9f).SetEase(Ease.Linear));
            }

            
            sequence.OnComplete(() =>
            {
                fromItem.transform.SetParent(toSlot.transform);
                
                if (toItem != default)
                    toItem.transform.SetParent(fromSlot.transform);
                
                fromSlot.SetItem(toItem);
                toSlot.SetItem(fromItem);
                
                fromItem.SetName($"{fromSlot.yPos}{fromSlot.xPos}");
                
                if (toItem != default)
                    toItem.SetName($"{toSlot.yPos}{toSlot.xPos}");
                
                fromSlot.isBusy = false;
                toSlot.isBusy = false;
                
                onComplete?.Invoke();
            });
        }
    }
}