using game.data;
using game.target;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace game.store
{
    public class StoreManager : MonoBehaviour
    {

        private static StoreManager _instance;

        public static StoreManager instance
        {
            get { return _instance; }
        }
        [SerializeField]
        private GameObject prefabProduct;

        private GunInfo[] _gunInfos;
        public GunInfo[] gunInfos
        {
            get { return _gunInfos; }
        }
        [SerializeField]
        private Transform content;
        private Product[] products;

        private int _currentLevelIndex;
        public int currentLevelIndex
        {
            get { return _currentLevelIndex; }
        }

        // Start is called before the first frame update
        void Awake()
        {
            _instance = this;
            
        }

        void Start()
        {
            _gunInfos = GlobalData.instance.gunInfos;
            _currentLevelIndex = GlobalData.GetTotalLevelsWin();

            products = new Product[_gunInfos.Length];

            for (int i = 0; i < _gunInfos.Length; i++)
            {
                if (!_gunInfos[i].isShootgun)
                {
                    products[i] = Instantiate(prefabProduct, content).GetComponent<Product>();
                    products[i].gunInfo = _gunInfos[i];
                }

            }
        }

        public void RefreshProductList()
        {
            for (int i = 0; i < _gunInfos.Length; i++)
                if (!_gunInfos[i].isShootgun)
                    products[i].Refresh();
        }

    }
}
