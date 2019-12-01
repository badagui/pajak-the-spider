using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private AudioEvent shootChargeSound;

    [SerializeField]
    private AudioEvent shootGoSound;

    [SerializeField]
    private Transform targetRotor;

    [SerializeField]
    GameObject projectilePrefab;

    public bool IsChargingShoot { get; private set; }

    private PlayerAudio playerAudio;

    public float ShootSpeedCurrent { get; private set; }
    public float ShootSpeedMax { get; private set; }
    private float shootSpeedChargeRate = 30f;

    private void Awake()
    {
        ShootSpeedMax = 36f;
        playerAudio = GetComponent<PlayerAudio>();
    }

    private void Update()
    {
        if (IsChargingShoot)
        {
            ShootSpeedCurrent += shootSpeedChargeRate * Time.deltaTime;

            if (ShootSpeedCurrent >= ShootSpeedMax)
            {
                DoChargedShoot();
            }
        }
    }

    public void DoChargedShoot()
    {
        if (IsChargingShoot)
        {
            playerAudio.PlaySimpleAudio(shootGoSound);
            var projectile = Instantiate(projectilePrefab, transform);
            var constantVelocity = projectile.GetComponent<ConstantVelocity>();
            if (constantVelocity)
            {
                Vector2 projectileVelocity = targetRotor.right * ShootSpeedCurrent;
                constantVelocity.SetVelocity(projectileVelocity.x, projectileVelocity.y);
            }
        }

        StopChargingShoot();
    }

    public void StartChargingShoot()
    {
        IsChargingShoot = true;
        playerAudio.PlayProgressiveAudio(shootChargeSound, ShootSpeedMax / shootSpeedChargeRate);
    }

    public void StopChargingShoot()
    {
        playerAudio.StopProgressiveAudio();
        ShootSpeedCurrent = 0f;
        IsChargingShoot = false;
    }
}
