using UnityEngine;
using System.Collections;

public class FireballController : MonoBehaviour {

    public GameObject player;

    public GameObject Fireball;
    public ParticleSystem explosion;
    public ParticleSystem fireballTail;
    public ParticleSystem fireballProjectiles;
    public ParticleSystem ProjectileExplosionParticleSystem;
    public GameObject ProjectileColliderObject;

    private ParticleSystem[] ManualParticleSystem;
    private ParticleSystem[] ProjectileDestroyParticleSystemsOnCollision;

    private GameObject copy;
	// Use this for initialization
	void Start () {
        Fireball.gameObject.SetActive(false);
        //ManualParticleSystem[0] = explosion;
      //  ProjectileDestroyParticleSystemsOnCollision[0] = fireballTail;
       // ProjectileDestroyParticleSystemsOnCollision[1] = fireballProjectiles;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.J))
        {
            copy = (GameObject)Instantiate(Fireball, player.transform, false);
            copy.transform.forward += copy.transform.forward;
            //copy.gameObject.SetActive(false);
            /*
            copy.AddComponent<DigitalRuby.PyroParticles.FireProjectileScript>();
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().StartTime = 0.25f;
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().StopTime = 1f;
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().Duration = 3f;
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().IsProjectile = true;
            //copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ManualParticleSystems.Initialize();
            //copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ManualParticleSystems[0] = explosion;
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ManualParticleSystems = ManualParticleSystem;
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ProjectileColliderObject = ProjectileColliderObject;
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ProjectileExplosionParticleSystem = ProjectileExplosionParticleSystem;
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ProjectileExplosionRadius = 10;
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ProjectileExplosionForce = 2000;
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ProjectileColliderSpeed = 25;
            ///copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ProjectileDestroyParticleSystemsOnCollision.Initialize();
            //copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ProjectileDestroyParticleSystemsOnCollision[0] = fireballTail;
            //copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ProjectileDestroyParticleSystemsOnCollision[1] = fireballProjectiles;
            copy.GetComponent<DigitalRuby.PyroParticles.FireProjectileScript>().ProjectileDestroyParticleSystemsOnCollision = ProjectileDestroyParticleSystemsOnCollision;
            copy.AddComponent<DigitalRuby.PyroParticles.FireLightScript>();
            copy.GetComponent<DigitalRuby.PyroParticles.FireLightScript>().IntensityModifier = 2;
            */
            copy.gameObject.SetActive(true);
        }

	}
}