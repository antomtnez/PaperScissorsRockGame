using UnityEngine;

public class Hand : MonoBehaviour{
    protected int m_Health;
    protected int m_MaxHealth = 10;
    public int Health => m_Health;
    public int MaxHealth => m_MaxHealth;
    protected HealthBarPresenter m_HealthBarPresenter;

    public void SetMaxHealth(int maxHealth){
        m_MaxHealth = maxHealth;
        m_Health = m_MaxHealth;
    }

    public void TakeDamage(int damage){
        int damageTaken = Mathf.Min(m_Health, damage);
        m_Health -= damageTaken;
        m_HealthBarPresenter.SetHealth();
    }

    public bool IsDead(){      
        return m_Health <= 0;
    }
}
