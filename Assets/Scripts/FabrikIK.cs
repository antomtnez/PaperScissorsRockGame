using UnityEngine;

public class FabrikIK : MonoBehaviour{
    [SerializeField] Transform[] bones;
    private float[] m_BonesLengths;
    private Quaternion[] m_InitialRotations;
    [SerializeField] int fabrikIterations = 5; //Times we apply Fabrik Method
    [SerializeField] Transform targetPosition;

    void Start(){
        CalculateBonesLengths();
        StoreInitialRotations();
    }

    void CalculateBonesLengths(){
        m_BonesLengths = new float[bones.Length];

        //Calculate each bone length. Last bone have 0 length
        for(int i=0; i < bones.Length; i++)
            m_BonesLengths[i] = i < bones.Length - 1 ? (bones[i+1].position - bones[i].position).magnitude : 0;
    }

    void StoreInitialRotations(){
        m_InitialRotations = new Quaternion[bones.Length];

        for(int i=0; i < bones.Length; i++)
            m_InitialRotations[i] = bones[i].localRotation;
    }

    void Update(){
        FabrikMethod();
    }

    void FabrikMethod(){
        Vector3[] finalBonesPositions = new Vector3[bones.Length];

        //Save start bones positions
        for(int i=0; i < bones.Length; i++)
            finalBonesPositions[i] = bones[i].position;

        //Apply Fabrik many times as fabrikIterations
        for(int i=0; i < fabrikIterations; i++)
            finalBonesPositions = SolveForwardPositions(SolveInversePositions(finalBonesPositions));

        //Apply each bone his result position and rotation
        for(int i=0; i < bones.Length; i++){
            bones[i].position = finalBonesPositions[i];
            /*
            bones[i].rotation = m_InitialRotations[i]; //Reset rotation

            //Apply rotation to each bone looks at next one
            Quaternion targetRotation = i != bones.Length - 1 ? 
                Quaternion.LookRotation(finalBonesPositions[i+1] - bones[i].position) : 
                Quaternion.LookRotation(targetPosition.position - bones[i].position);
            bones[i].rotation = bones[i].rotation * targetRotation;
            */
        }
    }

    Vector3[] SolveInversePositions(Vector3[] forwardPositions){
        Vector3[] inversePositions = new Vector3[forwardPositions.Length];

        for(int i=(forwardPositions.Length - 1); i >= 0; i--){
            //Last bone positions is target position
            if(i == (forwardPositions.Length - 1)){
                inversePositions[i] = targetPosition.position;
            }else{
                //Calculate unit vector from backward inverse position to current base position
                Vector3 direction = (inversePositions[i+1] - forwardPositions[i]).normalized;
                float boneLength = m_BonesLengths[i];
                inversePositions[i] = inversePositions[i+1] + (direction * boneLength);
            }
        }

        return inversePositions;;
    }

    Vector3[] SolveForwardPositions(Vector3[] inversePositions){
        Vector3[] forwardPositions = new Vector3[inversePositions.Length];

        for(int i=0; i < inversePositions.Length; i++){
            if(i == 0){
                //First bone position its always the base position, no change
                forwardPositions[i] = bones[0].position;
            }else{
                //Calculate unit vector from backward position to current position
                Vector3 direction = (inversePositions[i] - forwardPositions[i-1]).normalized;
                float boneLength = m_BonesLengths[i-1];
                forwardPositions[i] = forwardPositions[i-1] + (direction * boneLength);
            }
        }

        return forwardPositions;
    }
}
