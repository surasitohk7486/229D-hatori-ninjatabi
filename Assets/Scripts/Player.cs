using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public float HP;
    [SerializeField] private GameManager turn;
    [SerializeField] private Animator Ganim, GHP;
    [SerializeField] private float throwForce = 0f; // Initial throw force (can be changed based on gameplay needs)
    [SerializeField] private GameObject objectToThrow; // The projectile object to throw
    [SerializeField] private Transform throwPoint; // The starting point for throwing
    private const float maxThrowForce = 100f;
    private const float minThrowForce = 10f;
    public bool Throw = false, isChargingThrow = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // Check if the player presses the "Z" key
        {
            isChargingThrow = true;
        }
        if (isChargingThrow)
        {
            ChargeThrow();
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Throw = true;
            ThrowObject();
            IsThrowing();
        }
    }

    private void IsThrowing()
    {
        if (Throw)
        {
            Ganim.Play("G_Throw");
        }
    }

    private void ThrowObject()
    {
        isChargingThrow = false;
        // Calculate the target point (e.g., mouse position in world coordinates)
        Vector2 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the initial projectile velocity using the CalculateProjectile function
        Vector2 projectileVelocity = CalculateProjectile(throwPoint.position, targetPoint, time: 1.0f); // You can adjust the time parameter as needed

        // Instantiate the projectile at the throw point
        GameObject thrownObject = Instantiate(objectToThrow, throwPoint.position, throwPoint.rotation);

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = thrownObject.GetComponent<Rigidbody2D>();

        // If the Rigidbody2D component is found, set the velocity
        if (rb != null)
        {
            rb.velocity = projectileVelocity; // Set the initial velocity
        }

        // Reset throw force
        throwForce = 0f;
    }

    private void ChargeThrow()
    {
        throwForce += Time.deltaTime * 20f; // Increment throw force while charging
        throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce); // Clamp throw force within bounds
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is a projectile (e.g., Kunai)
        if (collision.gameObject.CompareTag("Kunai"))
        {
            // Calculate damage (you can adjust this calculation as needed)
            float damage = 1f;

            // Reduce the player's HP by the damage amount
            HP -= damage;

            // Check if the player's HP is zero or below
            if (HP <= 0)
            {
                GHP.SetBool("IsDie", true); // Trigger death animation
            }
        }
        // Destroy the projectile upon collision
        Destroy(collision.gameObject);
    }

    // CalculateProjectile function implementation
    Vector2 CalculateProjectile(Vector2 origin, Vector2 targetPoint, float time)
    {
        Vector2 distance = targetPoint - origin;

        // Calculate the initial velocity in the x and y directions
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        // Create a Vector2 with the calculated velocities
        Vector2 projectileVelocity = new Vector2(velocityX, velocityY);

        return projectileVelocity;
    }
}
