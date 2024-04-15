using System;
using System.Collections;
using UnityEngine;

public interface IHandConstestant{
    public void TakeDamage(int damageTaken);
    public bool IsDead();
    public void StartShaking();
    public Choice GetChoice();
    public void ShowYourChoice();
    public void CloseHand();
    public void WinnerCelebration();
}

public class Match : MonoBehaviour{
    public TurnCountdown TurnCountdown;
    public float TimeBtwTurns;
    private float m_CurrentTimeBtwTurns;
    [HideInInspector] public IHandConstestant Player;
    [HideInInspector] public IHandConstestant CPU;
    private EndMatchAnimation m_EndMatchAnimation;

    private MatchState m_MatchState;
    public event Action onWaitToNextTurnIsFinished;

    void Start(){
        Player = FindObjectOfType<Player>().GetComponent<IHandConstestant>();
        CPU = FindObjectOfType<CPU>().GetComponent<IHandConstestant>();
        m_EndMatchAnimation = FindObjectOfType<EndMatchAnimation>();

        ChangeState(new StartTurn(this));
    }

    public void ChangeState(MatchState matchState){
        m_MatchState = matchState;
        m_MatchState.EnterState();
    }

    public void WaitToNextTurn(){
        m_CurrentTimeBtwTurns = TimeBtwTurns;
        StartCoroutine(WaitingToNextTurn());
    }

    IEnumerator WaitingToNextTurn(){
        while(m_CurrentTimeBtwTurns > 0){
            yield return new WaitForSeconds(0.1f);
            m_CurrentTimeBtwTurns -= 0.1f;
        }

        onWaitToNextTurnIsFinished?.Invoke();
    }

    public void LoserAnimation(){
        m_EndMatchAnimation.LoseAnimation();
    }

    public void WinnerAnimation(){
        m_EndMatchAnimation.WinnerAnimation();
    }
}
