using UnityEngine;

public class HandAnimatorController{
    private Animator m_HandAnimatorController;

    public HandAnimatorController(Animator animator){
        m_HandAnimatorController = animator;
    }

    public void SetChoiceAnimation(Choice choice){
        m_HandAnimatorController.SetInteger("Choice", (int)choice);
    }

    public void CloseHandAnimation(){
        m_HandAnimatorController.SetInteger("Choice", 0);
    }
}
