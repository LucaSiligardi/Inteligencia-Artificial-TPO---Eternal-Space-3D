using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("Target a seguir")]
    public Transform target;

    [Header("Offset respecto al player")]
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    [Header("Suavidad del seguimiento")]
    public float followSpeed = 12f;

    void LateUpdate()
    {
        if (!target) return;

       
        Vector3 posicionCamanra = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, posicionCamanra, followSpeed * Time.deltaTime);

        transform.rotation = Quaternion.LookRotation(Vector3.forward);

    }
}