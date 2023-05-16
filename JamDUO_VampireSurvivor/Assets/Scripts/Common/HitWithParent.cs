using UnityEngine;


/// <summary>
/// Put it on a sub collider of a gameobject
/// to redirect the collision event to the parent
/// This allows to have different colliders with different layers on a parent gameobject
/// CAUTION : you have to detect manually if the event occurs on this gameobject
/// and call GetComponent to get either the original gameobject or the parent
/// if the HitWithParent component is here.
/// </summary>
public class HitWithParent : MonoBehaviour
{
    public static T GetComponent<T>(Collider collision) where T : MonoBehaviour
    {
        var other = collision.gameObject;
        var hitWithParent = collision.GetComponent<HitWithParent>();
        
        if (hitWithParent != null)
            other = hitWithParent.transform.parent.gameObject;

        return other.GetComponent<T>();
    }
}
