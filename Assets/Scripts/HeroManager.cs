using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeroManager : MonoBehaviour
{
    public static HeroManager Instance;

    public HeroConfig heroConfig;

    [SerializeField] public Transform parentHeroes;
    [SerializeField] private PanelHero panelHeroPrefab;
    private List<PanelHero> panelHeroes = new();
    private List<Hero> Heroes = new();

    private int _selectedHero = 0;
    public int selectedHero => _selectedHero;

    private void Awake()
    {
        Instance = this;
        InitHeroes();
    }

    public void InitHeroes()
    {
        foreach (var hd in heroConfig.Heroes)
        {
            var hero = new Hero(hd);
            Heroes.Add(hero);
            var hp = Instantiate(panelHeroPrefab, parentHeroes);
            hp.Init(hd, hero);
            panelHeroes.Add(hp);
            hp.gameObject.SetActive(hero.isUnlocked);
        }
        parentHeroes.gameObject.SetActive(false);
        SelectHero(0);
    }

    public void UnlockHero(int heroId)
    {
        if (Heroes.Any(h => h.id == heroId))
        {
            Heroes.FirstOrDefault(h => h.id == heroId).isUnlocked = true;
        }
        panelHeroes.ForEach(ph => ph.CheckUnlock());
    }

    public void AddScoreToHero(int heroId, int scores)
    {
        Heroes.FirstOrDefault(h => h.id == heroId).AddScore(scores);
    }

    public void SelectHero(int heroId)
    {
        foreach (var h in Heroes)
        {
            h.UpdateSelect(h.id == heroId);
        }
        _selectedHero = heroId;
    }
}