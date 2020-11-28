﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneCommander : MonoBehaviour
{
   [SerializeField] private GameObject mineralLaserCutsceneObject;
   [SerializeField] private GameObject breakTheSurfaceCutsceneObject;
   [SerializeField] private UnityEvent breakTheSurfaceEvents;
   
   public void TriggerMineralLaserCutscene()
   {
      mineralLaserCutsceneObject.SetActive(true);
   }

   public void TriggerBreakTheSurfaceCutscene()
   {
      breakTheSurfaceEvents.Invoke();
      breakTheSurfaceCutsceneObject.SetActive(true);
   }
}
