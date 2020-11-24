using DG.Tweening;
using Lean.Pool;
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
    
    private bool _unlockedLaser; //false by default
    private bool _canMove; //false by default
    private bool _canShoot = true;
    
    [Header("Projectile Properties")]
    [SerializeField] private Rigidbody bulletGameObject;
    [SerializeField] private float projectileForce = 4.0f;
    [SerializeField] private float projectileLife = 4.0f;
    [SerializeField] private Vector3 projectileScaling = new Vector3(0.09f, 0.09f, 0.09f);
    [SerializeField] private float cannonCoolDownTimer = 1.5f;
    
    [Header("Laser Properties")]
    [SerializeField] private GameObject laserObject;
    [SerializeField] private float laserCooldown = 2.0f;
    [SerializeField] private float laserConsuption = 0.5f;
    
    [Header("Debug Properties")]
    [SerializeField] private float rayDebugLength = 0.22f;

    public float RotationRange => rotationRange;

    public float ProjectileForce => projectileForce;

    public float ProjectileLife => projectileLife;

    public float ProjectileScale => projectileScaling.x;

    public float CannonCoolDownTimer => cannonCoolDownTimer;

    public bool UnlockedLaser => _unlockedLaser;

    public float LaserConsuption => laserConsuption;

    public float LaserCooldown => laserCooldown;

    void Update()
    {
        if (_canMove)
        {
            RotateTurret();
            ShootMechanic();
            SwitchWeapon();
        }
    }

    private void RotateTurret()
    {
        var position = turretRotation.transform.position;
        var mousePos = Input.mousePosition;
        var screenPos = turretCamera.ScreenToWorldPoint(
            new Vector3(mousePos.x, mousePos.y, position.z - turretCamera.transform.position.z));

        var eulerAngles = turretRotation.transform.rotation.eulerAngles;
        eulerAngles.z = Mathf.Atan2((screenPos.y - position.y), (screenPos.x - position.x)) * Mathf.Rad2Deg - 90.0f;
        eulerAngles.z = Mathf.Clamp(eulerAngles.z, -RotationRange, RotationRange);
        turretRotation.transform.rotation = Quaternion.Euler(eulerAngles);
    }

    private void ShootMechanic() 
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && _canShoot)
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && UnlockedLaser)
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
            
            GameLogic.Instance.ChangeWeapon(cannonType);
        }
    }
    
    private void CannonShoot()
    {
        var tipPosition = turretAimTip.transform.position;
        var vector = tipPosition - turretAimBase.transform.position;
        vector.Normalize();

        var bullet = LeanPool.Spawn(bulletGameObject, tipPosition, Quaternion.identity);
        bullet.transform.localScale = projectileScaling;
        bullet.AddForce(vector * ProjectileForce, ForceMode.Impulse);
        Destroy(bullet.gameObject, ProjectileLife);

        _canShoot = false;
        cooldownImage.fillAmount = 0.0f;
        cooldownImage.DOFillAmount(1.0f, CannonCoolDownTimer).OnComplete(CanShootAgain);
    }

    private void LaserShoot()
    {
        GameLogic.Instance.ShakeFightCamera(3.0f, 0.5f);
        laserObject.SetActive(true);
        _canShoot = false;
        cooldownImage.fillAmount = 1.0f;
        cooldownImage.DOFillAmount(0.0f, LaserConsuption).OnComplete(() =>
        {
            laserObject.SetActive(false);    
            cooldownImage.DOFillAmount(1.0f, LaserCooldown).OnComplete(CanShootAgain);
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

    public void UpgradeRotationRange(float range)
    {
        rotationRange = range;
    }

    public void UpgradeProjectileForce(float newForce)
    {
        projectileForce = newForce;
    }

    public void UpgradeProjectileScale(Vector3 newScale)
    {
        projectileScaling = newScale;
    }

    public void UpgradeCannonCooldown(float newCooldown)
    {
        cannonCoolDownTimer = newCooldown;
    }

    public void UpgradeLaserDuration(float newDuraction)
    {
        laserConsuption = newDuraction;
    }

    public void UpgradeLaserCooldown(float newCooldown)
    {
        laserCooldown = newCooldown;
    }

    public void UnlockLaser()
    {
        _unlockedLaser = true;
    }
}
