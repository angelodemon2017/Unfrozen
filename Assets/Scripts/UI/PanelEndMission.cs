using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEndMission : MonoBehaviour
{
    [SerializeField] private Text textPrefab;

    [SerializeField] private Transform parentPlayerFractions;
    [SerializeField] private Transform parentEnemyFractions;
    [SerializeField] private Text textTitle;
    [SerializeField] private Image imageIcon;
    [SerializeField] private Text textDescription;
    [SerializeField] private Button buttonMissionComplete;
    private Dictionary<Fraction, string> fractions = new Dictionary<Fraction, string>()
    {
        { Fraction.None, string.Empty },
        { Fraction.Eagles, "орлы" },
        { Fraction.Jackdaw, "галки" },
        { Fraction.Seagulls, "чайки" },
        { Fraction.Magpies, "сороки" },
        { Fraction.Jays, "сойки" },
        { Fraction.Sparrows, "воробьи" },
        { Fraction.Owls, "совы" },
        { Fraction.Crows, "вороны" },
        { Fraction.Phoenixes, "фениксы" },
    };

    private void Awake()
    {
        buttonMissionComplete.onClick.AddListener(MissionComplete);
    }

    public void Show(MissionData missionData)
    {
        gameObject.SetActive(true);
        textTitle.text = missionData.missionName;
        imageIcon.sprite = missionData.missionIcon;
        textDescription.text = missionData.missionText;

        parentPlayerFractions.DestroyChilds();
        parentEnemyFractions.DestroyChilds();
        foreach (var fract in missionData.playerFactions)
        {
            Instantiate(textPrefab, parentPlayerFractions).text = fractions[fract];
        }
        foreach (var fract in missionData.enemyFactions)
        {
            Instantiate(textPrefab, parentEnemyFractions).text = fractions[fract];
        }
    }

    private void MissionComplete()
    {
        gameObject.SetActive(false);
    }
}