using game.control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace game.ui
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollView : MonoBehaviour
    {
        [SerializeField]
        private float scrollScale = 0.1f;
        [SerializeField]
        private Scrollbar scrollbar;

        private float _lastAxisX;
        private bool isTouch = false;
        // Start is called before the first frame update
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {
            OVRInput.FixedUpdate();

            if (OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _lastAxisX = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).x;
                isTouch = true;
            }
            else
            {
                if (OVRInput.GetUp(OVRInput.Touch.PrimaryTouchpad) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    isTouch = false;
                }
                else
                {
                    if (isTouch)
                    {
                        float vector = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad).x;
                        scrollbar.value += (vector - _lastAxisX) * scrollScale;
                        _lastAxisX = vector;
                    }
                }
            }
        }

        
    }

}