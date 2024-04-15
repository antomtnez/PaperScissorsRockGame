using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EndMatchAnimation : MonoBehaviour{
    [SerializeField] GameObject BackgroundPanel;
    [SerializeField] GameObject WinnerImage;
    [SerializeField] GameObject LoseImage;
    [SerializeField] GameObject RetryButton;

    void StartPositions(){

    }

    public void WinnerAnimation(){
        PanelFadeIn();
        WinnerImage.transform.DOScale(Vector3.one, .5f).OnComplete(()=>{
            ShowButton();
        });
    }

    public void LoseAnimation(){
        PanelFadeIn();
        LoseImage.transform.DOScale(Vector3.one, .5f).OnComplete(()=>{
            ShowButton();
        });
    }

    void ShowButton(){
        RetryButton.transform.DOLocalMoveY(-285, 1f);
    }
    
    void PanelFadeIn(){
        BackgroundPanel.SetActive(true);
        BackgroundPanel.GetComponent<Image>().DOFade(.8f, .3f);
    }
}
