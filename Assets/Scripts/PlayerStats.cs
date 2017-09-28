using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int m_MaxLives;
    [SerializeField] private int m_CurrentLives;
    [SerializeField] private float m_CurrentAccuracy;

    public int MaxLives { get { return m_MaxLives; } set { m_MaxLives = value; } }
    public int CurrentLives { get { return m_CurrentLives; } set { m_CurrentLives = value; } }
    public float CurrentAccuracy { get { return m_CurrentAccuracy; } set { m_CurrentAccuracy = value; } }
}
