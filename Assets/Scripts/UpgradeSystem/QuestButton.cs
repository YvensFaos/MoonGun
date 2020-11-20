using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    [SerializeField] private Text asteroidReward;
    [SerializeField] private Text mineralReward;
    [SerializeField] private Text questName;
    [SerializeField] private Text questDescription;
   
    private QuestInfo _questInfo;
   
    public void Initialize(QuestInfo questInfo)
    {
        _questInfo = questInfo;
        asteroidReward.text = "+" + questInfo.asteroidReward;
        mineralReward.text = "+" +  questInfo.mineralReward;
        questName.text = questInfo.name;
        questDescription.text = questInfo.description;
    }

    public void ClickMe()
    {
        GameLogic.Instance.LabsControl.TakeQuest(_questInfo);
    }
}
