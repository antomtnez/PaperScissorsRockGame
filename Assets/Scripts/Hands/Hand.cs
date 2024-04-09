using UnityEngine;

public enum Choice { Rock, Paper, Scissors };

public abstract class Hand : MonoBehaviour, IHandConstestant{
    protected int m_Health;
    [SerializeField] protected int m_MaxHealth = 5;
    protected Choice m_Choice;
    public int Health => m_Health;
    public int MaxHealth => m_MaxHealth;

    protected HealthBarPresenter m_HealthBarPresenter;
    public HandAnimatorController HandAnimatorController;
    [SerializeField] DamageTakenFX DamageTakenFX;
    

    void Awake(){
        HandAnimatorController = GetComponent<HandAnimatorController>();
    }

    public void SetMaxHealth(int maxHealth){
        m_MaxHealth = maxHealth;
        m_Health = m_MaxHealth;
    }

    public virtual void SetChoice(int choice){}

    public virtual Choice GetChoice(){
        return m_Choice;
    }

    public virtual void TakeDamage(int damage){
        int damageTaken = Mathf.Min(m_Health, damage);
        m_Health -= damageTaken;
        m_HealthBarPresenter.SetHealth();

        DamageTakenFX.StartFX();
        if(IsDead()){
            HandAnimatorController.StartAnimation("HandDead");
        }else{
            HandAnimatorController.DamageAnimation();;
        }
    }

    public bool IsDead(){      
        return m_Health <= 0;
    }

    public void ShowYourChoice(){
        HandAnimatorController.ChoiceAnimation(m_Choice);
    }

    public void StartShaking(){
        HandAnimatorController.StartAnimation("HandShake");
    }

    public void CloseHand(){
        HandAnimatorController.CloseHandAnimation();
    }

    public void WinnerCelebration(){
        HandAnimatorController.WinnerAnimation();
    }
}
