using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject target;

    public float maxDistance = 0.7f;
    public float speedFalloff = 0.7f;

    public GunArray followerArray { get; set; }

    static Vector3 forwardOffset = Vector3.forward * 0;

    // Start is called before the first frame update
    void Start()
    {
        followerArray = GetComponentInChildren<GunArray>();
    }

    void Update()
    {
        if (tag == null) return;

        Vector3 targetDifference = (target.transform.position + forwardOffset) - transform.position;

        if (targetDifference.magnitude > maxDistance)
        {
            transform.position += targetDifference * Time.deltaTime * (targetDifference.magnitude / speedFalloff);
        }

    }

    public static Follower SpawnFollower(Follower follower, GameObject targetObject, Transform spawnTransform, GunArray initialArray)
    {
        GameObject go = Instantiate(follower.gameObject, spawnTransform.position, Quaternion.identity, PlayAreaManager.Instance.playArea);

        Follower newFollower = go.GetComponent<Follower>();
        newFollower.target = targetObject;

        GunArray newFollowerrArray = go.GetComponentInChildren<GunArray>();
        newFollowerrArray.ChangeArrayTo(initialArray.gameObject, initialArray.bulletType);

        return newFollower;
    }

    public void FollowerShoot()
    {
        followerArray.Shoot();
    }
}
