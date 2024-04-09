public class TurnCountdownPresenter{
    private TurnCountdown m_MatchCountdown;
    private TurnCountdownView m_View;

    public TurnCountdownPresenter(TurnCountdown matchCountdown, TurnCountdownView countdownView){
        m_MatchCountdown = matchCountdown;
        m_View = countdownView;
        m_MatchCountdown.onCountdownFinished += FinishCountdown;
    }

    public void StartCountdown(){
        m_View.ShowCountdownPanel();
    }

    public void SetCountdownText(int count){
        m_View.SetCountdown(count);
    }

    void FinishCountdown(){
        m_View.HideCountdownPanel();
    }
}
