using UnityEngine;

namespace Assets.Scripts
{
    public class MoveParentComponent : MonoBehaviour
    {
        public static MoveParentComponent instance { get; private set; }
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}