public class Player : Hand{
    void Start(){
        SetMaxHealth(MaxHealth);
        m_HealthBarPresenter = new HealthBarPresenter(this, FindObjectOfType<PlayerHealthView>());
    }
}
