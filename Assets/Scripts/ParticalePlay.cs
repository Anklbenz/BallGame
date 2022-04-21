using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ParticalePlay : MonoBehaviour
{
    private ParticleSystem _particleSystem;
   

      private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetColor(Color color)
    {
        var main = _particleSystem.main;
        main.startColor = color;
    }

    public void Play()
    {
        var main = _particleSystem.main;
        float time = main.duration;
        _particleSystem.Play();
       
        StartCoroutine(DeactivateParticales(time));
    }

    IEnumerator DeactivateParticales(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
