using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class RaycastActivate : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private GameObject activateObject;

    [SerializeField] private UnityEvent eventsWhenHovering;
    
    [SerializeField] private UnityEvent eventsWhenNotHovering;

    [SerializeField] private UnityEvent onClick;
    
    private Collider _collider;
    private bool _justHovered;
    private bool _readyToHover;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _justHovered = false;
        _readyToHover = false;
        
        activateObject.SetActive(false);
    }

    public void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit, 100.0f)) 
        {
            if (hit.collider.Equals(_collider) && GameLogic.Instance.ClickEnable)
            {
                _justHovered = true;
                activateObject.SetActive(true);
                if (!_readyToHover)
                {
                    eventsWhenHovering.Invoke();
                    _readyToHover = true;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    onClick.Invoke();
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
        if (_justHovered)
        {
            _justHovered = false;
            _readyToHover = false;
            eventsWhenNotHovering.Invoke();
            activateObject.SetActive(false);
        }
    }
}
