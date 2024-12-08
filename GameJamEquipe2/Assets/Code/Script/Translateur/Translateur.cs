using UnityEngine;
using System.Collections;
public class Translateur : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject objectToThrow;


    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;

    private GameObject projectile;

    public void CreateTranslateur()
    {
        if (projectile == null)
        {
            projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

            Rigidbody projectilRb = projectile.GetComponent<Rigidbody>();

            Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

            projectilRb.AddForce(forceToAdd, ForceMode.Impulse);

            StartCoroutine(HandleProjectileLifetime(projectile));

        }
    }

    private IEnumerator HandleProjectileLifetime(GameObject projectile)
    {
        yield return new WaitForSeconds(lifetime);

        if (projectile != null)
        {
            Vector3 lastPosition = projectile.transform.position;
            Destroy(projectile.gameObject);
            projectile = null;

            transform.position = lastPosition;
        }
    }
}