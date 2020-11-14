using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TurretControl : MonoBehaviour
{
    [Header("Turret Objects")]
    [SerializeField] private GameObject turretRotation;
    [SerializeField] private GameObject turretAimBase;
    [SerializeField] private GameObject turretAimTip;
    [SerializeField] private Image cooldownImage;
    
    [Header("Turret Properties")] 
    [SerializeField] private float coolDownTimer = 0.5f;
    [Range(1.0f, 150.0f)]
    [SerializeField] private float rotationRange = 1.0f;
    [SerializeField] private float rotationSpeed = 1.0f;

    private bool _canShoot = true;
    
    [Header("Projectile Properties")]
    [SerializeField] private Rigidbody bulletGameObject;
    [SerializeField] private float projectileForce = 100.0f;
    [SerializeField] private float projectileLife = 2.0f;
    [SerializeField] private Vector3 projectileScaling = new Vector3(1.0f, 1.0f, 1.0f);
    
    [Header("Debug Properties")]
    [SerializeField] private float rayDebugLength = 10.0f;
    
    void Update()
    {
        var rot = turretRotation.transform.rotation.eulerAngles;
        float rotation = 0.0f;
        bool rotationHappened = false;
        if (Input.GetKey(KeyCode.A))
        {
            rotation = rot.z + rotationSpeed;
            rotationHappened = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotation = rot.z - rotationSpeed;
            rotationHappened = true;
        }
        if (rotationHappened && (rotation > 360.0f - rotationRange || rotation < rotationRange))
        {
            rot.z = rotation;
        }
        turretRotation.transform.rotation = Quaternion.Euler(rot);

        
        if (Input.GetKeyDown(KeyCode.Space) && _canShoot)
        {
            var tipPosition = turretAimTip.transform.position;
            var vector = tipPosition - turretAimBase.transform.position;
            vector.Normalize();
            
            var bullet = Instantiate(bulletGameObject, turretRotation.transform.position,
                Quaternion.identity);
            bullet.transform.localScale = projectileScaling;
            bullet.AddForce(vector * projectileForce, ForceMode.Impulse);
            Destroy(bullet.gameObject, projectileLife);

            _canShoot = false;
            cooldownImage.fillAmount = 0.0f;
            var tween = cooldownImage.DOFillAmount(1.0f, coolDownTimer);
            tween.OnComplete(CanShootAgain);
        }
    }

    private void CanShootAgain()
    {
        _canShoot = true;
    }

    public void OnDrawGizmos()
    {
        if (turretAimBase != null && turretAimTip != null)
        {
            var tipPosition = turretAimTip.transform.position;
            var vector = tipPosition - turretAimBase.transform.position;
            vector.Normalize();
            Gizmos.color = Color.red;
            Gizmos.DrawRay(tipPosition, vector * rayDebugLength);
        }
    }
}
