using UnityEngine;
using UnityEngine.UI;

public class PanelHero : MonoBehaviour
{
    [SerializeField] private Button buttonSelect;
    [SerializeField] private Image backGroundForSelect;
    [SerializeField] private Image icon;
    [SerializeField] private Text textName;
    [SerializeField] private Text textScores;

    private Hero _hero;

    public void Init(HeroData heroData, Hero hero)
    {
        buttonSelect.onClick.AddListener(SelectThisHero);
        _hero = hero;
        icon.sprite = heroData.Icon;
        textName.text = heroData.heroName;
        _hero.Changed += UpdateUI;
        UpdateUI();
    }

    public void CheckUnlock()
    {
        gameObject.SetActive(_hero.isUnlocked);
    }

    private void UpdateUI()
    {
        textScores.text = $"{_hero.heroScores}";
        backGroundForSelect.color = _hero.isSelect ? Color.green : Color.white;
    }

    private void SelectThisHero()
    {
        HeroManager.Instance.SelectHero(_hero.id);
    }
}