using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class RaycastActivate : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private GameObject activateObject;

    [SerializeField] private UnityEvent eventsWhenHovering;
    
    [SerializeField] private UnityEvent eventsWhenNotHovering;

    private Collider _collider;
    private bool justHovered;
    private bool readyToHover;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        justHovered = false;
        readyToHover = false;
        
        activateObject.SetActive(false);
    }

    public void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit, 100.0f)) 
        {
            Debug.DrawLine (ray.origin, hit.point);

            if (hit.collider.Equals(_collider))
            {
                justHovered = true;
                activateObject.SetActive(true);
                if (!readyToHover)
                {
                    eventsWhenHovering.Invoke();
                    readyToHover = true;
                }
            }
            else
            {
                UndoHovering();
            }
        }
        else
        {
            UndoHovering();
        }    
    }

    private void UndoHovering()
    {
        if (justHovered)
        {
            justHovered = false;
            readyToHover = false;
            eventsWhenNotHovering.Invoke();
            activateObject.SetActive(false);
        }
    }
}
