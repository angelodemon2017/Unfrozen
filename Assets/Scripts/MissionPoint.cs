using UnityEngine;
using UnityEngine.UI;

public class MissionPoint : MonoBehaviour
{
    [SerializeField] private LineDrawer lineDrawerPrefab;
    [SerializeField] private Text IdMission;
    private SpriteRenderer spriteRenderer;
    private Mission _mission;
    public string id => _mission.Id;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(MissionData data, Mission mission)
    {
        _mission = mission;
        IdMission.text = data.missionId;
        transform.position = new Vector3(data.missionPosition.x, data.missionPosition.y, 0f);
        _mission.Changed += UpdateState;
        UpdateState();
    }

    public void UpdateState()
    {
        switch (_mission.State)
        {
            case MissionState.Active:
                gameObject.SetActive(true);
                spriteRenderer.color = Color.white;
                break;
            case MissionState.Lock:
                gameObject.SetActive(false);
                spriteRenderer.color = Color.magenta;
                break;
            case MissionState.ActiveLock:
                gameObject.SetActive(true);
                spriteRenderer.color = Color.gray;
                break;
            case MissionState.Complete:
                spriteRenderer.color = Color.blue;
                break;
        }
    }

    public void ShowPoint()
    {
        foreach (var mid in _mission.IdsComplete())
        {
            var pos = MissionManager.Instance.GetMissionPointById(mid).transform.position;
            Instantiate(lineDrawerPrefab, transform.position, Quaternion.identity, transform).Init(pos);
        }
    }

    private void OnMouseDown()
    { 
        if (_mission.State == MissionState.Active)
        {
            MissionManager.Instance.SelectMission(_mission.Id);
        }
    }

    private void OnMouseEnter()
    {
        if (_mission.State == MissionState.Active)
        {
            spriteRenderer.color = Color.green;
        }
    }

    private void OnMouseExit()
    {
        if (_mission.State == MissionState.Active)
        {
            spriteRenderer.color = Color.white;
        }
    }
}