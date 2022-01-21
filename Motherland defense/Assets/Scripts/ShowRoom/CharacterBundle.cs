using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Bundle", menuName = "Character Bundle", order = 12)]
public class CharacterBundle : ScriptableObject
{
    [SerializeField] private List<CharacterData> _characters;

    public List<CharacterData> Characters => _characters;
}
