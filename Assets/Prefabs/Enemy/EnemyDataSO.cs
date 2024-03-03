using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Enemy Data", order = 1)]
public class EnemyDataSO : ScriptableObject
{


    public new string name;
    public int health;
    public float movementSpeed;
    public int damage;
    public bool canShoot;
    public bool canMove;
    public bool rotating;


}
