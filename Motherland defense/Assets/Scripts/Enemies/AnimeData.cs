using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Anime Data", menuName = "Anime Data", order = 41)]
public class AnimeData : CharacterData
{
    [SerializeField] private float _zombieSpawnDelay;
    [SerializeField] private CharacterData _zombieData;

    public float ZombieSpawnDelay => _zombieSpawnDelay;
    public CharacterData ZombieData => _zombieData;
}
