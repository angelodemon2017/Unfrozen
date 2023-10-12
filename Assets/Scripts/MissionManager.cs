using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    [SerializeField] private PanelMissionBriffing panelMissionBriffing;
    [SerializeField] private PanelEndMission panelEndMission;
    [SerializeField] private MissionPoint missionPointPrefab;
    [SerializeField] private Transform missionPointsParent;
    public MissionConfig missionConfig;
    private List<Mission> Missions = new();
    private List<MissionPoint> missionPoints = new();

    private string selectedMission = string.Empty;

    private void Awake()
    {
        Instance = this;
        InitMissions();
    }

    private void InitMissions()
    {
        foreach (var md in missionConfig.missions)
        {
            var m = new Mission(md.missionId);
            Missions.Add(m);
            var mp = Instantiate(missionPointPrefab, missionPointsParent);
            mp.Init(md, m);
            missionPoints.Add(mp);
        }
        UpdateStateMission(Missions[0].Id, MissionState.Active);
        panelMissionBriffing.gameObject.SetActive(false);
        panelEndMission.gameObject.SetActive(false);
    }

    public void SelectMission(string idMission)
    {
        selectedMission = idMission;

        if (idMission != string.Empty)
        {
            panelMissionBriffing.Show(missionConfig.GetMissionById(idMission));
            HeroManager.Instance.parentHeroes.gameObject.SetActive(true);
        }
    }

    public void MissionComplete(MissionData missionData)
    {
        if (Missions.Any(m => m.Id == missionData.missionId))
        {
            UpdateStateMission(missionData.missionId, MissionState.Complete);
            ShowSummaryMission(missionData);
            CalcRewards(missionData);
            missionData.unlockHeroIDs.ForEach(x => HeroManager.Instance.UnlockHero(x));
            missionData.blockedByComplete.ForEach(x => UpdateStateMission(x, MissionState.ActiveLock));
        }
    }

    private void CalcRewards(MissionData missionData)
    {
        foreach (var r in missionData.scoreDistributions)
        {
            HeroManager.Instance.AddScoreToHero(r.HeroId == -1 ?
                HeroManager.Instance.selectedHero : r.HeroId,
                r.scoreChange);
        }
    }

    private void UpdateStateMission(string id, MissionState state)
    {
        var mis = Missions.FirstOrDefault(m => m.Id == id);
        mis.ChangeState(state);
        switch (state)
        {
            case MissionState.Active:
                foreach (var m in Missions.Where(x => mis.BlockedMission.Contains(x.Id)))
                {
                    if (m.State == MissionState.Active)
                    {
                        UpdateStateMission(m.Id, MissionState.ActiveLock);
                    }
                }
                if (Missions.Any(m => m.BlockedMission.Contains(id) && m.State == MissionState.Active))
                {
                    UpdateStateMission(id, MissionState.ActiveLock);
                }
                break;
            case MissionState.Complete:
                foreach (var m in Missions.Where(x => mis.BlockedMission.Contains(x.Id) && x.State == MissionState.ActiveLock))
                {
                    UpdateStateMission(m.Id, MissionState.Active);
                }
                foreach (var m in Missions.Where(x => x.requiredMissions.Any(y => y.requiredMissions.Contains(id))))
                {
                    foreach (var lr in m.requiredMissions)
                    {
                        if (IsDone(lr))
                        {
                            UpdateStateMission(m.Id, MissionState.Active);
                            missionPoints.FirstOrDefault(p => p.id == m.Id).ShowPoint();
                            break;
                        }
                    }
                }
                break;
        }
    }

    public bool IsDone(RequiredMission requiredMission)
    {
        return Missions.Where(m => requiredMission.requiredMissions.Contains(m.Id))
            .All(m => m.State == MissionState.Complete);
    }

    private void ShowSummaryMission(MissionData missionData)
    {
        panelEndMission.Show(missionData);
    }

    public MissionPoint GetMissionPointById(string id)
    {
        return missionPoints.FirstOrDefault(m => m.id == id);
    }
}