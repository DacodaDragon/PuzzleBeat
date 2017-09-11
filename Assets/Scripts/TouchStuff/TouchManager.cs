using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Android.Touchmanager
{
    public delegate void OnTouchLost(int id);

    public class TouchManager : MonoBehaviour
    {
        public OnTouchLost onTouchLost;
        List<int> m_activeTouches;
        int m_lastCount = 0;

        void Start()
        {
            m_activeTouches = new List<int>();
        }

        void Update()
        {
            if (m_lastCount != Input.touchCount)
                UpdateTouches();
        }

        void UpdateTouches()
        {
            // Setups 
            List<int> newTouchIDs = new List<int>();
            List<int> oldTouchIDs = m_activeTouches;
            Touch[] touches = Input.touches;
            for (int i = 0; i < touches.Length; i++)
            {
                newTouchIDs.Add(touches[i].fingerId);
            }

            // If we lost touches
            if (Input.touchCount < m_lastCount)
            {
                // Remove all new id's from old id's
                for (int i = 0; i < newTouchIDs.Count; i++)
                {
                    if (oldTouchIDs.Contains(newTouchIDs[i]))
                        oldTouchIDs.Remove(newTouchIDs[i]);
                }

                // Whatever is left in old id's arethe ones
                // that we have lost
                for (int i = 0; i < oldTouchIDs.Count; i++)
                {
                    if (onTouchLost != null)
                        onTouchLost(oldTouchIDs[i]);
                    m_activeTouches.Remove(oldTouchIDs[i]);
                }
            }

            // If we got new touches
            if (Input.touchCount > m_lastCount)
            {
                // remove all old id's from the new id's
                for (int i = 0; i < oldTouchIDs.Count; i++)
                {
                    if (newTouchIDs.Contains(oldTouchIDs[i]))
                        newTouchIDs.Remove(oldTouchIDs[i]);
                }

                // Leftovers have joined the touch family!
                for (int i = 0; i < newTouchIDs.Count; i++)
                {
                    OnNewTouch(newTouchIDs[i]);
                    m_activeTouches.Add(newTouchIDs[i]);
                }
            }

            m_lastCount = Input.touchCount;
        }

        void OnNewTouch(int ID)
        {
            Touch touch = Input.GetTouch(ID);
            Vector2 worldpos = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(worldpos, Vector2.zero);

            if (hit && hit.collider)
            {
                GameObject hitObject = hit.collider.gameObject;
                TouchListener reciever = hitObject.GetComponent<TouchListener>();
                if (reciever)
                {
                    reciever.RecieveTouchID(ID);
                }
            }
        }
    }
}
