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
    [SerializeField] private Camera turretCamera;
    

    [Header("Turret Properties")] 
    [Range(1.0f, 150.0f)]
    [SerializeField] private float rotationRange = 70.0f;
    [SerializeField] private TurretCannonType cannonType = TurretCannonType.CANNON;
    
    private bool _unlockedLaser = false;
    private bool _canMove = true;
    private bool _canShoot = true;
    
    [Header("Projectile Properties")]
    [SerializeField] private Rigidbody bulletGameObject;
    [SerializeField] private float projectileForce = 4.0f;
    [SerializeField] private float projectileLife = 4.0f;
    [SerializeField] private Vector3 projectileScaling = new Vector3(0.09f, 0.09f, 0.09f);
    [SerializeField] private float coolDownTimer = 1.5f;
    
    [Header("Laser Properties")]
    [SerializeField] private GameObject laserObject;
    [SerializeField] private float laserCooldown = 2.0f;
    [SerializeField] private float laserConsuption = 0.5f;
    
    [Header("Debug Properties")]
    [SerializeField] private float rayDebugLength = 0.22f;
    
    void Update()
    {
        if (_canMove)
        {
            Vector2 positionOnScreen = turretCamera.WorldToViewportPoint (transform.position);
            Vector2 mouseOnScreen = turretCamera.ScreenToViewportPoint(Input.mousePosition);
            
            float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
            {
                var y = a.y - b.y;
                //Force positive Y to avoid angle inversion at 180 degrees.
                y = y < 0.0f ? 0.0f : y;
                return Mathf.Atan2(y, a.x - b.x) * Mathf.Rad2Deg;
            }

            //-90.0f in the end was necessary due to the object's initial position
            float angle = AngleBetweenTwoPoints( mouseOnScreen, positionOnScreen) - 90.0f;
            angle = Mathf.Clamp(angle, -rotationRange, rotationRange);
            turretRotation.transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));
        }
        
        ShootMechanic();
        SwitchWeapon();
    }

    private void ShootMechanic() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canShoot)
        {
            switch (cannonType)
            {
                case TurretCannonType.CANNON: CannonShoot();
                    break;
                case TurretCannonType.LASER: LaserShoot();
                    break;
            }
        }
    }

    private void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _unlockedLaser)
        {
            switch (cannonType)
            {
                case TurretCannonType.CANNON:
                    cannonType = TurretCannonType.LASER; 
                    break;
                case TurretCannonType.LASER:
                    cannonType = TurretCannonType.CANNON;
                    break;
            }
        }
    }
    

    private void CannonShoot()
    {
        var tipPosition = turretAimTip.transform.position;
        var vector = tipPosition - turretAimBase.transform.position;
        vector.Normalize();

        var bullet = Instantiate(bulletGameObject, tipPosition,
            Quaternion.identity);
        bullet.transform.localScale = projectileScaling;
        bullet.AddForce(vector * projectileForce, ForceMode.Impulse);
        Destroy(bullet.gameObject, projectileLife);

        _canShoot = false;
        cooldownImage.fillAmount = 0.0f;
        var tween = cooldownImage.DOFillAmount(1.0f, coolDownTimer);
        tween.OnComplete(() =>
        {
            CanShootAgain();
        });
    }

    private void LaserShoot()
    {
        GameLogic.Instance.ShakeFightCamera(3.0f, 0.5f);
        laserObject.SetActive(true);
        _canShoot = false;
        cooldownImage.fillAmount = 1.0f;
        var tween = cooldownImage.DOFillAmount(0.0f, laserConsuption);
        tween.OnComplete(() =>
        {
            laserObject.SetActive(false);    
            cooldownImage.DOFillAmount(1.0f, laserCooldown).OnComplete(() =>
            {
                CanShootAgain();    
            });
        });
    }

    private void CanShootAgain()
    {
        _canShoot = true;
    }

    public void ToggleTurretMovement(bool movement)
    {
        _canMove = movement;
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

    public void UpgradeProjectileForce(float newForce)
    {
        projectileForce = newForce;
    }

    public void UpgradeProjectileScale(Vector3 newScale)
    {
        projectileScaling = newScale;
    }

    public void UpgradeCooldown(float newCooldown)
    {
        coolDownTimer = newCooldown;
    }

    public void UnlockLaser()
    {
        _unlockedLaser = true;
    }
}
