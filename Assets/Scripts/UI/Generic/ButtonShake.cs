using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonShake : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public void OnPointerEnter(PointerEventData eventData){
        transform.DOScale(Vector3.one * 1.1f, 0.1f);
        transform.DOShakePosition(0.1f, Vector3.one * 50f);
    }

    public void OnPointerExit(PointerEventData eventData){
        transform.DOScale(Vector3.one, 0.1f);
    }

}
