using System.Collections;
using UnityEngine;

public class HandDead : HandAnimation{
    [SerializeField] GameObject SkeletonRoot;
    [SerializeField] GameObject HandCenterOfMass;
    [SerializeField] float LastHitImpulseForce;

    public override void StartAnimation(){        
        ActiveRagdoll();
        HandCenterOfMass.GetComponent<Rigidbody>().AddForce(Vector3.up * LastHitImpulseForce, ForceMode.Impulse);
        StartCoroutine(ActiveRagdollDelayed());
    }

    IEnumerator ActiveRagdollDelayed(){
        yield return new WaitForSeconds(.3f);
        GetComponentInParent<Animator>().enabled = false;
    }

    void ActiveRagdoll(){
        foreach(Rigidbody rigidbody in SkeletonRoot.GetComponentsInChildren<Rigidbody>()){
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
    }
}
