using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game.lib
{
    public class MovementPoint : MonoBehaviour
    {
        public bool isStartPoint = false;
        public int orderStartPoint = -1;
        public bool isDrawGizmos = true;
        public Color GizmosColor = Color.white;
        public Transform nextPoint;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        void OnDrawGizmos()
        {
            if (isDrawGizmos)
            {
                nextPoint = NextChild();


                Gizmos.color = GizmosColor;
                Gizmos.DrawLine(transform.position, nextPoint.position);
            }
        }

        private Transform NextChild()
        {
            // Check where we are
            int thisIndex = this.transform.GetSiblingIndex();

            // We have a few cases to rule out
            if (this.transform.parent == null)
                return null;

            if (this.transform.parent.childCount <= thisIndex + 1)
                return this.transform.parent.GetChild(0).GetComponent<Transform>();

            // Then return whatever was next, now that we're sure it's there
            return this.transform.parent.GetChild(thisIndex + 1).GetComponent<Transform>();
        }


        private Transform PreviousChild()
        {
            // Check where we are
            int thisIndex = this.transform.GetSiblingIndex();

            // We have a few cases to rule out
            if (this.transform.parent == null)
                return null;

            if (thisIndex == 0)
                return this.transform.parent.GetChild(this.transform.parent.childCount - 1).GetComponent<Transform>();

            // Then return whatever was next, now that we're sure it's there
            return this.transform.parent.GetChild(thisIndex - 1).GetComponent<Transform>();
        }

    }

}