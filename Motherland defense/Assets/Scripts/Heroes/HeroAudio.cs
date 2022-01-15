using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAudio : MonoBehaviour
{
    private AudioClip _hitSound;
    private AudioClip _attackSound;
    private AudioClip _dieSound;
    private AudioSource _source;
    private Hero _hero;

    public void Initialise(AudioClip hitSound, AudioClip attackSound, AudioClip dieSound)
    {
        _hitSound = hitSound;
        _attackSound = attackSound;
        _dieSound = dieSound;
    }

    private void Start()
    {
        _hero = GetComponent<Hero>();
        _source = GetComponent<AudioSource>();
        GetComponent<HeroFight>().OnHitEnemy += PlayAttackSound;
        _hero.OnDamageTaken += PlayHitSound;
        _hero.OnDied += PlayDeathSound;
    }

    private void PlayHitSound(int health, int damage)
    {
        _source.PlayOneShot(_hitSound);
    }

    private void PlayDeathSound()
    {
        _source.PlayOneShot(_dieSound);
    }

    private void PlayAttackSound(Enemy enemy)
    {
        _source.PlayOneShot(_attackSound);
    }
}
