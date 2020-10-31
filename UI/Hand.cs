using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game.ui
{
    public class Hand : MonoBehaviour
    {
        public Transform body;
        public LineRenderer lineRenderer;


        private void Update()
        {
            lineRenderer.SetPosition(0, transform.position);
        }

        public void ChangePositionHitPoint()
        {
            lineRenderer.SetPosition(1, transform.position + transform.forward);
        }
        public void ChangePositionHitPoint(Vector3 position)
        {
            lineRenderer.SetPosition(1, position);
        }
    }
}
