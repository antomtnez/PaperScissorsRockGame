using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenFX : MonoBehaviour{
    private SkinnedMeshRenderer m_HandMeshRenderer;
    private Material m_MaterialEffect;

    public float EffectDuration;
    public float EffectIntensity;
    private float m_EffectTimer;

    void Start(){
        m_HandMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        FindMaterialEffect();
    }

    void FindMaterialEffect(){
        List<Material> materials = new List<Material>();
        m_HandMeshRenderer.GetMaterials(materials);
        m_MaterialEffect = materials[1];
    }

    public void StartFX(){
        m_EffectTimer = EffectDuration;
        StartCoroutine(EndFX());
    }

    IEnumerator EndFX(){
        Color finalColor = Color.white * 1;

        m_MaterialEffect.color = finalColor;
        m_MaterialEffect.SetColor("_EmissionColor", finalColor);

        yield return new WaitForSeconds(0.3f);

        while(m_EffectTimer > 0){
            yield return new WaitForSeconds(0.1f);
            m_EffectTimer -= 0.1f;

            float lerp = Mathf.Clamp01(m_EffectTimer/EffectDuration);
            float intensity = lerp * EffectIntensity;

            finalColor = Color.white * intensity;

            m_MaterialEffect.color = finalColor;
            m_MaterialEffect.SetColor("_EmissionColor", finalColor);
        }
    }
}
