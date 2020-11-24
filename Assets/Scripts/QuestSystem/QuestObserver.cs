﻿using UnityEngine;

public class QuestObserver : MonoBehaviour
{
    private QuestInfo _currentQuestInfo;
    private int _countDownAsteroid;
    private bool _hasActiveQuest;

    [Header("Quests")] 
    [SerializeField] private QuestList questList;
    
    public void ActivateQuest(QuestInfo questInfo)
    {
        _currentQuestInfo = questInfo;
        _hasActiveQuest = true;

        _countDownAsteroid = questInfo.asteroidsToDestroy;
    }

    public void NotifyAsteroidDestroyed(AsteroidType type)
    {
        if (_hasActiveQuest)
        {
            if (!_currentQuestInfo.complexQuest)
            {
                if ((_currentQuestInfo.asteroidType == type || _currentQuestInfo.asteroidType == AsteroidType.ANY_TYPE) && _countDownAsteroid >= 0)
                {
                    --_countDownAsteroid;
                    _hasActiveQuest = false;
                    GameLogic.Instance.QuestCompleted(_currentQuestInfo);
                    UnlockNextQuest(_currentQuestInfo);
                }
            }
        }
    }

    private void UnlockNextQuest(QuestInfo currentQuest)
    {
        if (currentQuest.nextQuestToUnlock != 0)
        {
            var unlockQuest = questList.quests.Find(quest => quest.questNumber == currentQuest.nextQuestToUnlock);
            GameLogic.Instance.LabsControl.AddQuestToList(unlockQuest);
        }    
    }
}
