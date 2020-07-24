using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float speed = .6f;
    public float rotateTarget = 0f;
    public LayerMask mask;
    public Swipe swipe;
    // Update is called once per frame
    void Update()
    {
        rotateTarget %= 360;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || swipe.SwipeLeft)
            if (RotateOpenCheck(-transform.right))
                rotateTarget -= 90f;

        if (Input.GetKeyDown(KeyCode.RightArrow) || swipe.SwipeRight)
            if (RotateOpenCheck(transform.right))
                rotateTarget += 90f;

        if (Input.GetKeyDown(KeyCode.DownArrow) || swipe.SwipeDown)
            if (RotateOpenCheck(-transform.forward))
                rotateTarget += 180f;

        if (swipe.SwipeLeft)
            Debug.Log("Swipe Left!");
            

    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.Euler(transform.rotation.x, rotateTarget, transform.rotation.z),
            speed);
    }

    bool RotateOpenCheck(Vector3 Direction)
    {
        Ray ray = new Ray(transform.position, Direction);
        RaycastHit hitInfu;
        if (Physics.Raycast(ray, out hitInfu, 100f, mask, QueryTriggerInteraction.Ignore))
            Debug.DrawLine(ray.origin, hitInfu.point, Color.yellow);
        if (hitInfu.distance <= 3f)
            return false;
        return true;
    }
}
