using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tankfender
{
    public class HPManager : MonoBehaviour
    {
        [SerializeField] private List<HPUIElement> playerHP;
        private int currentPlayerHP;
        private int currentMaxPlayerHP;
        private int maxPlayerHP;
        private int heartQuantity;


        public void SetParameters(int currentPlayerHP, int currentMaxPlayerHP)
        {
            this.maxPlayerHP = playerHP.Count;
            SetCurrentMaxHP(currentMaxPlayerHP);
            SetCurrentHP(currentPlayerHP);

        }

        public void SetCurrentHP(int newPlayerHP)
        {
            this.currentPlayerHP = newPlayerHP;
            for (int i = 0; i < currentMaxPlayerHP; i++)
            {
                if (currentPlayerHP > i)
                {
                    playerHP[i].Full();
                }
                else
                {
                    playerHP[i].Empty();
                }
            }
        }

        public void SetCurrentMaxHP(int currentMaxPlayerHP)
        {
            this.currentMaxPlayerHP = currentMaxPlayerHP;
            for (int i = 0; i < playerHP.Count - 1; i++)
            {
                if (currentMaxPlayerHP >= i + 1)
                {
                    playerHP[i].gameObject.SetActive(true);
                }
                else
                {
                    playerHP[i].gameObject.SetActive(false); ;
                }
            }
        }

        public void AddOneMoreMaxHP()
        {
            if (currentMaxPlayerHP + 1 <= maxPlayerHP)
            {
                playerHP[currentMaxPlayerHP].gameObject.SetActive(true);
                this.currentMaxPlayerHP += 1;
            }
            else
            {
                Debug.Log("Maxima cantidad de hp alcanzada");
            }
        }

        public int GetHPQuantity()
        {
            return playerHP.Count;
        }

        public void ReduceHPBy1()
        {
            SetCurrentHP(currentPlayerHP - 1);
        }

        public void Refill()
        {
            SetCurrentHP(currentMaxPlayerHP);
        }
    }
}
