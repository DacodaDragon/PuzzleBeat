using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchListener))]
public class DraggableObject : MonoBehaviour {

    TouchListener m_touchListener;
    bool m_currentlySelected;
    Vector2 MoveOffset;

    [SerializeField] Sprite m_active;
    [SerializeField] Sprite m_inactive;

	void Start ()
    {
        m_currentlySelected = false;

        m_touchListener = GetComponent<TouchListener>();
        m_touchListener.onTouchPress += OnStartDrag;
        m_touchListener.onTouchPress += () => { GetComponent<SpriteRenderer>().sprite = m_active; };
        m_touchListener.onTouchRelease += OnEndDrag;
        m_touchListener.onTouchRelease += () => { GetComponent<SpriteRenderer>().sprite = m_inactive; };

    }

    void LateUpdate ()
    {
        if (m_currentlySelected)
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(m_touchListener.GetTouch().position) - MoveOffset;
	}

    public void OnStartDrag()
    {
        m_currentlySelected = true;
        MoveOffset = Camera.main.ScreenToWorldPoint(m_touchListener.GetTouch().position) - transform.position;
    }

    public void OnEndDrag()
    {
        m_currentlySelected = false;
    }
}
