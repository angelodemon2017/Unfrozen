using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "MissionList", menuName = "Missions/Mission List")]
public class MissionConfig : ScriptableObject
{
    //Можно доработать кастомной отрисовкой для более удобного заполнения

    public List<MissionData> missions;

    public MissionData GetMissionById(string id)
    {
        return missions.FirstOrDefault(m => m.missionId == id);
    }
}