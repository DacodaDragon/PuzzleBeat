using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TouchListener))]
public class TappableObject : MonoBehaviour {

    [SerializeField] Sprite m_spriteDefault;
    [SerializeField] Sprite m_spritePressed;
    [SerializeField] Sprite m_spriteActive;

	void Start () {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        TouchListener listener = GetComponent<TouchListener>();
        listener.onTouchPress += () => { renderer.sprite = m_spritePressed; };
        listener.onTouchRelease += () => { renderer.sprite = m_spriteActive; };
    }

    private void OnTap()
    {
        Destroy(gameObject);
    }
}
