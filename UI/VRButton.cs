using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace game.ui
{
    public class VRButton : MonoBehaviour
    {
        [SerializeField]
        public UnityEvent onClick;
    }
}
