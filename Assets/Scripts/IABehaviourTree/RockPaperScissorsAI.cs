using System.Collections.Generic;
using UnityEngine;

public class RockPaperScissorsAI : MonoBehaviour{ 
    public class MatchHistoryEnter{
        public GameManager.Choice playerChoice;
        public GameManager.Choice iAChoice;
        public int result;
    }

    private List<MatchHistoryEnter> m_MatchHistory = new List<MatchHistoryEnter>();
    private float rockProb = 0, scissorsProb = 0, paperProb = 0;
    private GameManager.Choice m_ChosenMove;
    public GameManager.Choice ChosenMove => m_ChosenMove;

    public void UpdateOpponentMove(MatchHistoryEnter matchHistoryEnter){
        m_MatchHistory.Add(matchHistoryEnter);
        CalculateProbabilities();
    }

    void CalculateProbabilities(){
        foreach(MatchHistoryEnter matchHistoryEnter in m_MatchHistory){
            if(matchHistoryEnter.playerChoice == GameManager.Choice.Rock) rockProb++;
            if(matchHistoryEnter.playerChoice == GameManager.Choice.Scissors) scissorsProb++;
            if(matchHistoryEnter.playerChoice == GameManager.Choice.Paper) paperProb++;
        }

        TakeAccountPlaysRecurrenceOnProbabilities();

        rockProb /= m_MatchHistory.Count;
        scissorsProb /=  m_MatchHistory.Count;
        paperProb /=  m_MatchHistory.Count;

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
        if(m_MatchHistory[m_MatchHistory.Count].playerChoice == GameManager.Choice.Rock){
            if(m_MatchHistory[m_MatchHistory.Count].playerChoice == GameManager.Choice.Scissors){
                scissorsProb += .5f;
                paperProb += 1f;
            }else{
                paperProb += .5f;
                scissorsProb += 1f;
            }
        }
        if(m_MatchHistory[m_MatchHistory.Count].playerChoice == GameManager.Choice.Scissors){
            if(m_MatchHistory[m_MatchHistory.Count].playerChoice == GameManager.Choice.Rock){
                rockProb += .5f;
                paperProb += 1f;
            }else{
                paperProb += .5f;
                rockProb += 1f;
            }
        }
        if(m_MatchHistory[m_MatchHistory.Count].playerChoice == GameManager.Choice.Paper){
            if(m_MatchHistory[m_MatchHistory.Count].playerChoice == GameManager.Choice.Rock){
                rockProb += .5f;
                scissorsProb += 1f;
            }else{
                scissorsProb += .5f;
                rockProb += 1f;
            }
        }
    }

    // This is the logic to chose a move
    // Return true if chosen move is based in probabilities
    // Return false if chosen move is a random move
    public bool EvaluateProbabilitiesAndChooseMove(){
        if(GetNextChoice() > 0.5f)
            return true;
            
        return false;
    }

    float GetNextChoice(){
        m_ChosenMove = GameManager.Choice.Rock;
        float max = rockProb;
        if(scissorsProb > max){
            m_ChosenMove = GameManager.Choice.Scissors;
            max = scissorsProb;
        }
        if(paperProb > max) {
            m_ChosenMove = GameManager.Choice.Paper;
            max = paperProb;
        }

        return max;
    }

    public void MakeRandomChoice(){
        m_ChosenMove = (GameManager.Choice)Random.Range(0, 3);
    }
}