using UnityEngine;

namespace PROTO._Scripts.Characters.Npc
{
    public class NpcBounds : MonoBehaviour
    {
        [SerializeField] private Vector3 boundsSize = new Vector3(4f, 4f, 1f);
        [SerializeField] protected GameObject dialogueCanvas;
        public Vector3 boundsCenter;
        public Bounds npcBounds;

        private void Start()
        {
            boundsCenter = transform.position;
            npcBounds = new Bounds(boundsCenter, boundsSize);
        }
        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(npcBounds.center, npcBounds.size);
        }
        
    }
}
