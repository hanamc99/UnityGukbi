using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] Transform firePoint;
    float attackRange = 5f;
    Coroutine routine;
    int ammo = 8;

    [SerializeField] ParticleSystem muzzleFireEffect;
    [SerializeField] ParticleSystem shellEjectEffect;
    [SerializeField] AudioSource gunAudioHost;
    [SerializeField] AudioClip gunShotSound;
    [SerializeField] AudioClip gunReloadSound;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    public void Reload()
    {
        if(routine != null)
        {
            return;
        }
        routine = StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        gunAudioHost.PlayOneShot(gunReloadSound);
        yield return new WaitForSeconds(0.85f);
        ammo = 8;
        routine = null;
    }

    public void Fire()
    {
        if(routine != null || ammo <= 0)
        {
            return;
        }
        routine = StartCoroutine(ShotRoutine());
        ammo -= 1;
    }

    IEnumerator ShotRoutine()
    {
        Ray ray = new Ray(firePoint.position, transform.forward);
        float lineLength = attackRange;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackRange))
        {
            lineLength = (firePoint.position - hit.point).magnitude;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, firePoint.position + transform.forward * lineLength);
        lineRenderer.enabled = true;
        muzzleFireEffect.Play();
        shellEjectEffect.Play();
        gunAudioHost.PlayOneShot(gunShotSound);
        yield return new WaitForSeconds(0.03f);
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(0.4f);
        routine = null;
    }
}
