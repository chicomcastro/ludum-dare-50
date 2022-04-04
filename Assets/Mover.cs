using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 5f;
    public float destroyDelay = 0.1f;

    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = speed * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Scenario" || other.tag == "Player" || other.tag == "Enemy")
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
