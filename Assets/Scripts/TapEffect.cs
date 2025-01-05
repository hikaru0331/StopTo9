using UnityEngine;
using UnityEngine.UIElements;

public class TapEffect : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    [SerializeField]
    private Camera _camera;
 
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
 
    void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            var pos = Input.mousePosition;
            pos.z = 10f; 
                 
            transform.position = _camera.ScreenToWorldPoint(pos);
            _particleSystem.Play();
        }
    }
}