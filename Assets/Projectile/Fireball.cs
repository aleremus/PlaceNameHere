using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField, Range(1, 10)] float fireballLifeTime = 3;
    [SerializeField, Range(10, 100)] float fireballSpeed = 20;
    [SerializeField, Range(1, 100)] int damage;

    private void Start()
    {
        StartCoroutine("SelfDestruct");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * fireballSpeed * Time.deltaTime);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(fireballLifeTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Magic"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().DealDamage(damage);
        }
    }
}
