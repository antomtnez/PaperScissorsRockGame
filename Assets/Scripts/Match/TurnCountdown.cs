using System;
using System.Collections;
using UnityEngine;

public class TurnCountdown : MonoBehaviour{
    public int timeLeft = 3;
    public event Action onCountdownFinished;
    private TurnCountdownPresenter m_CountdownPresenter;

    public void Init(){
        m_CountdownPresenter = new TurnCountdownPresenter(this, FindObjectOfType<TurnCountdownView>());
        timeLeft = 3;
        StartCoroutine(Countdown());
        m_CountdownPresenter.StartCountdown();
    }

    IEnumerator Countdown(){
        while(timeLeft > 0){
            m_CountdownPresenter.SetCountdownText(timeLeft);
            yield return new WaitForSeconds(1.5f);
            timeLeft--;
        }

        onCountdownFinished?.Invoke();
    }
}
