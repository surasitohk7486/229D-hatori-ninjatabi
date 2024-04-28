using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_NinjaM : MonoBehaviour
{
    public float HP;
    [SerializeField] private GameManager turn;
    [SerializeField] private Animator Banim, BHP;
    [SerializeField] private float throwForce = 10f; // แรงในการโยน
    [SerializeField] private GameObject objectToThrow; // อ็อบเจกต์ที่จะโยน
    [SerializeField] private Transform throwPoint; // จุดที่โยนจะเริ่ม
    private const float maxThrowForce = 100f;
    private const float minThrowForce = 10f;
    public bool Throw = false , isChargingThrow = false;
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.C)) // ตรวจสอบว่าผู้เล่นคลิกเมาส์ซ้าย
            {
                isChargingThrow = true;
            }
            if (isChargingThrow)
            {
                ChargeThrow();
            }
            if (Input.GetKeyUp(KeyCode.C))
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
            Banim.Play("B_Throw");
            
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
        if (collision.gameObject.CompareTag("KunaiFM"))
        {
            // Calculate damage (you can adjust this calculation as needed)
            float damage = 1f;

            // Reduce the player's HP by the damage amount
            HP -= damage;

            // Check if the player's HP is zero or below
            if (HP <= 0)
            {
                BHP.SetBool("IsDie", true); // Trigger death animation
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
