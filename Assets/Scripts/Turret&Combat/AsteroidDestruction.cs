using DG.Tweening;
using Lean.Pool;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(AudioSource))]
public class AsteroidDestruction : MonoBehaviour
{
   private Rigidbody _rigidbody;
   private Renderer _renderer;
   private Material _material;
   private AudioSource _audioSource;
   
   [SerializeField] private ParticleSystem particleSystem;

   private readonly string uniformName = "LightPower";
   private readonly string transparencyName = "Transparency";

   [Header("Asteroid Properties")] 
   [SerializeField] private int asteroidsValue;
   [SerializeField] private int mineralsValue;
   [SerializeField] private float shakeForce = 2.0f;
   [SerializeField] private float shakeTime = 0.5f;
   [SerializeField] private float lightEffectDefault = -2.0f;
   [SerializeField] private float lightEffectPower = -2.0f;
   [SerializeField] private float lightEffectTimer = 0.5f;
   [SerializeField] private float despawnTimer = 0.5f;
   [SerializeField] private float damagePower = 0.0f;

   public float DamagePower => damagePower;

   [SerializeField] private AsteroidType type;
   [SerializeField] private AsteroidEffects effect;

   private float _lightIntensity;
   
   private void Awake()
   {
      _renderer = GetComponent<Renderer>();
      _rigidbody = GetComponent<Rigidbody>();
      _audioSource = GetComponent<AudioSource>();
      _material = _renderer.material;
   }

   private void OnEnable()
   {
      _material.SetFloat(uniformName, lightEffectDefault);
      _material.SetFloat(transparencyName, 1.0f);
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
            GameLogic.Instance.ShakeFightCamera(shakeForce * 1.2f, shakeTime);
         }
         StopAndAnimateAsteroidDestruction(false);
      }
   }

   private void StopAndAnimateAsteroidDestruction(bool wasHit = true)
   {
      _audioSource.Play();
      if (particleSystem != null)
      {
         var particles= LeanPool.Spawn(particleSystem, transform.position, Quaternion.identity);
         LeanPool.Despawn(particles, 0.5f);
      }
      
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
            DOTween.To(() => _material.GetFloat(transparencyName),
               value => _material.SetFloat(transparencyName, value),
               0.0f, despawnTimer * 0.9f);
            LeanPool.Despawn(gameObject, despawnTimer);
         });
   }
}