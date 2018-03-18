 using UnityEngine;
 using System.Collections;
 
 public class ParticlesAutoDestroy : MonoBehaviour {
     private ParticleSystem ps;
 
     // Use this for initialization
     void Awake () {
         ps = GetComponent<ParticleSystem>();
    }
    void Update(){
        if (!ps.IsAlive())
            Destroy(gameObject);
    }
 }