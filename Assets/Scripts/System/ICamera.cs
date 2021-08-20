using UnityEngine;

public interface ICamera
{
    void SetOffset();
    void FollowTarget();
    void SetTarget(Transform _transform);
}