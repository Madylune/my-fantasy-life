using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
  private static Player instance;

  public static Player MyInstance
  {
    get
    {
      if (instance == null)
      {
        instance = FindObjectOfType<Player>();
      }
      return instance;
    }
  }

  public PlayerMovement movement;
  public PlayerAttack attack;
  public PlayerHealth health;

  private void Start() 
  {
    movement = GetComponent<PlayerMovement>();
    attack = GetComponent<PlayerAttack>();
    health = GetComponent<PlayerHealth>();
  }
}
