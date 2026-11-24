using UnityEngine;

namespace PROTO._Scripts.Destructibles

{
    public class Bush : Destructibles
    {
        [SerializeField] private GameObject prefab;
        

        private void Start()
        {
            IsQuittingOrInitializing = false; 
        }
        private void OnDisable()
        {
            if (IsQuittingOrInitializing || noDrops) return;
            DropItem();
        }

        private void OnApplicationQuit()
        {
            IsQuittingOrInitializing = true;
        }

        protected override void DropItem() 
        {
            if(prefab != null )
                Instantiate(prefab, transform.position, transform.rotation);
           
        }
        
    }
}
