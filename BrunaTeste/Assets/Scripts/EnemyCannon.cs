using UnityEngine;

public class EnemyCannon : MonoBehaviour
{
    [SerializeField]
    Transform transformSpawnBall;
    [SerializeField]
    GameObject enemyCannonBall;
    [SerializeField]
    float fireRate;
    [SerializeField]
    GameObject cannonEffect;

    Animator recoilAnimator;
    float nextFireTime;

    private void Start()
    {
        recoilAnimator = GetComponent<Animator>();
    }

    public void EnemyShoot()
    {
        if (nextFireTime < Time.time)
        {
            cannonEffect.GetComponent<EnemyEffectsController>().EnableEffect();
            Instantiate(enemyCannonBall, transformSpawnBall.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
            recoilAnimator.SetTrigger("Shoot");
        }
    }
}
