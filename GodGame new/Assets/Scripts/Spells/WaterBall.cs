using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    public float effectArea;
    public GameObject particlePrefab;


    private void OnCollisionEnter(Collision other) {
        // Get all objects in effectArea
        var colliders =  Physics.OverlapSphere(transform.position, effectArea);

        foreach (var collider in colliders)
        {
            // Put down fire od every fireGridCell and FireableObject that were in effectArea
            if(collider.TryGetComponent<FireGridCell>(out var fireCell))
                fireCell.PutDownFire();
            else if(collider.TryGetComponent<FireableObject>(out var fireableObject))
                fireableObject.CoolDownTemperature();
        }

        // Create particle effect
        var particle = Instantiate(particlePrefab, transform.position, particlePrefab.transform.rotation);
        particle.transform.localScale = new Vector3(particle.transform.localScale.x * effectArea, particle.transform.localScale.y * effectArea, particle.transform.localScale.z * effectArea);

        Destroy(gameObject);
    }  
}
