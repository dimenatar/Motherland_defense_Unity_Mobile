using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioClip _hitSound;
    private AudioClip _attackSound;
    private AudioClip _dieSound;
    private AudioSource _source;
    private Enemy _enemy;

    public void Initialise(AudioClip hitSound, AudioClip attackSound, AudioClip dieSound)
    {
        _hitSound = hitSound;
        _attackSound = attackSound;
        _dieSound = dieSound;
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _source = GetComponent<AudioSource>();
        GetComponent<EnemyFight>().OnHitOpponent += PlayAttackSound;
        _enemy.OnDamageTaken += PlayHitSound;
        _enemy.OnDied += PlayDeathSound;
    }

    private void PlayHitSound(int health, int damage)
    {
        _source.PlayOneShot(_hitSound);
    }

    private void PlayDeathSound()
    {
        _source.PlayOneShot(_dieSound);
    }

    private void PlayAttackSound(Hero hero)
    {
        _source.PlayOneShot(_attackSound);
    }
}
