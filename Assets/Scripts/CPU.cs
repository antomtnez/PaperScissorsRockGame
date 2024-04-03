public class CPU : Hand{
    void Start(){
        SetMaxHealth(MaxHealth);
        m_HealthBarPresenter = new HealthBarPresenter(this, FindObjectOfType<CPUHealthView>());
    }
}
