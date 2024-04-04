public class Player : Hand{
    void Start(){
        SetMaxHealth(MaxHealth);
        m_HealthBarPresenter = new HealthBarPresenter(this, FindObjectOfType<PlayerHealthView>());
    }

    public override void TakeDamage(int damage){
        CameraShaker.Invoke();
        base.TakeDamage(damage);
    }

    public override Choice GetChoice(){
        return m_Choice;
    }

    public override void SetChoice(int choice){
        m_Choice = (Choice)choice;
    }
}
