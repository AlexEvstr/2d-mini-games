﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SlotMachine
{
    public class GameController : MonoBehaviour
    {
        private static GameController _ins;
        public static GameController ins
        {
            get
            {
                return _ins;
            }
        }

        public enum RewardEnum                  // Your Custom Reward Category
        {
            None,
            Coin,
            Diamond
        };

        [System.Serializable]
        public class MultiDimensionalArray
        {
            public RewardEnum rewardCategory;   //You should select reward category at inspector that determines the given reward when this slot selected
            public int rewardValue;             //All 3 rows same reward
            public int rewardChance;            //Chance to give this slot as result reward
            public Sprite slotIcon;             //This is aoutomaticaly using as this rows icon
        }

        [Header("Rewards Custom Settings")]
        [Space]
        public MultiDimensionalArray[] SlotTypes;

        [System.Serializable]
        public class RewarTable
        {
            public Image[] rewardImages;
            public Text rewardText;
            public Image rewardCurrencyIcon;
        }

        [Header("Reward List Table Settings")]  //Reward table is for show reward table to player which reward gives how many gold or diamond.
        [Space]
        public bool showRewardListTable;        //Set it true if you want to use reward table.
        public GameObject rewardTablePanel;
        public RewarTable[] RewardListTable;

        [Header("Game Inputs")]
        [Space]
        public int pullCost;                    // How much coins user spend when pull the handle
        public int chanceRatio;                 // Giving reward chance ratio
        public Column[] rows;

        [Header("Reward Popup Panel Inputs")]
        [Space]
        public Transform popupPanel;
        public Sprite rewardIconGold, rewardIconDiamond;
        private AudioGame _audioGame;

        [Header("UI Elements")]
        [Space]
        public Text priceText;
        public Text currentCoinsText;           // Pop-up text with spend or rewarded coins amount
        public Text currentDiamondsText;
        public Text rewardTextPopup;
        public Image rewardImagePopup;
        public Button pullButton;
        public Button pullHandle;

        [Header("Effect Settings")]
        [Space]
        public ParticleSystem confetiEffect;
        public Texture rewardTextureGold, rewardTextureDiamond; //Confetti effect icon changes acording to result reward currency.

        private string coinString = "totalGold";
        private string diamondString = "totalGem1";

        private int currentCoin;                 // Total coins amount. In your project it can be set up from PlayerProgress, DataController or from PlayerPrefs.
        private int previousCoin;                // For coin animation
        private int currentDiamond;              // Same as coin                
        private int previousDiamond;
        private int nextSlotIndex;
        private int rewardMultiplier = 1;       //As default it setted to 1. If player selects x2 button and watches rewarded as set this property to 2 to double prize.

        private const float seconds = 0.5f;

        private bool rewardSelected;
        private bool nextSlotSelected;
        private bool gameStarted;

        public int NextSlotIndex
        {
            get
            {
                return nextSlotIndex;
            }
        }

        public bool NextSlotSelected
        {
            get
            {
                return nextSlotSelected;
            }
        }

        private void Awake()
        {
            if (_ins == null)
                _ins = this;

            previousCoin = currentCoin;
            currentCoinsText.text = currentCoin.ToString();
            priceText.text = pullCost.ToString();

            GetPlayerProgress();

            if (showRewardListTable)
                RewardTableSettings();

            _audioGame = GetComponent<AudioGame>();
        }

        #region PlayerProgress
        private void GetPlayerProgress()
        {
            //Coin
            if (PlayerPrefs.HasKey(coinString))
            {
                currentCoin = PlayerPrefs.GetInt(coinString);
            }
            else
            {
                currentCoin = 100; //Default Coin Value
                PlayerPrefs.SetInt(coinString, currentCoin);
                PlayerPrefs.Save();
            }

            //Diamond
            if (PlayerPrefs.HasKey(diamondString))
            {
                currentDiamond = PlayerPrefs.GetInt(diamondString);
            }
            else
            {
                currentDiamond = 0;
                PlayerPrefs.SetInt(diamondString, currentDiamond);
            }

            PlayerPrefs.Save();
            StartCoroutine(UpdatePlayerProgress());
        }

        private IEnumerator UpdatePlayerProgress()
        {
            // Animation for increasing and decreasing of currencies amount
            float elapsedTime = 0;

            while (elapsedTime < seconds)
            {
                if (previousCoin != currentCoin)
                    currentCoinsText.text = Mathf.Floor(Mathf.Lerp(previousCoin, currentCoin, (elapsedTime / seconds))).ToString();
                if (previousDiamond != currentDiamond)
                    currentDiamondsText.text = Mathf.Floor(Mathf.Lerp(previousDiamond, currentDiamond, (elapsedTime / seconds))).ToString();

                elapsedTime += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            previousCoin = currentCoin;
            previousDiamond = currentDiamond;

            currentCoinsText.text = currentCoin.ToString();
            currentDiamondsText.text = currentDiamond.ToString();

            SavePlayerProgress(coinString, currentCoin);
            SavePlayerProgress(diamondString, currentDiamond);
        }

        private void SavePlayerProgress(string st, int value)
        {
            PlayerPrefs.SetInt(st, value);
            PlayerPrefs.Save();
        }
        #endregion PlayerProgress


        private void RewardTableSettings()
        {
            //rewardTablePanel.SetActive(true);

            for (int i = 0; i < SlotTypes.Length; i++)
            {
                RewardListTable[i].rewardImages[0].sprite = SlotTypes[i].slotIcon;
                RewardListTable[i].rewardImages[1].sprite = SlotTypes[i].slotIcon;
                RewardListTable[i].rewardImages[2].sprite = SlotTypes[i].slotIcon;

                RewardListTable[i].rewardText.text = "=" + SlotTypes[i].rewardValue.ToString();

                if (SlotTypes[i].rewardCategory == RewardEnum.Coin)
                    RewardListTable[i].rewardCurrencyIcon.sprite = rewardIconGold;
                else if (SlotTypes[i].rewardCategory == RewardEnum.Diamond)
                    RewardListTable[i].rewardCurrencyIcon.sprite = rewardIconDiamond;
            }
        }

        public void CheckResults() //Here we check IF we certanly giving a reward OR we may give reward by change or not giving it.
        {
            pullHandle.interactable = false; //Disable pull button untill result comes.
            pullButton.interactable = false;
            rewardSelected = false;

            if (CheckGivingReward())
            {
                Debug.Log("Give certanly a reward and select reward type acording to percentage from reward list.");
                nextSlotSelected = true;
                SelectReward();
            }
            else
            {
                Debug.Log("Here there is a still a change to give reward but it is not certain.");
                nextSlotSelected = false;
                SpinSlots();
            }
        }

        private bool CheckGivingReward()
        {
            int randomChange = Random.Range(0, 100);
            if (randomChange < chanceRatio) // % change according to developer input from inspector.
                return true;
            else
                return false;
        }

        public void SelectReward()
        {
            int randomValue = Random.Range(0, 100);

            for (int i = 0; i < SlotTypes.Length; i++)
            {
                if (randomValue <= SlotTypes[i].rewardChance && !rewardSelected)
                {
                    rewardSelected = true;
                    nextSlotIndex = i;
                }
            }

            SpinSlots();
        }

        public void SpinSlots()
        {
            if (currentCoin >= pullCost) // Player has enough money to play
            {
                if (rows[0].ColumnStopped && rows[1].ColumnStopped && rows[2].ColumnStopped)
                {
                    currentCoin -= pullCost;                    // Claim pull cost
                    StartCoroutine(UpdatePlayerProgress());
                    _audioGame.PlaySpinSound();

                    gameStarted = true;

                    rows[0].GetComponent<Column>().StartRotating();
                    rows[1].GetComponent<Column>().StartRotating();
                    rows[2].GetComponent<Column>().StartRotating();
                }
            }
            else
            {
                Debug.LogWarning("Player does not have enough gold. Here you should open the shop for in app purchase.");
                _audioGame.PlayDeclineSound();
            }
        }

        private void Update()
        {
            if (gameStarted)
            {
                if (rows[0].ColumnStopped && rows[1].ColumnStopped && rows[2].ColumnStopped)
                {
                    gameStarted = false;

                    if (rows[0].currentSlot == rows[1].currentSlot && rows[0].currentSlot == rows[2].currentSlot) //If there is a reward show panel
                        StartCoroutine(RewardPopup(nextSlotIndex));
                    else
                        ActivatePullButtons();
                }
            }
        }

        private void ActivatePullButtons()
        {
            pullButton.interactable = true;
            pullHandle.interactable = true;
        }

        IEnumerator RewardPopup(int rewardIndex)
        {
            yield return new WaitForSeconds(0.5f);

            if (SlotTypes[nextSlotIndex].rewardCategory == RewardEnum.Coin)
            {
                rewardImagePopup.sprite = rewardIconGold;
            }
            else if (SlotTypes[nextSlotIndex].rewardCategory == RewardEnum.Diamond)
            {
                rewardImagePopup.sprite = rewardIconDiamond;
            }

            rewardTextPopup.text = SlotTypes[rewardIndex].rewardValue.ToString();
            popupPanel.gameObject.SetActive(true);
            _audioGame.PlayWinSound();


            //If player clicks "Claim", give that reward Call ClaimReward() function OR if player selects "x2 Button", then show rewarded ad and then give double reward Call DoubleReward() function
        }

        public void CallRewardedAd()
        {
            Debug.LogWarning("Here you should call your show rewarded ad function and when rewarded ad completed call DoubleReward() function as result.");
        }

        private void DoubleReward()
        {
            rewardMultiplier = 2;
            Debug.Log("Rewards multiplier setted as " + rewardMultiplier);
            ClaimReward();
        }

        public void ClaimReward()
        {
            //Confeti Effect part
            if (SlotTypes[nextSlotIndex].rewardCategory == RewardEnum.Coin)
            {
                confetiEffect.GetComponent<ParticleSystemRenderer>().material.mainTexture = rewardTextureGold;
            }
            else if (SlotTypes[nextSlotIndex].rewardCategory == RewardEnum.Diamond)
            {
                confetiEffect.GetComponent<ParticleSystemRenderer>().material.mainTexture = rewardTextureDiamond;
            }

            confetiEffect.Play();
            popupPanel.gameObject.SetActive(false);
            _audioGame.PlayClickSound();

            //Add Rewards
            GiveReward();
        }

        private void GiveReward()
        {
            if (SlotTypes[nextSlotIndex].rewardCategory == RewardEnum.Coin) //Give reward as Coin
            {
                currentCoin += SlotTypes[nextSlotIndex].rewardValue * rewardMultiplier;
            }
            else if (SlotTypes[nextSlotIndex].rewardCategory == RewardEnum.Diamond) //Give reward as Diamond
            {
                currentDiamond += SlotTypes[nextSlotIndex].rewardValue * rewardMultiplier;
            }
            else
            {
                Debug.LogWarning("Reward can not be given unless Reward Categorgy selected for this slot... Here slot index: " + nextSlotIndex);
            }

            rewardMultiplier = 1;   //Reset for next rewards
            StartCoroutine(UpdatePlayerProgress());
            ActivatePullButtons();
        }
    }
}