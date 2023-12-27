using UnityEngine;

public class EffectScripts : MonoBehaviour
{
    void Start()
    {
        // ‰‰o‚ªŠ®—¹‚µ‚½‚çíœ‚·‚é
        var particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, particleSystem.main.duration);
    }
}
