using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchListener))]
public class SpinnableObject : MonoBehaviour
{
    TouchListener m_TouchListener;

    private void Start()
    {
        m_TouchListener = GetComponent<TouchListener>();
        m_TouchListener.onTouchPress += OnTouch;
    }

    private void OnTouch()
    {

    }

    void Update()
    {
        if (m_TouchListener.Selected)
        {

            transform.rotation = Quaternion.Euler(0, 0, GetAngleToTouch());
        }
    }

    public float GetAngleToTouch()
    {
        Vector2 AngleVector = m_TouchListener.GetTouchPosition() - transform.position;
        float angle = Mathf.Atan2(AngleVector.y, AngleVector.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;
        return angle;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.localPosition, 1);
    }

}
