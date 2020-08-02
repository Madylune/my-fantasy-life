using System;
using UnityEngine;

[Serializable]
public class Spell : IUseable, IMoveable, IDescribable
{
  [SerializeField]
  private string name;
  
  [SerializeField]
  private int damage;

  [SerializeField]
  private Sprite icon;

  [SerializeField]
  private float speed;

  [SerializeField]
  private float castTime;

  [SerializeField]
  private GameObject spellPrefab;

    [SerializeField]
    private string description;

  [SerializeField]
  private Color barColor;

  public string MyName
  {
    get 
    {
      return name;
    }
  }

  public int MyDamage
  {
    get 
    {
      return damage;
    }
  }

  public Sprite MyIcon
  {
    get 
    {
      return icon;
    }
  }

  public float MySpeed
  {
    get 
    {
      return speed;
    }
  }

  public float MyCastTime
  {
    get 
    {
      return castTime;
    }
  }

  public GameObject MySpellPrefab
  {
    get 
    {
      return spellPrefab;
    }
  }

  public Color MyBarColor
  {
    get 
    {
      return barColor;
    }
  }

    public string GetDescription()
    {
        return string.Format("<color='#FFF390'>{0}</color>\n{1}\nCast Time: {2}s\nDamage: {3}", name, description, castTime, damage);
    }

    public void Use()
    {
        Player.MyInstance.attack.CastSpell(MyName);
    }
}
