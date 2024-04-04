public class CPU : Hand{
    private AIBehaviour m_AIBehaviour;

    void Start(){
        m_AIBehaviour = new AIBehaviour();
        
        SetMaxHealth(MaxHealth);
        m_HealthBarPresenter = new HealthBarPresenter(this, FindObjectOfType<CPUHealthView>());
    }

    public override Choice GetChoice(){
        return m_AIBehaviour.GetAIChoice();
    }

    public override void SetChoice(int choice){
        throw new System.NotImplementedException();
    }
}
