using UnityEngine;

public class GameManager : MonoBehaviour{
    public enum Choice { Rock, Paper, Scissors };
    public AIBehaviour aIBehaviour;
    public Choice playerChoice;

    public void SetPlayerChoice(Choice choice){
        playerChoice = choice;
    }
    
    void Match(){

    }
}