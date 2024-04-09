using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnCountdownView : MonoBehaviour{
    [SerializeField] GameObject CountdownPanel;
    [SerializeField] Image NumberImage;
    [SerializeField] List<Sprite> NumberImageList = new List<Sprite>();
    private Vector3 m_StartPanelPosition;
    private Vector3 m_FinishPanelPosition;

    void Start(){
        m_StartPanelPosition = CountdownPanel.transform.localPosition;
        m_FinishPanelPosition = CountdownPanel.transform.localPosition + new Vector3(200f, -300f, 0f);
    }

    public void SetCountdown(int count){
        NumberImage.sprite = NumberImageList[count - 1];
        PanelShake();
    }

    public void HideCountdownPanel(){
        HidePanel();
    }

    public void ShowCountdownPanel(){
        ShowPanel();
    }

    public void PanelShake(){
        CountdownPanel.transform.DOShakePosition(0.3f, new Vector2(20f, 20f));
    }

    public void ShowPanel(){
        CountdownPanel.transform.DOKill();
        CountdownPanel.SetActive(true);
        CountdownPanel.transform.DOScale(Vector3.one, 0.2f);
        CountdownPanel.transform.DOLocalMove(m_StartPanelPosition, 0.2f).OnComplete(()=>{
            PanelShake();
        });
    }

    public void HidePanel(){
        CountdownPanel.transform.DOLocalMove(m_FinishPanelPosition, 0.2f);
        CountdownPanel.transform.DOScale(Vector3.zero, 0.2f).OnComplete(()=>{
            CountdownPanel.SetActive(false);
        });
    }
}
