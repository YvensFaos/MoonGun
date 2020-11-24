using System;

[Serializable]
public struct QuestInfo : IComparable
{
    public int questNumber;
    public string name;
    public string description;
    public int mineralReward;
    public int asteroidReward;

    public int asteroidsToDestroy;
    public bool complexQuest;
    public int nextQuestToUnlock;

    public AsteroidType asteroidType;
    
    public int CompareTo(object obj)
    {
        var questInfo = (QuestInfo) obj;
        return questNumber.CompareTo(questInfo.questNumber);
    }
}
