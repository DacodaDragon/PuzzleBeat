using UnityEngine;

[RequireComponent(typeof(TouchListener))]
public class PuzzleLever : PuzzleElement {

    private TouchListener m_touchListener;
    private bool m_currentlySelected;
    private Vector2 m_positionOffset;

    [SerializeField] private Sprite m_active;
    [SerializeField] private Sprite m_inactive;

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
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(m_touchListener.GetTouch().position) - m_positionOffset;
	}

    public void OnStartDrag()
    {
        m_currentlySelected = true;
        m_positionOffset = Camera.main.ScreenToWorldPoint(m_touchListener.GetTouch().position) - transform.position;
    }

    public void OnEndDrag()
    {
        m_currentlySelected = false;
    }
}
