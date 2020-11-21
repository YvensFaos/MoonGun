using System;
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
   
   private void Awake()
   {
      _renderer = GetComponent<Renderer>();
      _rigidbody = GetComponent<Rigidbody>();
      _material = _renderer.material;
   }

   private void OnEnable()
   {
      _material.SetFloat(uniformName, lightEffectDefault);
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
         GameLogic.Instance.AsteroidDestroyed(asteroidsValue, mineralsValue);
         GameLogic.Instance.QuestControl.NotifyAsteroidDestroyed();
         
         _rigidbody.velocity = Vector3.zero;
         
         DOTween.To(() => _material.GetFloat(uniformName), 
            value => _material.SetFloat(uniformName, value), 
            lightEffectPower, lightEffectTimer).OnComplete(
            () =>
            {
               particleSystem.transform.parent = null;
               LeanPool.Despawn(gameObject, 0.2f);
            }); 
      }
   }
}