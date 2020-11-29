using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneCommander : MonoBehaviour
{
   [Header("Mineral Laser")]
   [SerializeField] private GameObject mineralLaserCutsceneObject;
   [SerializeField] private UnityEvent mineralLaserEvents;
   
   [Header("Break the Surface")]
   [SerializeField] private GameObject breakTheSurfaceCutsceneObject;
   [SerializeField] private UnityEvent breakTheSurfaceEvents;
   
   [Header("Drain the Core")]
   [SerializeField] private GameObject drainTheCoreCutsceneObject;
   [SerializeField] private UnityEvent drainTheCoreEvents;
   
   [Header("So Long, and Thanks for All The Phlebotium")]
   [SerializeField] private GameObject soLongCutsceneObject;
   [SerializeField] private UnityEvent soLongEvents;
   
   public void TriggerMineralLaserCutscene()
   {
      mineralLaserEvents.Invoke();
      mineralLaserCutsceneObject.SetActive(true);
   }

   public void TriggerBreakTheSurfaceCutscene()
       {
          breakTheSurfaceEvents.Invoke();
          breakTheSurfaceCutsceneObject.SetActive(true);
       }
   
   public void TriggerDrainTheCoreCutscene()
   {
      drainTheCoreEvents.Invoke();
      drainTheCoreCutsceneObject.SetActive(true);
   }

   public void TriggerSoLong()
   {
      soLongEvents.Invoke();
      soLongCutsceneObject.SetActive(true);
   }
}
