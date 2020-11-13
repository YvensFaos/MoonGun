using UnityEngine;

public class AsteroidDestruction : MonoBehaviour
{
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
         GameLogic.Instance.AsteroidDestroyed();
         Destroy(gameObject, 0.1f);
         Destroy(collidingGameObject, 0.1f);
      }
   }
}