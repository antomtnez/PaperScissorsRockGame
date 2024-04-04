using UnityEngine;

public interface IHandConstestant{
    public Choice GetChoice();
    public void TakeDamage(int damageTaken);
    public bool IsDead();
}

public class Match : MonoBehaviour{
    [SerializeField] MatchCountdown m_MatchCountdown;
    private IHandConstestant m_Player;
    private IHandConstestant m_CPU;
    private Choice m_PlayerChoice;
    private Choice m_CPUChoice;

    void Start(){
        m_Player = FindObjectOfType<Player>().GetComponent<IHandConstestant>();
        m_CPU = FindObjectOfType<CPU>().GetComponent<IHandConstestant>();
    }

    public void StartTurn(){
        Debug.Log("Start new turn");
        m_MatchCountdown.Init();
        m_MatchCountdown.onCountdownFinished += EndTurn;
    }

    void EndTurn(){
        m_PlayerChoice = m_Player.GetChoice();
        m_CPUChoice = m_CPU.GetChoice();

        int result = TurnResult();

        if(result == 0) m_CPU.TakeDamage(1);
        if(result == 2) m_Player.TakeDamage(1);

        Debug.Log($"You chose {m_PlayerChoice} and CPU chose {m_CPUChoice}. The result is {result}");
        m_MatchCountdown.onCountdownFinished -= EndTurn;

        GameManager.Instance.UpdateTurnHistory(new TurnHistoryEnter(){
            playerChoice = m_PlayerChoice,
            iAChoice = m_CPUChoice,
            result = result
        });

        if(IsMatchFinished()){
            if(m_Player.IsDead()){
                Debug.Log("YOU LOSE!");
            }else{
                Debug.Log("YOU WIN!");
            }
        }else{
            StartTurn();
        }
    }

    //Check the moves chosen by player and ai and return the match result.
    //0 -> player win, 1 -> ties, 2 -> ai win
    int TurnResult(){
        if(m_PlayerChoice == m_CPUChoice) return 1;
        if(m_PlayerChoice == Choice.Rock){
            if(m_CPUChoice == Choice.Paper)
                return 2;
        }
        if(m_PlayerChoice == Choice.Paper){
            if(m_CPUChoice == Choice.Scissors)
                return 2;
        }
        if(m_PlayerChoice == Choice.Scissors){
            if(m_CPUChoice == Choice.Rock)
                return 2;
        }
        return 0;
    }

    bool IsMatchFinished(){
        return m_Player.IsDead() || m_CPU.IsDead();
    }
}
