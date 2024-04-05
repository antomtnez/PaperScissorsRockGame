public abstract class MatchState{
    protected Match m_Match;
    protected IHandConstestant m_Player;
    protected IHandConstestant m_CPU;
    protected MatchState m_NextState;
    public MatchState(Match match){
        m_Match = match;
        m_Player = match.Player;
        m_CPU = match.CPU;
    }
    public abstract void EnterState();
    public virtual void ChangeState(){
        m_Match.ChangeState(m_NextState);
    }
}

public class StartTurn : MatchState{
    public StartTurn(Match match) : base(match){}

    public override void EnterState(){
        HandContestantsStartToShake();
        m_Match.TurnCountdown.Init();
        m_Match.TurnCountdown.onCountdownFinished += ChangeState;
    }

    void HandContestantsStartToShake(){
        m_Player.StartShaking();
        m_CPU.StartShaking();
    }

    public override void ChangeState(){
        m_Match.TurnCountdown.onCountdownFinished -= ChangeState;
        m_NextState = new EndTurn(m_Match);
        base.ChangeState();
    }
}

public class EndTurn : MatchState{
    private Choice m_PlayerChoice;
    private Choice m_CPUChoice;

    public EndTurn(Match match) : base(match){}
    
    public override void EnterState(){
        SetChoices();
        HandContestantsShowTheirChoices();
        WhoWinsThisTurn();

        m_Match.WaitToNextTurn();
        m_Match.onWaitToNextTurnIsFinished += ChangeState;
    }

    public void SetChoices(){
        m_PlayerChoice = m_Player.GetChoice();
        m_CPUChoice = m_CPU.GetChoice();
    }

    void HandContestantsShowTheirChoices(){
        m_Player.ShowYourChoice();
        m_CPU.ShowYourChoice();
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
            m_Player.CloseHand();
            m_CPU.CloseHand();
            m_NextState = new StartTurn(m_Match);
        }

        m_Match.onWaitToNextTurnIsFinished -= ChangeState;
        base.ChangeState();
    }

    bool IsMatchFinished(){
        return m_Player.IsDead() || m_CPU.IsDead();
    }
}

public class WinMatch : MatchState
{
    public WinMatch(Match match) : base(match){}

    public override void EnterState(){
        m_Player.WinnerCelebration();
    }
}

public class LoseMatch : MatchState
{
    public LoseMatch(Match match) : base(match){}

    public override void EnterState(){
        m_CPU.WinnerCelebration();
    }
}
