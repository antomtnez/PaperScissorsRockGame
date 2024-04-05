using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour{
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] Slider HealthSlider;
    [SerializeField] Slider DamagedSlider;
    private float m_DamagedSliderTimer;

    protected void SetNameText(string name){
        NameText.SetText(name);
    }

    public void SetMaxHealth(int health){
        HealthSlider.maxValue  = health;
        HealthSlider.value  = HealthSlider.maxValue;
        DamagedSlider.maxValue = health;
        DamagedSlider.value = DamagedSlider.maxValue;
    }

    public void SetHealth(int health){
        HealthSlider.value  = health;
        StartCoroutine(DamagedSliderDecreaseDelayed());
    }
    /*
    void Update(){
        m_DamagedSliderTimer -= Time.deltaTime;
        if(m_DamagedSliderTimer <= 0){
            if(HealthSlider.value < DamagedSlider.value){
                float decreaseSpeed = 1f;
                DamagedSlider.value -= decreaseSpeed * Time.deltaTime;
            }
        }
    }
    */

    IEnumerator DamagedSliderDecreaseDelayed(){
        yield return new WaitForSeconds(.5f);
        while(DamagedSlider.value > HealthSlider.value){
            yield return new WaitForSeconds(0.01f);
            DamagedSlider.value -= 0.02f;
        }
    }
}