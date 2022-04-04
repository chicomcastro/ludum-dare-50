using UnityEngine;

public class LifeManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            print("Game over");
        }
    }
}
