using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class ParticleGenerator : MonoBehaviour
{
    public static ParticleGenerator Instance;
    
    [SerializeField, TooltipAttribute("パーティクルのPrefab")]
    private GameObject particlePrefab;
    private ParticlePool particlePool;
    
    void Awake ()
    {
        // シングルトンの実装
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        }
        else {
            Destroy (gameObject);
        }
    }
    
    private void Start()
    {
        //ObjectPoolを生成
        particlePool = new ParticlePool(transform, particlePrefab.GetComponent<TapEffect>());
        
        //破棄されたとき（Disposeされたとき）にObjectPoolを解放する
        this.OnDestroyAsObservable().Subscribe(_ => particlePool.Dispose());
    }
    
    public void GenerateParticle(Vector3 position)
    { 
        //ObjectPoolから1つ取得
        var effect = particlePool.Rent();
        
        //エフェクトを再生し、再生終了したらpoolに返却する
        effect.PlayParticle(position)
            .Subscribe(__ => { particlePool.Return(effect); });
    }
}