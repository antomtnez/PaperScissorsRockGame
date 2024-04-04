using System;
using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour{
    private Camera m_Camera;
    [SerializeField] Vector3 PositionStrenght;

    private static event Action Shake;

    void OnEnable() => Shake += CameraShake;
    void OnDisable() => Shake -= CameraShake;
    void Start(){
        m_Camera = Camera.main;
    }

    public static void Invoke(){
        Shake?.Invoke();
    }

    void CameraShake(){
        m_Camera.DOComplete();
        m_Camera.DOShakePosition(.5f, PositionStrenght);
    }
    
}
