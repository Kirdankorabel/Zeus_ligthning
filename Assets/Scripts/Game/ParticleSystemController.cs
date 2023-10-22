using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private int _ParticleCount = 20;
    [SerializeField] private ParticleSystem _particleSystem;

    public void Emission(Vector3 position, Color color)
    {
        _particleSystem.transform.position = position;
        _particleSystem.Emit(_ParticleCount);
        var colorGradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = color;
        colorKeys[0].time = 0.0f;
        colorKeys[1].color = color; 
        colorKeys[1].time = 1.0f;
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].alpha = 0.0f;
        alphaKeys[1].time = 1.0f;
        colorGradient.SetKeys(colorKeys, alphaKeys);

        var colorOverLifetime = _particleSystem.colorOverLifetime;
        colorOverLifetime.color = colorGradient;
    }
}
