public class CPU : Hand{
    private AIBehaviour m_AIBehaviour;

    void Start(){
        m_AIBehaviour = new AIBehaviour();
        
        SetMaxHealth(MaxHealth);
        m_HealthBarPresenter = new HealthBarPresenter(this, FindObjectOfType<CPUHealthView>());
    }

    public override Choice GetChoice(){
        m_Choice = m_AIBehaviour.GetAIChoice();
        return m_Choice;
    }
}
