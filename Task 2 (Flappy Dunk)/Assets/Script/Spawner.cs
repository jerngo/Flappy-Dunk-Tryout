using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timeUntilSpawn=3;
    public float maxHeight;

    public float ringRotation=0;
    public float ringRotationMax=90;

    public float ringSpeed=1.5f;

    float normalSpeed;

    public float ringBorderLimit=-2.5f;

    public GameObject[] ringPrefab;

    Queue<GameObject> rings;
    public GameObject[] ringInQueue;

    [SerializeField]
    int ringIndexMax;

    // Start is called before the first frame update
    void Start()
    {
        //ringIndexMax = 0;
        normalSpeed = ringSpeed;
        rings = new Queue<GameObject>();
        StartCoroutine(spawnRing(timeUntilSpawn));
       
    }

    //slow down the movement
    public void slowDown(bool isOn) {
        if (ringSpeed != 0) {
            if (isOn)
            {
                ringSpeed = ringSpeed / 2;
            }
            else
            {
                ringSpeed = normalSpeed;
            }
        }
    
       
    }

    //increase number index for choosing the prefabs
    public void increaseRingCollectionIndex() {
        if (ringIndexMax < ringPrefab.Length) {
            ringIndexMax += 1;
        }

        
    }

    //stop all movement
    public void stopMovement() {
        ringSpeed = 0;
        StopAllCoroutines();
    }

    //spawn ring with delay
    IEnumerator spawnRing(float delay) {
        GameObject ring = Instantiate(ringPrefab[Random.Range(0,ringIndexMax)]);
        ring.transform.position = transform.position + new Vector3(0, Random.Range(maxHeight, -maxHeight), 0);
        ring.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, ringRotation));
        //store ring that been created in queue
        rings.Enqueue(ring);

        yield return new WaitForSeconds(delay);

        StartCoroutine(spawnRing(timeUntilSpawn));

    }

    private void Update()
    {
        ringInQueue = rings.ToArray();

        if (rings.Count > 0)
        {
            //only make the first ring in queue the active one
            Ring firstRinginQueue = rings.Peek().GetComponent<Ring>();

            if (firstRinginQueue == null) {
                firstRinginQueue = rings.Peek().GetComponentInChildren<Ring>();
            }

            if (!firstRinginQueue.isActivated)
            {
                firstRinginQueue.activateRing();
            }
        }

           
    }

    //dequeue ring in queue
    public void dequeRing() {
        if (rings.Count > 0) {
            rings.Dequeue();
        }
    }

    //increase max rotation for ring
    public void increaseRotation(int increasedRotation) {
        if (ringRotation <= ringRotationMax) {
            ringRotation += increasedRotation;
        }
    }
}
