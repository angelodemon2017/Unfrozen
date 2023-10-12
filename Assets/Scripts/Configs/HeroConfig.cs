using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHeroConfigurator", menuName = "Heroes/Heroes")]
public class HeroConfig : ScriptableObject
{
    public List<HeroData> Heroes = new();

    public HeroData GetHeroById(int id)
    {
        return Heroes.FirstOrDefault(x => x.id == id);
    }
}