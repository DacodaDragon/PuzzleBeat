using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchListener))]
public class SpinnableObject : MonoBehaviour
{
    TouchListener m_TouchListener;
    SpriteRenderer m_SpriteRenderer;

    Sprite m_SpriteActive;
    Sprite m_SpriteInActive;

    float m_angleOffset;

    private void Start()
    {
        m_TouchListener                 = GetComponent<TouchListener>();
        m_TouchListener.onTouchPress   += () => { m_SpriteRenderer.sprite = m_SpriteInActive; };
        m_TouchListener.onTouchRelease += () => { m_SpriteRenderer.sprite = m_SpriteInActive; };
    }

    void Update()
    {
        if (m_TouchListener.Selected)
        {
            transform.rotation = Quaternion.Euler(0, 0, GetAngleToTouch() + m_angleOffset);
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
