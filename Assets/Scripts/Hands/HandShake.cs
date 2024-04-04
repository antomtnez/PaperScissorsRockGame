using UnityEngine;
using DG.Tweening;

public class HandShake : MonoBehaviour{
    [SerializeField] float MoveTime;
    [SerializeField] int TotalShakes;
    private int m_CurrentShakes = 0;
    [Header("Movement")]
    [SerializeField] float MoveOffset;
    [Header("Rotation")]
    [SerializeField] float RotationOffset;

    public void StartShaking(){
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(RotationOffset, 0, 0), MoveTime);
        transform.DOMoveY(transform.position.y + MoveOffset, MoveTime).OnComplete(()=>{
            ComeBack();
        });
    }

    void ComeBack(){
        transform.DORotate(transform.rotation.eulerAngles - new Vector3(RotationOffset, 0, 0), MoveTime);
        transform.DOMoveY(transform.position.y - MoveOffset, MoveTime).OnComplete(()=>{
            if(m_CurrentShakes == TotalShakes){
                m_CurrentShakes = 0;
                transform.DOKill();
            }else{
                m_CurrentShakes++;
                StartShaking();
            }
        });
    }
}
