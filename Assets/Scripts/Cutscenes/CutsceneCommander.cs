using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCommander : MonoBehaviour
{
   [SerializeField] private GameObject mineralLaserCutsceneObject;

   public void TriggerMineralLaserCutscene()
   {
      mineralLaserCutsceneObject.SetActive(true);
   }
}
