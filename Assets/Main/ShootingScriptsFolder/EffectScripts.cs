using UnityEngine;

public class EffectScripts : MonoBehaviour
{
    void Start()
    {
        // ���o������������폜����
        var particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, particleSystem.main.duration);
    }
}
