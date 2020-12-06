using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickup : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    [SerializeField]
    float Distance = 5f;
    Camera mainCam;
    public LayerMask layer;
    public Text pickupText;
    public GameObject objective;
    public GameObject finalObjective;
    public int keyCount;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(keyCount == 4)
        {
            objective.SetActive(false);
            finalObjective.SetActive(true);
        }
        ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Physics.Raycast(ray,out hit, Distance, layer))
        {
            pickupText.enabled = true;
            pickupText.text = hit.transform.name.ToString();
            if(hit.transform.tag == "key")
            {
                pickupKey();
            }
        }
        else
        {
            pickupText.enabled = false;
        }
    }

    void pickupKey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(hit.transform.gameObject);
            keyCount++;
            pickupText.enabled = false;
        }
    }
}
