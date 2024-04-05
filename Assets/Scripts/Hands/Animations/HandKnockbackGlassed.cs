using DG.Tweening;
using UnityEngine;

public class HandKnockbackGlassed : HandAnimation{
    [SerializeField] float KnockbackTime;
    [SerializeField] float RecoveryTime;

    [Header("Movement")]
    [SerializeField] float KnockbackOffset;

    public override void StartAnimation(){
        DamageKnockback();
    }

    void DamageKnockback(){
        CameraShaker.Invoke();
        m_Hand.DOLocalMoveX(m_Hand.position.x + KnockbackOffset, KnockbackTime).OnComplete(() => {
            Comeback();
        });
    }

    void Comeback(){
        m_Hand.DOLocalMoveX(m_Hand.position.x - KnockbackOffset, RecoveryTime);
    }
}
