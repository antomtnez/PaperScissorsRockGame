using DG.Tweening;
using UnityEngine;

public class TitleTransition : MonoBehaviour{
    [SerializeField] GameObject TitlePanel;
    [SerializeField] ReadyMessageAnimation ReadyMessageAnimation;
    private Vector3 m_StartPosition;

    void Start(){
        m_StartPosition = transform.localPosition;
    }

    public void StartTransition(){
        InTransition();
    }

    void InTransition(){
        transform.DOLocalMoveX(transform.localPosition.x - 2100, 1.4f).OnComplete(()=>{
            TitlePanel.SetActive(false);
            OutTransition();
        });
    }

    void OutTransition(){
        transform.DOLocalMoveX(transform.localPosition.x - 2100, 1.4f).OnComplete(()=>{
            ReadyMessageAnimation.StartAnimation();
        });
    }
}