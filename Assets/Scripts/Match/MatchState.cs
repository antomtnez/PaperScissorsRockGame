public abstract class MatchState{
    protected Match m_Match;
    protected MatchState m_NextState;
    public MatchState(Match match){
        m_Match = match;
    }
    public abstract void EnterState();
    public virtual void ChangeState(){
        m_Match.ChangeState(m_NextState);
    }
}

public class StartTurn : MatchState{
    public StartTurn(Match match) : base(match){}

    public override void EnterState(){
        m_Match.TurnCountdown.Init();
        m_Match.TurnCountdown.onCountdownFinished += ChangeState;
    }

    public override void ChangeState(){
        m_Match.TurnCountdown.onCountdownFinished -= ChangeState;
        m_NextState = new EndTurn(m_Match);
        base.ChangeState();
    }
}

public class EndTurn : MatchState{
    private IHandConstestant m_Player;
    private IHandConstestant m_CPU;
    private Choice m_PlayerChoice;
    private Choice m_CPUChoice;

    public EndTurn(Match match) : base(match){
        m_Player = match.Player;
        m_CPU = match.CPU;
    }
    
    public override void EnterState(){
        SetChoices();
        WhoWinsThisTurn();
        ChangeState();
    }

    public void SetChoices(){
        m_PlayerChoice = m_Player.GetChoice();
        m_CPUChoice = m_CPU.GetChoice();
    }

    void WhoWinsThisTurn(){
        int result = TurnResult();

        if(result == 0) m_CPU.TakeDamage(1);
        if(result == 2) m_Player.TakeDamage(1);

        SaveResultOnDatabase(result);
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

    public void SaveResultOnDatabase(int result){
        GameManager.Instance.UpdateTurnHistory(new TurnHistoryEnter(){
            playerChoice = m_PlayerChoice,
            iAChoice = m_CPUChoice,
            result = result
        });
    }

    public override void ChangeState(){
        if(IsMatchFinished()){
            if(m_Player.IsDead()){
                m_NextState = new LoseMatch(m_Match);
            }else{
                m_NextState = new WinMatch(m_Match);
            }
        }else{
            m_NextState = new StartTurn(m_Match);
        }

        base.ChangeState();
    }

    bool IsMatchFinished(){
        return m_Player.IsDead() || m_CPU.IsDead();
    }
}

public class ShowResult : MatchState
{
    public ShowResult(Match match) : base(match){}

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }
}

public class WinMatch : MatchState
{
    public WinMatch(Match match) : base(match){}

    public override void EnterState(){}
}

public class LoseMatch : MatchState
{
    public LoseMatch(Match match) : base(match){}

    public override void EnterState(){}
}
