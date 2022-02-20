using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "ScriptableObjects/ShipData", order = 1)]
public sealed class ShipData : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _turnSpeed = 4f;
    [SerializeField] private int _hp = 100;
    [SerializeField] private int _armor = 100;
    [SerializeField] private LayerMask _shootingLayer;
    [SerializeField] private GameObject _shipPrefab;

    public float MoveSpeed => _moveSpeed;
    public float TurnSpeed => _turnSpeed;
    public int HP => _hp;
    public int Armor => _armor;
    public LayerMask ShootingLayer => _shootingLayer;
    public GameObject ShipPrefab => _shipPrefab;
}