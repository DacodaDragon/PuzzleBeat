using Android.Touchmanager;
using UnityEngine;

public delegate void OnTouchPress();
public delegate void OnTouchRelease();
public delegate void OnTouchTap();
public delegate void OnTouchMultiTap(int amount);

public class TouchListener : MonoBehaviour
{
    private int m_TouchID;
    private bool m_Touched = false;
    public bool Selected { get { return m_Touched; } }
    private TouchManager manager;

    public OnTouchPress onTouchPress;
    public OnTouchRelease onTouchRelease;
    public OnTouchTap onTouchTap;
    public OnTouchMultiTap onTouchMultiTap;

    void Start()
    {
        manager = FindObjectOfType<TouchManager>();
    }

    public Touch GetTouch()
    {
        if (m_Touched)
            return manager.GetTouch(m_TouchID);
        else return new Touch();
    }

    public Vector3 GetTouchPosition()
    {
        return Camera.main.ScreenToWorldPoint(GetTouch().position);
    }

    public void RecieveTouchID(int ID)
    {
        if (m_Touched)
            return;
        m_Touched = true;
        m_TouchID = ID;
        manager.onTouchLost += OnTouchLost;

        if (onTouchPress != null)
            onTouchPress();
        if (onTouchTap != null)
            onTouchTap();
        if (onTouchMultiTap != null)
            onTouchMultiTap(Input.GetTouch(ID).tapCount);
    }

    private void OnTouchLost(int ID)
    {
        if (ID == m_TouchID)
        {
            m_Touched = false;
            manager.onTouchLost -= OnTouchLost;

            if (onTouchRelease != null)
                onTouchRelease();
        }
    }
}
