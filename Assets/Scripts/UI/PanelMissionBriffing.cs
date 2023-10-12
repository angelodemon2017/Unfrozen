using UnityEngine;
using UnityEngine.UI;

public class PanelMissionBriffing : MonoBehaviour
{
    [SerializeField] private Button buttonClose;
    [SerializeField] private Text textTitle;
    [SerializeField] private Image imageIcon;
    [SerializeField] private Text textDescription;
    [SerializeField] private Button buttonStartMission;

    private MissionData _missionId;

    private void Awake()
    {
        buttonStartMission.onClick.AddListener(StartMission);
        buttonClose.onClick.AddListener(ClosePanel);
    }

    public void Show(MissionData missionData)
    {
        gameObject.SetActive(true);
        _missionId = missionData;
        textTitle.text = missionData.missionName;
        textDescription.text = missionData.missionTextBefore;
    }

    private void StartMission()
    {
        MissionManager.Instance.MissionComplete(_missionId);
        ClosePanel();
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
        HeroManager.Instance.parentHeroes.gameObject.SetActive(false);
    }
}