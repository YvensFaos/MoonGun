using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class AsteroidDestruction : MonoBehaviour
{
   private Rigidbody _rigidbody;
   private Renderer _renderer;
   private Material _material;
   
   [SerializeField] private ParticleSystem particleSystem;

   private readonly string uniformName = "LightPower";
   
   private void Awake()
   {
      _renderer = GetComponent<Renderer>();
      _rigidbody = GetComponent<Rigidbody>();
      _material = _renderer.material;
   }

   private void OnCollisionEnter(Collision other)
   {
      Destroy(other.gameObject);   
   }

   private void OnTriggerEnter(Collider other)
   {
      Destroy(other.gameObject);   
   }

   private void Destroy(GameObject collidingGameObject)
   {
      if (collidingGameObject.CompareTag("Bullet"))
      {
         if (particleSystem != null)
         {
            particleSystem.Play();
         }
         GameLogic.Instance.ShakeFightCamera(2.0f, 0.5f);
         GameLogic.Instance.AsteroidDestroyed();
         _rigidbody.velocity = Vector3.zero;
         DOTween.To(() => _material.GetFloat(uniformName), 
            value => _material.SetFloat(uniformName, value), 
            -2.0f, 0.5f).OnComplete(
            () =>
            {
               particleSystem.transform.parent = null;
               Destroy(gameObject, 0.2f);
            }); 
      }
   }
}