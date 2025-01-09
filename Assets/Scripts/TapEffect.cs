using System;
using UnityEngine;
using UniRx;

public class TapEffect : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public IObservable<Unit> PlayParticle(Vector3 position)
    {
        transform.position = position;
        particle.Play();

        // ParticleSystemのstartLifetimeに設定した秒数が経ったら終了通知
        return Observable.Timer(TimeSpan.FromSeconds(particle.main.startLifetimeMultiplier))
            .ForEachAsync(_ => particle.Stop());
    }
}
