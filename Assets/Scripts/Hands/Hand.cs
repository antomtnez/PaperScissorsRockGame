using UnityEngine;

public enum Choice { Rock, Paper, Scissors };

[RequireComponent(typeof(Animator))]
public abstract class Hand : MonoBehaviour, IHandConstestant{
    protected int m_Health;
    protected int m_MaxHealth = 10;
    protected Choice m_Choice;
    public int Health => m_Health;
    public int MaxHealth => m_MaxHealth;

    protected HealthBarPresenter m_HealthBarPresenter;
    public HandAnimatorController HandAnimatorController;

    void Awake(){
        HandAnimatorController = new HandAnimatorController(GetComponent<Animator>());
    }

    public void SetMaxHealth(int maxHealth){
        m_MaxHealth = maxHealth;
        m_Health = m_MaxHealth;
    }

    public abstract void SetChoice(int choice);
    public abstract Choice GetChoice();

    public void TakeDamage(int damage){
        int damageTaken = Mathf.Min(m_Health, damage);
        m_Health -= damageTaken;
        m_HealthBarPresenter.SetHealth();
    }

    public bool IsDead(){      
        return m_Health <= 0;
    }
}
