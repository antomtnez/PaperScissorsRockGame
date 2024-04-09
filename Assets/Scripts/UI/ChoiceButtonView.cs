using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceButtonView : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image ButtonImage;
    [SerializeField] Image ButtonIcon;
    [SerializeField] Image ButtonText;
    private Color m_NoSelectedColor = Color.grey;
    private Color m_SelectedColor = Color.white;

    void Start(){
        DeselectedColor();
    }

    public void OnSelect(BaseEventData eventData){
        ButtonImage.transform.DOScale(Vector3.one * 1.1f, 0.1f);
        ButtonIcon.transform.DOScale(Vector3.one * 1.1f, 0.1f);
        SelectedColor();
    }

    public void OnDeselect(BaseEventData eventData){
        ButtonImage.transform.DOScale(Vector3.one, 0.1f);
        ButtonIcon.transform.DOScale(Vector3.one, 0.1f);
        DeselectedColor();
    }

    public void OnPointerEnter(PointerEventData eventData){
        ButtonImage.transform.DOScale(Vector3.one * 1.1f, 0.1f);
        ButtonIcon.transform.DOScale(Vector3.one * 1.1f, 0.1f);
        ButtonText.transform.DOShakePosition(0.1f, Vector3.one * 50f);
    }

    public void OnPointerExit(PointerEventData eventData){
        ButtonImage.transform.DOScale(Vector3.one, 0.1f);
        ButtonIcon.transform.DOScale(Vector3.one, 0.1f);
    }

    void SelectedColor(){
        ButtonImage.color = m_SelectedColor;
        ButtonIcon.color = m_SelectedColor;
        ButtonText.color = m_SelectedColor;
    }

    void DeselectedColor(){
        ButtonImage.color = m_NoSelectedColor;
        ButtonIcon.color = m_NoSelectedColor;
        ButtonText.color = m_NoSelectedColor;
    }
}
