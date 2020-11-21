    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "QuestList", menuName = "List of Quests", order = 1)]
    public class QuestList : ScriptableObject
    {
        public List<QuestInfo> quests;

        private void Awake()
        {
            quests.Sort();
        }
    }
