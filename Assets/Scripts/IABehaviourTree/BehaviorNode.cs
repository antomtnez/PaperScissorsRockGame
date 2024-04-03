public abstract class BehaviorNode {
    public abstract bool Execute();
}

public class EvaluateProbabilitiesNode : BehaviorNode{
    private RockPaperScissorsAI ai;

    public EvaluateProbabilitiesNode(RockPaperScissorsAI ai){
        this.ai = ai;
    }

    public override bool Execute(){
        return ai.EvaluateProbabilitiesAndChooseMove();
    }
}

public class RandomChoiceNode : BehaviorNode{
    private RockPaperScissorsAI ai;

    public RandomChoiceNode(RockPaperScissorsAI ai){
        this.ai = ai;
    }

    public override bool Execute(){
        ai.MakeRandomChoice();
        // Always return true because this is a final action
        return true; 
    }
}