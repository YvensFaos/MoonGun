using UnityEngine;

public class TurretControl : MonoBehaviour
{

    public GameObject turrentRotation;
    [Range(1.0f, 150.0f)]
    public float rotationRange = 1.0f;

    public float rotationSpeed = 1.0f;
    
    public GameObject turrentAimBase;
    public GameObject turrentAimTip;

    public float rayDebugLength = 10.0f;

    public Rigidbody bulletGameObject;
    public float projectileForce = 100.0f;
    public float projectileLife = 2.0f;
    
    void Update()
    {
        var rot = turrentRotation.transform.rotation.eulerAngles;
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
        turrentRotation.transform.rotation = Quaternion.Euler(rot);

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var tipPosition = turrentAimTip.transform.position;
            var vector = tipPosition - turrentAimBase.transform.position;
            vector.Normalize();
            
            var bullet = Instantiate(bulletGameObject, turrentRotation.transform.position,
                Quaternion.identity);
            bullet.AddForce(vector * projectileForce, ForceMode.Impulse);
            Destroy(bullet.gameObject, projectileLife);
        }
    }

    public void OnDrawGizmos()
    {
        if (turrentAimBase != null && turrentAimTip != null)
        {
            var tipPosition = turrentAimTip.transform.position;
            var vector = tipPosition - turrentAimBase.transform.position;
            vector.Normalize();
            Gizmos.color = Color.red;
            Gizmos.DrawRay(tipPosition, vector * rayDebugLength);
        }
    }
}
