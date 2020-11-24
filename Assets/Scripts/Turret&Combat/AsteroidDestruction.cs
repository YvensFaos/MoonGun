using DG.Tweening;
using Lean.Pool;
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

   [Header("Asteroid Properties")] 
   [SerializeField] private int asteroidsValue;
   [SerializeField] private int mineralsValue;
   [SerializeField] private float shakeForce = 2.0f;
   [SerializeField] private float shakeTime = 0.5f;
   [SerializeField] private float lightEffectDefault = -2.0f;
   [SerializeField] private float lightEffectPower = -2.0f;
   [SerializeField] private float lightEffectTimer = 0.5f;

   [SerializeField] private AsteroidType type;
   [SerializeField] private AsteroidEffects effect;

   private float _lightIntensity;
   
   private void Awake()
   {
      _renderer = GetComponent<Renderer>();
      _rigidbody = GetComponent<Rigidbody>();
      _material = _renderer.material;
   }

   private void OnEnable()
   {
      _material.SetFloat(uniformName, lightEffectDefault);
      _lightIntensity = GameLogic.Instance.LightIntensity;
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
            var particles= LeanPool.Spawn(particleSystem, transform.position, Quaternion.identity);
            LeanPool.Despawn(particles, 0.5f);
         }
         GameLogic.Instance.ShakeFightCamera(shakeForce, shakeTime);
         GameLogic.Instance.AsteroidDestroyed(type, asteroidsValue, mineralsValue);
         if (effect != AsteroidEffects.NO_EFFECT)
         {
            GameLogic.Instance.AsteroidEffect(effect);
         }
         GameLogic.Instance.QuestControl.NotifyAsteroidDestroyed(type);
         
         StopAndAnimateAsteroidDestruction();
      }
      else if (collidingGameObject.CompareTag("Shield"))
      {
         if (effect == AsteroidEffects.DAMAGE)
         {
            GameLogic.Instance.DamageShield(this);
         }
         else
         {
            GameLogic.Instance.AnimateShield();
         }
         StopAndAnimateAsteroidDestruction(false);
      }
   }

   private void StopAndAnimateAsteroidDestruction(bool wasHit = true)
   {
      if (wasHit)
      {
         _rigidbody.velocity = Vector3.zero;   
      }
      else
      {
         _rigidbody.velocity = _rigidbody.velocity * 0.1f;
      }

      DOTween.To(() => _material.GetFloat(uniformName),
         value => _material.SetFloat(uniformName, value),
         _lightIntensity * lightEffectPower, lightEffectTimer).OnComplete(
         () =>
         {
            particleSystem.transform.parent = null;
            LeanPool.Despawn(gameObject, 0.3f);
         });
   }
}