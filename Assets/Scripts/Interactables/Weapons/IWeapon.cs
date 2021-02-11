using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface IWeapon
{
    public float GunRange { get; set; }
    public float BulletTrailSize { get; set; }
    public float RateOfFire { get; set; }
    public float TimeBeforeNextShot { get; set; }
    public GameObject GunTip { get; set; }
    public LineRenderer BulletTrailPrefab { get; set; }
    public ParticleSystem MuzzleFlashParticles { get; set; }
    public ParticleSystem CartridgeEjectionParticles { get; set; }
    public LayerMask GunHitLayers { get; set; }
    public AudioSource AudioSource { get; set; }
    public XRSocketInteractor xRSocketInteractor { get; set; }

    void Shoot();
    void DropClip();

    void InsertAmmo();
}
