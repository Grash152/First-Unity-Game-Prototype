using UnityEngine;
using UnityEngine.Tilemaps;

namespace PROTO._Scripts.Characters.Cat
{
    public class PlayerMove :  MonoBehaviour
    {
    
        public float maxSpeed = 5f;
        private float _acceleration = 3f;
        private readonly float _deceleration = 2f;
        private Vector3 _direction = new Vector3(0f, 0f, 0f);
        public static Vector3 Velocity = new Vector3(5f, 0f, 5f);
        private float _dash;
        private readonly float _dashSpeed = 15f;
        private readonly float _dashDuration = 0.5f;
        private readonly float _dashCooldown = 1f;
        private bool _isDashing;
        public Tilemap obstacleTilemap;
        public static float LeftRight;
        public static  float UpDown;


        // Update is called once per frame
        private void Update()
        {
            LeftRight = Input.GetAxisRaw("Horizontal");
            UpDown = Input.GetAxisRaw("Vertical");
            if (Input.GetKeyDown(KeyCode.Space) && _dash <= 0f && !_isDashing)
            {
                _dash = _dashCooldown;
                _acceleration += _dashSpeed * _dashDuration;
                _isDashing = true;
            }

            if (_isDashing)
            {
                _acceleration -= _dashSpeed * Time.deltaTime;
                if(_acceleration <= 2f)
                    _isDashing = false;
            }
            if (_dash <= 0f)
            {
                _dash = 0f;
            }
            else
            {
                _dash -= Time.deltaTime;
            }
            if (LeftRight != 0 || UpDown != 0 && !_isDashing)
            {
                if (_acceleration <= 0f && !_isDashing)
                    _acceleration = 2f;
                if (_acceleration <= maxSpeed && !_isDashing)
                    _acceleration += _acceleration * Time.deltaTime;
                _direction = new Vector3(LeftRight,UpDown,0f).normalized;
 
            }
            else
            {
                _acceleration -= _deceleration * Time.deltaTime;
                if (_acceleration <= 0f)
                    _acceleration = 0f;
            }
            
            Velocity = _direction * _acceleration;
            Vector3 targetPosition = transform.position + Velocity * Time.deltaTime;
            Vector3Int targetCell = obstacleTilemap.WorldToCell(targetPosition);
            PROTO._Scripts.Destructibles.Destructibles[] spawned  = Object.FindObjectsByType<PROTO._Scripts.Destructibles.Destructibles>();
            bool pathIsClear = !obstacleTilemap.HasTile(targetCell);
            if (pathIsClear)
            { 
                foreach (PROTO._Scripts.Destructibles.Destructibles obj in spawned)
                {
                    if (obj != null)
                    {
                        if (PlayerBounds.FutureBounds.Intersects(obj.MyBounds))
                        {
                            pathIsClear = false;
                            break;

                        }
                    }
                }
            
            }

            if (pathIsClear)
            {
                transform.position = targetPosition;
            }
        }
    }
}
