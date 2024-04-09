using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour{
    [SerializeField] Slider HealthSlider;
    [SerializeField] Slider DamagedSlider;
    private float m_DamagedSliderTimer;

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

    IEnumerator DamagedSliderDecreaseDelayed(){
        yield return new WaitForSeconds(.5f);
        while(DamagedSlider.value > HealthSlider.value){
            yield return new WaitForSeconds(0.01f);
            DamagedSlider.value -= 0.02f;
        }
    }
}