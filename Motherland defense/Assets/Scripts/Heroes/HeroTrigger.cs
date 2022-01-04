using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class HeroTrigger : MonoBehaviour
{
    private Hero hero;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            hero.AddNewTarget(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            hero.RemoveTarget(other.gameObject);
        }
    }

    private void Start()
    {
        hero = GetComponent<Hero>();
    }
}
