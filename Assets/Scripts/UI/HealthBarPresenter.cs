public class HealthBarPresenter{
    private Hand m_Hand;
    private HealthBarView m_HealthBarView;

    public HealthBarPresenter(Hand hand, HealthBarView healthBarView){
        m_Hand = hand;
        m_HealthBarView = healthBarView;
        m_HealthBarView.SetMaxHealth(m_Hand.MaxHealth);
    }

    public void SetHealth(){
        m_HealthBarView.SetHealth(m_Hand.Health);
    }
}
