using System;

[Serializable]
public struct QuestInfo
{
    public int questNumber;
    public string name;
    public string description;
    public int mineralReward;
    public int asteroidReward;

    public int asteroidsToDestroy;
    public bool complexQuest;

    public QuestInfo(int questNumber, string name, string description, int mineralReward, int asteroidReward, int asteroidsToDestroy, bool complexQuest)
    {
        this.questNumber = questNumber;
        this.name = name;
        this.description = description;
        this.mineralReward = mineralReward;
        this.asteroidReward = asteroidReward;
        this.asteroidsToDestroy = asteroidsToDestroy;
        this.complexQuest = complexQuest;
    }
}
