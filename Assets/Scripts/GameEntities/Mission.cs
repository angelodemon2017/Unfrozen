using System;
using System.Collections.Generic;
using System.Linq;

public class Mission
{
    public Action Changed;
    public string Id;
    public MissionState State;
    public List<RequiredMission> requiredMissions => MissionManager.Instance.missionConfig.GetMissionById(Id).VariantRequireds;
    public List<string> BlockedMission => MissionManager.Instance.missionConfig.GetMissionById(Id).temporaryBlockedMission;

    public Mission(string id)
    {
        Id = id;
        State = MissionState.Lock;
    }

    public void ChangeState(MissionState state)
    {
        State = state;
        Changed?.Invoke();
    }

    public List<string> IdsComplete()
    {
        return requiredMissions.FirstOrDefault(x => MissionManager.Instance.IsDone(x)).requiredMissions;
    }
}