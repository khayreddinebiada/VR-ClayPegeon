using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace game.control
{
    public class ControllerInputs : MonoBehaviour
    {


        public Vector3 deltaDroping;
        public bool invisibleMouseOnStarting = true;

        // Start is called before the first frame update
        void Start()
        {
            if(invisibleMouseOnStarting)
                Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            OnPCController();
        }

        private void OnPCController()
        {
            deltaDroping = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        }

    }
}
