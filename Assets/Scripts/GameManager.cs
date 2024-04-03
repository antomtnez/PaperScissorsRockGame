using UnityEngine;

public class GameManager : MonoBehaviour{
    public enum Choice { Rock, Paper, Scissors };
    private AIBehaviour m_AIBehaviour;
    private Choice m_AIChoice;
    private Choice m_PlayerChoice;

    void Start(){
        m_AIBehaviour = new AIBehaviour();
    }

    public void SetPlayerChoice(int choice){
        m_PlayerChoice = (Choice)choice;
        ShowMeTheResult();
    }

    void SetAIChoice(){
        m_AIChoice = m_AIBehaviour.GetAIChoice();
    }

    //Check the moves chosen by player and ai and return the match result.
    //0 -> player win, 1 -> ties, 2 -> ai win
    int Match(){
        SetAIChoice();

        if(m_PlayerChoice == m_AIChoice) return 1;
        if(m_PlayerChoice == Choice.Rock){
            if(m_AIChoice == Choice.Paper)
                return 2;
        }
        if(m_PlayerChoice == Choice.Paper){
            if(m_AIChoice == Choice.Scissors)
                return 2;
        }
        if(m_PlayerChoice == Choice.Scissors){
            if(m_AIChoice == Choice.Rock)
                return 2;
        }
        return 0;
    }

    public void ShowMeTheResult(){
        int result = Match();
        m_AIBehaviour.AddMatchEntry(new RockPaperScissorsAI.MatchHistoryEnter(){
            playerChoice = m_PlayerChoice,
            iAChoice = m_AIChoice,
            result = result
        });

        Debug.Log($"You chose {m_PlayerChoice} and CPU chose {m_AIChoice}. The result is {result}");
    }
}