public class AIBehaviour{
    private RockPaperScissorsAI m_AI;
    private EvaluateProbabilitiesNode m_EvaluateNode;
    private RandomChoiceNode m_RandomNode;

    public AIBehaviour(){
        m_AI = new RockPaperScissorsAI();
        m_EvaluateNode = new EvaluateProbabilitiesNode(m_AI);
        m_RandomNode = new RandomChoiceNode(m_AI);
    }

    GameManager.Choice GetAIChoice(){
        if(!m_EvaluateNode.Execute())
            m_RandomNode.Execute();
        
        return m_AI.ChosenMove;
    }

    public void AddMatchEntry(RockPaperScissorsAI.MatchHistoryEnter matchHistoryEnter){
        m_AI.UpdateOpponentMove(matchHistoryEnter);
    }
}
