using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HandAnimatorController : MonoBehaviour{
    private Animator m_HandAnimatorController;
    private Dictionary<string, HandAnimation> m_HandAnimations = new Dictionary<string, HandAnimation>();

    void Start(){
        m_HandAnimatorController = GetComponent<Animator>();
        InitializeAnimationDictionary();
    }

    void InitializeAnimationDictionary(){
        foreach(HandAnimation handAnimation in GetComponentsInChildren<HandAnimation>()){
            m_HandAnimations.Add(handAnimation.GetType().Name, handAnimation);
        }   
    }

    public void ChoiceAnimation(Choice choice){
        m_HandAnimatorController.SetInteger("Choice", (int)choice);
    }

    public void CloseHandAnimation(){
        m_HandAnimatorController.SetInteger("Choice", 0);
    }

    public void StartAnimation(string animationKey){
        m_HandAnimations.TryGetValue(animationKey, out HandAnimation handAnimation);
        handAnimation.StartAnimation();
    }

    public void WinnerAnimation(){
        m_HandAnimatorController.SetTrigger("IsWinner");
    }

    public void DamageAnimation(){
        m_HandAnimatorController.SetTrigger("Damaged");
        m_HandAnimations.TryGetValue("HandKnockback", out HandAnimation handAnimation);
        handAnimation.StartAnimation();
    }
}
