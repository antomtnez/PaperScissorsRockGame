using UnityEngine;

public interface IHandConstestant{
    public Choice GetChoice();
    public void TakeDamage(int damageTaken);
    public bool IsDead();
}

public class Match : MonoBehaviour{
    public TurnCountdown TurnCountdown;
    [HideInInspector] public IHandConstestant Player;
    [HideInInspector] public IHandConstestant CPU;

    private MatchState m_MatchState;

    void Start(){
        Player = FindObjectOfType<Player>().GetComponent<IHandConstestant>();
        CPU = FindObjectOfType<CPU>().GetComponent<IHandConstestant>();

        ChangeState(new StartTurn(this));
    }

    public void ChangeState(MatchState matchState){
        m_MatchState = matchState;
        m_MatchState.EnterState();
    }
}
