using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public interface IHandConstestant{
    public void TakeDamage(int damageTaken);
    public bool IsDead();
    public void StartShaking();
    public Choice GetChoice();
    public void ShowYourChoice();
    public void CloseHand();
}

public class Match : MonoBehaviour{
    public TurnCountdown TurnCountdown;
    public float TimeBtwTurns;
    private float m_CurrentTimeBtwTurns;
    [HideInInspector] public IHandConstestant Player;
    [HideInInspector] public IHandConstestant CPU;

    private MatchState m_MatchState;
    public event Action onWaitToNextTurnIsFinished;

    void Start(){
        Player = FindObjectOfType<Player>().GetComponent<IHandConstestant>();
        CPU = FindObjectOfType<CPU>().GetComponent<IHandConstestant>();

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
}
