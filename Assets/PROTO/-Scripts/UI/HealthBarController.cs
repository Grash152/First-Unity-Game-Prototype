/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;

namespace PROTO._Scripts.UI
{
    public class HealthBarController : MonoBehaviour
    {
        private GameObject[] _heartContainers;
        private Image[] _heartFills;

        public Transform heartsParent;
        public GameObject heartContainerPrefab;

        private void Start()
        {
            // Should I use lists? Maybe :)
            _heartContainers = new GameObject[(int)PlayerStats.Instance.MaxTotalHealth];
            _heartFills = new Image[(int)PlayerStats.Instance.MaxTotalHealth];

            PlayerStats.Instance.OnHealthChangedCallback += UpdateHeartsHUD;
            InstantiateHeartContainers();
            UpdateHeartsHUD();
        }

        private void UpdateHeartsHUD()
        {
            SetHeartContainers();
            SetFilledHearts();
        }

        void SetHeartContainers()
        {
            for (int i = 0; i < _heartContainers.Length; i++)
            {
                if (i < PlayerStats.Instance.MaxHealth)
                {
                    _heartContainers[i].SetActive(true);
                }
                else
                {
                    _heartContainers[i].SetActive(false);
                }
            }
        }

        void SetFilledHearts()
        {
            for (int i = 0; i < _heartFills.Length; i++)
            {
                if (i < PlayerStats.Instance.Health)
                {
                    _heartFills[i].fillAmount = 1;
                }
                else
                {
                    _heartFills[i].fillAmount = 0;
                }
            }

            if (PlayerStats.Instance.Health % 1 != 0)
            {
                int lastPos = Mathf.FloorToInt(PlayerStats.Instance.Health);
                _heartFills[lastPos].fillAmount = PlayerStats.Instance.Health % 1;
            }
        }

        void InstantiateHeartContainers()
        {
            for (int i = 0; i < PlayerStats.Instance.MaxTotalHealth; i++)
            {
                GameObject temp = Instantiate(heartContainerPrefab);
                temp.transform.SetParent(heartsParent, false);
                _heartContainers[i] = temp;
                _heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
            }
        }
    }
}
