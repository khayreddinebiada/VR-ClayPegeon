using game.data;
using game.objects;
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
        [SerializeField]
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
            _currentLevelIndex = GlobalData.GetIndexLevelsWin();
            products = new Product[_gunInfos.Length];

            for (int i = 0; i < _gunInfos.Length; i++)
            {
                products[i] = Instantiate(prefabProduct, content).GetComponent<Product>();
                products[i].gunInfo = _gunInfos[i];
                
            }
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
