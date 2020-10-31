using game.data;
using game.objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace game.store
{
    public class Product : MonoBehaviour
    {

        [SerializeField]
        private bool isBought = false;
        [SerializeField]
        private Button buttonBuy;
        [SerializeField]
        private Button buttonSelect;
        [SerializeField]
        private Image buttonSelected;
        [SerializeField]
        private Image LockImage;

        [SerializeField]
        private GunInfo _gunInfo;
        public GunInfo gunInfo
        {
            get { return _gunInfo; }
            set { _gunInfo = value; }
        }


        private static Product lastProductSelected;

        public GunInfo gun
        {
            get { return _gunInfo; }
        }

        // Start is called before the first frame update
        void Start()
        {
            if(GlobalData.GetIndexCurrentGun() == _gunInfo.Index)
            {
                ChoiceThisGun();
            }
            else
            {
                if(gun.price == -1 || GlobalData.IsItUnlockedThisGun(_gunInfo.Index))
                {
                    SelectableGun();
                }
                else
                {
                    if (StoreManager.instance.currentLevelIndex <= _gunInfo.unlockLevel)
                    {
                        MakeLockGun();
                    }
                    else
                    {
                        MakeForBuy();
                    }
                }
            }
        }

        public void BuyProduct()
        {
            if(!isBought && GlobalData.IsItUnlockedThisGun(_gunInfo.Index))
            {
                if (gun.price <= GlobalData.TotalCoins())
                {
                    DeselectLastProduct();
                    GlobalData.BuyGun(_gunInfo.Index, gun.price);
                    isBought = true;
                    ChoiceThisGun();
                }
            }
        }

        public void SelectGun()
        {
            if (isBought)
            {
                DeselectLastProduct();
                ChoiceThisGun();
            }
        }

        private void ChoiceThisGun()
        {
            isBought = true;
            GlobalData.SetCurrentGunUsed(_gunInfo.Index);
            buttonSelected.gameObject.SetActive(true);
            buttonSelect.gameObject.SetActive(false);
            buttonBuy.gameObject.SetActive(false);
            LockImage.gameObject.SetActive(false);
        }

        private void MakeLockGun()
        {
            isBought = false;
            buttonSelected.gameObject.SetActive(false);
            buttonSelect.gameObject.SetActive(false);
            buttonBuy.gameObject.SetActive(false);
            LockImage.gameObject.SetActive(true);
        }
        private void MakeForBuy()
        {
            isBought = false;
        }

        private void SelectableGun()
        {
            isBought = true;
            buttonSelected.gameObject.SetActive(false);
            buttonSelect.gameObject.SetActive(true);
            buttonBuy.gameObject.SetActive(false);
            LockImage.gameObject.SetActive(false);
        }
        private void DeselectLastProduct()
        {
            if (lastProductSelected != null)
                lastProductSelected.gameObject.SetActive(false);
            lastProductSelected = this;
        }

    }
}
