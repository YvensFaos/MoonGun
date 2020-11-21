using UnityEngine;

public class QuestObserver : MonoBehaviour
{
    private QuestInfo _currentQuestInfo;
    private int _countDownAsteroid;
    private bool _hasActiveQuest = false;
    
    public void ActivateQuest(QuestInfo questInfo)
    {
        _currentQuestInfo = questInfo;
        _hasActiveQuest = true;

        _countDownAsteroid = questInfo.asteroidsToDestroy;
    }

    public void NotifyAsteroidDestroyed()
    {
        if (_hasActiveQuest)
        {
            if (!_currentQuestInfo.complexQuest)
            {
                if (--_countDownAsteroid <= 0)
                {
                    _hasActiveQuest = false;
                    GameLogic.Instance.QuestCompleted(_currentQuestInfo);
                }
            }
        }
    }
}
