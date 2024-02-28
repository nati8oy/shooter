using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Player Data", order = 1)]
public class PlayerData : ScriptableObject
{

    public float rotationSpeed;
    public float movementSpeed;
    public int health;
    public int damage;
    public float accuracy;

}
