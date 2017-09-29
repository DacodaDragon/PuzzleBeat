using UnityEngine;

public delegate void OnPlayerDeathDelegate();

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int m_MaxLives;
    [SerializeField] private int m_CurrentLives;
    [SerializeField] private float m_CurrentAccuracy;

    private OnPlayerDeathDelegate onPlayerDeath;
    public event OnPlayerDeathDelegate OnPlayerDeath
    { add { onPlayerDeath += value; } remove { onPlayerDeath -= value; } }

    public int MaxLives
    {
        get { return m_MaxLives; }

        set
        {
            m_MaxLives = value;
            if (MaxLives > 0)
                return;
            if (onPlayerDeath != null)
                return;
            onPlayerDeath.Invoke();
        }
    }

    public int CurrentLives { get { return m_CurrentLives; } set { m_CurrentLives = value; } }
    public float CurrentAccuracy { get { return m_CurrentAccuracy; } set { m_CurrentAccuracy = value; } }
}
