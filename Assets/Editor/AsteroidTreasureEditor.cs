using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(AsteroidTreasure))]
    public class AsteroidTreasureEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AsteroidTreasure treasurer = (AsteroidTreasure)target;
            if(GUILayout.Button("Generate Treasure"))
            {
                treasurer.GenerateTreasureFromAsteroid();
            }
        }
    }
}