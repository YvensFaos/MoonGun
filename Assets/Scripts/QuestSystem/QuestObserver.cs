using UnityEngine;

public class QuestObserver : MonoBehaviour
{
    private QuestInfo _currentQuestInfo;
    private int _countDownAsteroid;
    private bool _hasActiveQuest;

    [Header("Quests")] 
    [SerializeField] private QuestList questList;

    [Header("Effects")] 
    [SerializeField] private AudioClip questSuccessSound;
    
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
            if (!_currentQuestInfo.complexQuest && (_currentQuestInfo.asteroidType == type || _currentQuestInfo.asteroidType == AsteroidType.ANY_TYPE))
            {
                --_countDownAsteroid;
                if (_countDownAsteroid == 0)
                {
                    
                    _hasActiveQuest = false;
                    GameLogic.Instance.QuestCompleted(_currentQuestInfo);
                    GameLogic.Instance.PlayUISound(questSuccessSound);
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
