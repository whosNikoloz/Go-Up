using UnityEngine;

public class HeadBob
{
    private Transform joint;
    private float bobSpeed;
    private Vector3 bobAmount;
    private Vector3 jointOriginalPos;
    private Rigidbody rb;
    private float timer = 0;

    public HeadBob(FirstPersonController fpc, Transform joint, float bobSpeed, Vector3 bobAmount, Rigidbody rb)
    {
        this.joint = joint;
        this.bobSpeed = bobSpeed;
        this.bobAmount = bobAmount;
        this.rb = rb;
        jointOriginalPos = joint.localPosition;
    }

    public void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;
            joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
        }
        else
        {
            timer = 0;
            joint.localPosition = Vector3.Lerp(joint.localPosition, jointOriginalPos, Time.deltaTime * bobSpeed);
        }
    }
}
