using UnityEngine;

public class RockPaperScissorsAI{ 
    private float rockProb, scissorsProb, paperProb;
    private Choice m_ChosenMove;
    public Choice ChosenMove => m_ChosenMove;

    public RockPaperScissorsAI(){
        rockProb = 0; 
        scissorsProb = 0; 
        paperProb = 0;
    }

    // This is the logic to chose a move
    // Return true if chosen move is based in probabilities
    // Return false if chosen move is a random move
    public bool EvaluateProbabilitiesAndChooseMove(){
        if(GameManager.Instance.TurnHistory.Count > 0)
            CalculateProbabilities();

        if(GetNextChoice() > 0.5f)
            return true;
            
        return false;
    }

    //AI chose the element it allows to win the match
    float GetNextChoice(){
        m_ChosenMove = Choice.Paper;
        float max = rockProb;
        if(scissorsProb > max){
            m_ChosenMove = Choice.Rock;
            max = scissorsProb;
        }
        if(paperProb > max) {
            m_ChosenMove = Choice.Scissors;
            max = paperProb;
        }

        return max;
    }

    void CalculateProbabilities(){
        for(int i = GameManager.Instance.TurnHistory.Count - 1; i >= 0; i--){
            if(i <= GameManager.Instance.TurnHistory.Count - 21) break;
            if(GameManager.Instance.TurnHistory[i].playerChoice == Choice.Rock) rockProb++;
            if(GameManager.Instance.TurnHistory[i].playerChoice == Choice.Scissors) scissorsProb++;
            if(GameManager.Instance.TurnHistory[i].playerChoice == Choice.Paper) paperProb++;
        }

        if(GameManager.Instance.TurnHistory.Count >= 2)
            TakeAccountPlaysRecurrenceOnProbabilities();

        rockProb /= GameManager.Instance.TurnHistory.Count;
        scissorsProb /=  GameManager.Instance.TurnHistory.Count;
        paperProb /=  GameManager.Instance.TurnHistory.Count;

        //Normalize probabilities fitted to 1
        float totalProb = rockProb + scissorsProb + paperProb;
        rockProb /= totalProb;
        scissorsProb /= totalProb;
        paperProb /= totalProb;        
    }

    //We take into account the recurrence of plays.
    //To the immediately preceding move we don't add anything, 
    //to the previous move we add 0.5 and to the remaining one we add 1
    void TakeAccountPlaysRecurrenceOnProbabilities(){
        if(GameManager.Instance.TurnHistory[GameManager.Instance.TurnHistory.Count-1].playerChoice == Choice.Rock){
            if(GameManager.Instance.TurnHistory[GameManager.Instance.TurnHistory.Count-2].playerChoice == Choice.Scissors){
                scissorsProb += .5f;
                paperProb += 1f;
            }else{
                paperProb += .5f;
                scissorsProb += 1f;
            }
        }
        if(GameManager.Instance.TurnHistory[GameManager.Instance.TurnHistory.Count-1].playerChoice == Choice.Scissors){
            if(GameManager.Instance.TurnHistory[GameManager.Instance.TurnHistory.Count-2].playerChoice == Choice.Rock){
                rockProb += .5f;
                paperProb += 1f;
            }else{
                paperProb += .5f;
                rockProb += 1f;
            }
        }
        if(GameManager.Instance.TurnHistory[GameManager.Instance.TurnHistory.Count-1].playerChoice == Choice.Paper){
            if(GameManager.Instance.TurnHistory[GameManager.Instance.TurnHistory.Count-2].playerChoice == Choice.Rock){
                rockProb += .5f;
                scissorsProb += 1f;
            }else{
                scissorsProb += .5f;
                rockProb += 1f;
            }
        }
    }

    public void MakeRandomChoice(){
        m_ChosenMove = (Choice)Random.Range(0, 3);
    }
}
