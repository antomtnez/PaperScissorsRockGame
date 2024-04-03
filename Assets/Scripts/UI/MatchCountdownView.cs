using TMPro;
using UnityEngine;

public class MatchCountdownView : MonoBehaviour{
    [SerializeField] TextMeshProUGUI CountdownText;

    public void SetCountdownText(string text){
        CountdownText.SetText(text);
    }

    public void HideCountdownText(){
        CountdownText.gameObject.SetActive(false);
    }

    public void ShowCountdownText(){
        CountdownText.gameObject.SetActive(true);
    }
}
