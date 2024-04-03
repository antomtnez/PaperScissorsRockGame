using UnityEngine;

public class Match : MonoBehaviour
{
    public enum Choice { Rock, Paper, Scissors };

    private AIBehaviour m_AIBehaviour;
    private Choice m_AIChoice;
    private Choice m_PlayerChoice;
    [SerializeField] MatchCountdown m_MatchCountdown;
    private Player m_Player;
    private CPU m_CPU;

    void Start(){
        m_AIBehaviour = new AIBehaviour();
        m_Player = FindObjectOfType<Player>();
        m_CPU = FindObjectOfType<CPU>();
    }

    public void SetPlayerChoice(int choice){
        m_PlayerChoice = (Choice)choice;
    }

    void SetAIChoice(){
        m_AIChoice = m_AIBehaviour.GetAIChoice();
    }

    //Check the moves chosen by player and ai and return the match result.
    //0 -> player win, 1 -> ties, 2 -> ai win
    public void StartMatch(){
        m_MatchCountdown.Init();
        m_MatchCountdown.onCountdownFinished += ShowMeTheResult;
        SetAIChoice();
    }

    int MatchResult(){
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

    void ShowMeTheResult(){
        int result = MatchResult();

        if(result == 0) m_CPU.TakeDamage(1);
        if(result == 2) m_Player.TakeDamage(1);

        m_AIBehaviour.AddMatchEntry(new RockPaperScissorsAI.MatchHistoryEnter(){
            playerChoice = m_PlayerChoice,
            iAChoice = m_AIChoice,
            result = result
        });

        Debug.Log($"You chose {m_PlayerChoice} and CPU chose {m_AIChoice}. The result is {result}");
        m_MatchCountdown.onCountdownFinished -= ShowMeTheResult;
    }
}
