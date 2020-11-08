using game.data;
using game.objects;
using game.ui;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace game.store
{
    public class Product : MonoBehaviour
    {

        [SerializeField]
        private Button buttonBuy;
        [SerializeField]
        private Button buttonSelect;
        [SerializeField]
        private Image buttonSelected;
        [SerializeField]
        private Image LockImage;

        [Header("Gun Info")]
        private GunInfo _gunInfo;
        public GunInfo gunInfo
        {
            get { return _gunInfo; }
            set { _gunInfo = value; }
        }

        [SerializeField]
        private Image movingOnShoot;
        [SerializeField]
        private Image focusRadius;
        [SerializeField]
        private Image additionalBullets;

        private bool isBaught = false;
        private bool isLock = false;

        private static Product lastProductSelected;
        private void Start()
        {
            Refresh();
            _gunInfo.UpdateDataOnCanvas(movingOnShoot, focusRadius, additionalBullets);
        }

        public void Refresh()
        {
            if (GlobalData.GetIndexCurrentGun() == _gunInfo.Index)
            {
                lastProductSelected = this;
                isLock = false;
                isBaught = true;
                ChoiceThisGun();
                return;
            }
            if (GlobalData.WeHaveThisGun(_gunInfo.Index))
            {
                isLock = false;
                isBaught = true;
                SelectableGun();
                return;
            }
            if (StoreManager.instance.currentLevelIndex < _gunInfo.unlockLevel)
            {
                isLock = true;
                MakeLockGun();
                return;
            }

            MakeForBuy();
        }

        private void MakeForBuy()
        {
            isLock = false;
            isBaught = false;
            buttonSelected.gameObject.SetActive(false);
            buttonSelect.gameObject.SetActive(false);
            buttonBuy.gameObject.SetActive(true);
            LockImage.gameObject.SetActive(false);
        }

        private void ChoiceThisGun()
        {
            lastProductSelected = this;
            buttonSelected.gameObject.SetActive(true);
            buttonSelect.gameObject.SetActive(false);
            buttonBuy.gameObject.SetActive(false);
            LockImage.gameObject.SetActive(false);

        }

        private void DeselectGun()
        {
            buttonSelected.gameObject.SetActive(false);
            buttonSelect.gameObject.SetActive(true);
            buttonBuy.gameObject.SetActive(false);
            LockImage.gameObject.SetActive(false);
        }

        private void MakeLockGun()
        {
            buttonSelected.gameObject.SetActive(false);
            buttonSelect.gameObject.SetActive(false);
            buttonBuy.gameObject.SetActive(false);
            LockImage.gameObject.SetActive(true);
        }

        private void SelectableGun()
        {
            buttonSelected.gameObject.SetActive(false);
            buttonSelect.gameObject.SetActive(true);
            buttonBuy.gameObject.SetActive(false);
            LockImage.gameObject.SetActive(false);
        }

        private void DeselectLastProduct()
        {
            if (lastProductSelected != null)
                lastProductSelected.DeselectGun();

            lastProductSelected = this;
        }

        #region Buttons function
        public void BuyProduct()
        {
            if(!isBaught)
            {
                if (_gunInfo.price <= GlobalData.TotalCoins())
                {
                    isBaught = true;
                    GlobalData.ChangeIndexCurrentGun(_gunInfo.Index);
                    GlobalData.MinusCoin(_gunInfo.price);
                    GlobalData.TakeThisGun(_gunInfo.Index);
                    DeselectLastProduct();
                    ChoiceThisGun();
                }
            }

            StoreManager.instance.RefreshProductList();
        }

        public void SelectGun()
        {
            if (isBaught)
            {
                DeselectLastProduct();
                ChoiceThisGun();
            }
        }
        #endregion
    }
}
