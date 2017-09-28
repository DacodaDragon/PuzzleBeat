using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchListener))]
public class PuzzleSpinner : PuzzleElement
{
    TouchListener m_TouchListener;
    SpriteRenderer m_SpriteRenderer;

    [SerializeField]
    float m_AngleToRotate;
    float m_angleOffset;
    float m_rotationSpeed;
    float m_previousRotation;
    float m_angleDelta;

    bool m_rotating;

    private void Start()
    {
        m_TouchListener = GetComponent<TouchListener>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_TouchListener.onTouchPress += StartRotation;
        m_TouchListener.onTouchRelease += EndRotation;
    }

    void Update()
    {
        if (m_TouchListener.Selected)
            Rotate();
        else AfterRotate();
    }

    private  void StartRotation()
    {
        m_angleOffset = transform.rotation.z + Vector2DMath.GetAngleBetween(m_TouchListener.GetTouchPosition(), transform.position);
        Debug.Log("START ROTATING!!");
    }

    private void EndRotation()
    {
        m_rotationSpeed = m_angleDelta;
    }

    private void Rotate()
    {
        Debug.Log("ROTATING!");
        float Rotation = Vector2DMath.GetAngleBetween(m_TouchListener.GetTouchPosition(), transform.position)
            - m_angleOffset;
        m_angleDelta = m_previousRotation - Rotation;
        m_previousRotation = Rotation;
        transform.rotation = Quaternion.Euler(0, 0, Rotation);

        m_AngleToRotate -= m_angleDelta;
        if (m_AngleToRotate < 0)
            Solve();
    }

    private void AfterRotate()
    {
        m_rotationSpeed *= 0.9f;
        transform.Rotate(new Vector3(0, 0, 1), -m_rotationSpeed);
    }
}
