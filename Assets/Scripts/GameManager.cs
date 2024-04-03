using UnityEngine;

public class GameManager : MonoBehaviour{
    public static GameManager Instance;

    void Awake(){
        if(Instance != null){
            Instance = this;
            DontDestroyOnLoad(this);
        }else{
            Destroy(this);
        }
    }
}