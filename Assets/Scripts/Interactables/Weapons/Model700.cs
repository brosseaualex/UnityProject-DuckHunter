using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model700 : MonoBehaviour, IWeapon
{
    [field: SerializeField] public float GunRange { get; set; } = 50f;
    public GameObject GunTip { get; set; }
    [field: SerializeField] public LineRenderer BulletTrailPrefab { get; set; }
    [field: SerializeField] public ParticleSystem MuzzleFlashParticles { get; set; }
    [field: SerializeField] public ParticleSystem CartridgeEjectionParticles { get; set; }
    [field: SerializeField] public LayerMask GunHitLayers { get; set; }

    void Start()
    {
        GunTip = GameObject.Find("Model700GunTip");
    }

    public void Shoot()
    {
        Ray ray = new Ray(GunTip.transform.position, GunTip.transform.TransformDirection(Vector3.forward));
        RaycastHit raycastHit;
        Vector3 lineRendererEnd;

        if (Physics.Raycast(ray, out raycastHit, GunRange, GunHitLayers.value))
        {
            lineRendererEnd = raycastHit.point;

            if (XRInputDebugger.Instance.inputDebugEnabled)
            {
                string debugMessage = "Raycast on: " + raycastHit.transform.name;
                Debug.Log(debugMessage);
                XRInputDebugger.Instance.DebugLogInGame(debugMessage);
            }
        }
        else
        {
            lineRendererEnd = ray.origin + ray.direction * GunRange;
        }

        LineRenderer bulletTrailClone = Instantiate(BulletTrailPrefab);
        bulletTrailClone.SetPositions(new Vector3[] { GunTip.transform.position, lineRendererEnd });

        StartCoroutine(LineRendererFade.Instance.FadeLineRenderer(bulletTrailClone));
    }

    public void Reload()
    {
        if (XRInputDebugger.Instance.inputDebugEnabled)
        {
            string debugMessage = name + " Reload";
            Debug.Log(debugMessage);
            XRInputDebugger.Instance.DebugLogInGame(debugMessage);
        }
    }
}