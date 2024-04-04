using System.Collections.Generic;
using UnityEngine;

public class TurnHistoryEnter{
    public Choice playerChoice;
    public Choice iAChoice;
    public int result;
}

public class GameManager : MonoBehaviour{
    public static GameManager Instance;
    private List<TurnHistoryEnter> m_TurnHistory = new List<TurnHistoryEnter>();
    public List<TurnHistoryEnter> TurnHistory => m_TurnHistory;

    void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this);
        }

    }

    public void UpdateTurnHistory(TurnHistoryEnter turnHistoryEnter){
        m_TurnHistory.Add(turnHistoryEnter);
    }
}