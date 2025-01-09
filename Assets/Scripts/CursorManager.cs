using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Camera particleCamera;
    
    private void Update()
    {
        Vector3 mousePos = particleCamera.ScreenToWorldPoint(Input.mousePosition + particleCamera.transform.forward * 5);

        if (Input.GetMouseButtonDown(0))
        {
            ParticleGenerator.Instance.GenerateParticle(mousePos);
        }
    }
}