using UnityEngine;

public class BlockClickPanel : MonoBehaviour
{
    private void OnEnable()
    {
        GameLogic.Instance.ClickEnable = false;
    }

    private void OnDisable()
    {
        GameLogic.Instance.ClickEnable = true;
    }
}
