using DG.Tweening;
using UnityEngine;

public class HandKnockback : HandAnimation{
    [SerializeField] float KnockbackTime;
    [SerializeField] float RecoveryTime;

    [Header("Movement")]
    [SerializeField] float KnockbackOffset;

    [Header("Hand Side")]
    [SerializeField] bool IsOnRight = false;

    public override void StartAnimation(){
        if(IsOnRight){
            DamageKnockbackGlassed();
        }else{
            DamageKnockback();
        }
    }

    void DamageKnockback(){
        CameraShaker.Invoke();
        m_Hand.DOLocalMoveX(m_Hand.position.x - KnockbackOffset, KnockbackTime).OnComplete(() => {
            Comeback();
        });
    }

    void Comeback(){
        m_Hand.DOLocalMoveX(m_Hand.position.x + KnockbackOffset, RecoveryTime);
    }

    void DamageKnockbackGlassed(){
        CameraShaker.Invoke();
        m_Hand.DOLocalMoveX(m_Hand.position.x + KnockbackOffset, KnockbackTime).OnComplete(() => {
            ComebackGlassed();
        });
    }

    void ComebackGlassed(){
        m_Hand.DOLocalMoveX(m_Hand.position.x - KnockbackOffset, RecoveryTime);
    }
}