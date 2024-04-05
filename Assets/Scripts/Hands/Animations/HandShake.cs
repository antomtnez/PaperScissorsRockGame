using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class HandShake : HandAnimation{
    [SerializeField] float MoveTime;
    [SerializeField] int TotalShakes;
    private int m_CurrentShakes;
    [Header("Movement")]
    [SerializeField] float MoveOffset;
    [Header("Rotation")]
    [SerializeField] float RotationOffset;

    public override void StartAnimation(){
        StartShaking();
    }

    void StartShaking(){
        m_Hand.DORotate(m_Hand.rotation.eulerAngles + new Vector3(RotationOffset, 0, 0), MoveTime);
        m_Hand.DOMoveY(m_Hand.position.y + MoveOffset, MoveTime).OnComplete(()=>{
            ComeBack();
        });
    }

    void ComeBack(){
        m_Hand.DORotate(m_Hand.rotation.eulerAngles - new Vector3(RotationOffset, 0, 0), MoveTime);
        m_Hand.DOMoveY(m_Hand.position.y - MoveOffset, MoveTime).OnComplete(()=>{
            if(m_CurrentShakes == TotalShakes){
                m_CurrentShakes = 0;
                m_Hand.DOKill();
            }else{
                m_CurrentShakes++;
                StartShaking();
            }
        });
    }
}
