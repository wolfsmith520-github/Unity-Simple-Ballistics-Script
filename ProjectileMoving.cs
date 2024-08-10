using UnityEngine;

public class ProjectileMoving : MonoBehaviour
{
    /*
    This script is written by me
    but the idea behind this script is from youtuber World of Zone
    Here is the tutorial he made : https://www.youtube.com/watch?v=d7pwmO6IS2I
    */
    public float speed = 420.0f;
    public int predictionPerFrame = 6;
    public Vector3 bulletVelocity;
    public Vector3 gravity = new Vector3(0, -9.8f, 0);

    private void Start()
    {
        bulletVelocity = transform.forward * speed;
    }
    void Update()
    {
        Vector3 point1 = this.transform.position;
        float stepSize = 1.0f / predictionPerFrame;
        for (float step = 0; step < 1; step += stepSize)
        {
            bulletVelocity += gravity * stepSize * Time.deltaTime;
            Vector3 point2 = point1 + bulletVelocity * stepSize * Time.deltaTime;
            
            Ray ray = new Ray(point1, point2);
            if (Physics.Raycast(ray, (point2 - point1).magnitude))
            {
                Debug.Log("hit");
            }
            point1 = point2;
        }
        this.transform.position = point1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 point1 = this.transform.position;
        float stepSize = 0.01f;
        Vector3 predictedBulletVelocity = bulletVelocity;
        for (float step = 0; step < 1; step += stepSize)
        {
            predictedBulletVelocity += gravity * stepSize;
            Vector3 point2 = point1 + predictedBulletVelocity * stepSize;
            Gizmos.DrawLine(point1, point2);
            point1 = point2;
        }
    }
}
