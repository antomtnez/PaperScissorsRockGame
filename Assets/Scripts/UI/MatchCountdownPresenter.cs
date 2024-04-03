public class MatchCountdownPresenter{
    private MatchCountdown m_MatchCountdown;
    private MatchCountdownView m_View;

    public MatchCountdownPresenter(MatchCountdown matchCountdown, MatchCountdownView countdownView){
        m_MatchCountdown = matchCountdown;
        m_View = countdownView;
        m_MatchCountdown.onCountdownFinished += FinishCountdown;
    }

    public void StartCountdown(){
        m_View.ShowCountdownText();
    }

    public void SetCountdownText(int count){
        m_View.SetCountdownText($"{count}");
    }

    void FinishCountdown(){
        m_View.HideCountdownText();
    }
}
