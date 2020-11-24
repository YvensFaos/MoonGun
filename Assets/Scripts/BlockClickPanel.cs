using System;
using UnityEngine;

public class BlockClickPanel : MonoBehaviour
{
    private void Start()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        GameLogic.Instance.ClickEnable = false;
    }

    private void OnDisable()
    {
        GameLogic.Instance.ClickEnable = true;
    }
}
