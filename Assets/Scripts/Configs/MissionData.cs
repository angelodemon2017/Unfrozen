using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionData
{
    public string missionId;
    public string missionName;
    public Sprite missionIcon;
    public List<RequiredMission> VariantRequireds;
    public List<string> temporaryBlockedMission;
    public List<string> blockedByComplete;
    public Vector2 missionPosition;
    public string missionTextBefore;
    public string missionText;
    public List<Fraction> playerFactions;
    public List<Fraction> enemyFactions;
    public List<int> unlockHeroIDs;
    public List<ScoreDistribution> scoreDistributions;
}