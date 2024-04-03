using System;
using System.Collections;
using UnityEngine;

public class MatchCountdown : MonoBehaviour{
    public int timeLeft = 3;
    public event Action onCountdownFinished;
    private MatchCountdownPresenter m_CountdownPresenter;

    void Start(){
        m_CountdownPresenter = new MatchCountdownPresenter(this, GameObject.FindObjectOfType<MatchCountdownView>());
    }

    public void Init(){
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
