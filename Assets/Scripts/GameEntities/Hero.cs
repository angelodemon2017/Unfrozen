using System;

public class Hero
{
    public Action Changed;
    public int id;
    public int heroScores;
    public bool isUnlocked;
    public bool isSelect;

    public Hero(HeroData heroData)
    {
        id = heroData.id;
        heroScores = 0;
        isUnlocked = id == 0;
        isSelect = false;
    }

    public void UpdateSelect(bool select)
    {
        isSelect = select;
        Changed?.Invoke();
    }

    public void AddScore(int scores)
    {
        heroScores += scores;
        Changed?.Invoke();
    }
}