using UnityEngine;
using DG.Tweening;

public class HandShake : MonoBehaviour{
    [SerializeField] float MoveTime;
    [Header("Movement")]
    [SerializeField] float MoveOffset;
    [Header("Rotation")]
    [SerializeField] float RotationOffset;

    void Start(){
        ShakeMe();
    }

    public void ShakeMe(){
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(RotationOffset, 0, 0), MoveTime);
        transform.DOMoveY(transform.position.y + MoveOffset, MoveTime).OnComplete(()=>{
            ComeBack();
        });
    }

    void ComeBack(){
        transform.DORotate(transform.rotation.eulerAngles - new Vector3(RotationOffset, 0, 0), MoveTime);
        transform.DOMoveY(transform.position.y - MoveOffset, MoveTime).OnComplete(()=>{
            ShakeMe();
        });
    }
}
