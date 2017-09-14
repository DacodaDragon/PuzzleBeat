using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchListener))]
public class SpinnableObject : MonoBehaviour
{
    TouchListener m_TouchListener;
    SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    Sprite m_SpriteActive;
    [SerializeField]
    Sprite m_SpriteInactive;

    float m_angleOffset;

    private void Start()
    {
        m_TouchListener                 = GetComponent<TouchListener>();
        m_SpriteRenderer                = GetComponent<SpriteRenderer>();
        m_TouchListener.onTouchPress   += () => { m_SpriteRenderer.sprite = m_SpriteActive; };
        m_TouchListener.onTouchRelease += () => { m_SpriteRenderer.sprite = m_SpriteInactive; };
    }

    void Update()
    {
        if (m_TouchListener.Selected)
            transform.rotation = Quaternion.Euler(0, 0, GetAngleToTouch() + m_angleOffset);
    }

    public float GetAngleToTouch()
    {
        Vector2 AngleVector = m_TouchListener.GetTouchPosition() - transform.position;
        float angle = Mathf.Atan2(AngleVector.y, AngleVector.x) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;
        return angle;
    }
}
