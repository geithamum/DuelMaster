using UnityEngine;
using EzySlice;

public class Sword : MonoBehaviour
{
    public Transform startSlicePoint;          // The start point of the slice
    public Transform endSlicePoint;            // The end point of the slice
    public LayerMask sliceableLayer;           // Layer mask for sliceable objects
    public VelocityEstimator velocityEstimator; // Reference to VelocityEstimator
    public float cutForce = 200f;              // Force applied during the slice
    public Material crossSectionMaterial;      // Material for sliced surfaces

    private void FixedUpdate()
    {
        // Check if the sword intersects with any sliceable objects
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        // Get the velocity estimate using the VelocityEstimator component
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();

        // Calculate the slicing plane normal using the sword's velocity and direction
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        // Slice the target using the computed normal
        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }
    }

    private void SetupSlicedComponent(GameObject sliceObject)
    {
        // Add Rigidbody and MeshCollider to the sliced object
        Rigidbody rb = sliceObject.AddComponent<Rigidbody>();
        MeshCollider collider = sliceObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, sliceObject.transform.position, 1);
    }
}
