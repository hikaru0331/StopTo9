using UnityEngine;
using UniRx.Toolkit;

public class ParticlePool : ObjectPool<TapEffect>
{
    private readonly TapEffect _prefab;
    private readonly Transform _parentTransform;

    public ParticlePool(Transform transform, TapEffect prefab)
    {
        _parentTransform = transform;
        _prefab = prefab;
    }

    protected override TapEffect CreateInstance()
    {
        var e = Object.Instantiate(_prefab, _parentTransform, true);

        return e;
    }
}