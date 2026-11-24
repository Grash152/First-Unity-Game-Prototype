using PROTO._Scripts.Collectibles;
using TMPro;
using UnityEngine;

namespace PROTO._Scripts.Characters.Cat
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI coinText;
        [SerializeField] public TextMeshProUGUI hayText;
        [SerializeField] private GameObject optionsMenu;
        public static float Invincible = 3f;
        public const float InvincibleDuration = 2f;
        public static int NumberOfHay;
        private static SpriteRenderer _spriteRenderer;
        private Animator _anim;
        private bool _menu;
        

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
        }
        

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _menu = !_menu;
            }
            if (_menu)
            {
                optionsMenu.SetActive(true);
            }
            else if (!_menu)
            {
                optionsMenu.SetActive(false);
            }
            
            coinText.text = "Coins: " + Coin.NumberOfCoins + " / 5";
            hayText.text = "Hay :" + NumberOfHay;
            if (Invincible >= 2.5f)
            {
                _spriteRenderer.color = Color.white;
            }
            if (Invincible < InvincibleDuration)
            {
                
                _spriteRenderer.color = Color.red;
            }
            else
                _spriteRenderer.color = Color.white;

            if (PlayerMove.LeftRight > 0f)
            {
                _spriteRenderer.flipX = false; // Facing Right
            }
            else if (PlayerMove.LeftRight < 0f)
            {
                _spriteRenderer.flipX = true;  // Facing Left
            }
            
            if (PlayerMove.LeftRight != 0f || PlayerMove.UpDown != 0f)
            {
                _anim.SetBool("IsRunning", true);
            }
            else if (PlayerMove.Velocity == new Vector3(0f,0f,0f))
            {
                _anim.SetBool("IsRunning", false);
            }

        }
    
    }
}
