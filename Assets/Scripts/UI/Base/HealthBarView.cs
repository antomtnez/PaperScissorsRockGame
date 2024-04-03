using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour{
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] Slider HealthSlider;

    protected void SetNameText(string name){
        NameText.SetText(name);
    }

    public void SetMaxHealth(int health){
        HealthSlider.maxValue  = health;
        HealthSlider.value  = health;
    }

    public void SetHealth(int health){
        HealthSlider.value  = health;
    }
}