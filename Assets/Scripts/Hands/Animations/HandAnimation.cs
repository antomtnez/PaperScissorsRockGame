using UnityEngine;

public abstract class HandAnimation : MonoBehaviour{
    protected Transform m_Hand;
    void Start(){
        m_Hand = GetComponentInParent<Hand>().transform;
    }
    public abstract void StartAnimation();
}
