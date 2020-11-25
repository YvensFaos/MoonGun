using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponTextPanel : MonoBehaviour
{
    [SerializeField] Text weaponText;
    [SerializeField] private UnityEvent switchEvent;
    
    public void ChangeTextTo(TurretCannonType type)
    {
        switch (type)
        {
            case TurretCannonType.LASER: weaponText.text = "LASER";
                break;
            case TurretCannonType.CANNON: weaponText.text = "CANNON";
                break;
        }
        switchEvent.Invoke();
    }
}
