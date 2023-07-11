using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DastGhost : MonoBehaviour
{
    [SerializeField]float ghostDelay;
    float ghostDelaySeconds;

    [SerializeField] GameObject ghost;
    public bool makeGhost = false;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                //Ghost »ý¼º
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 0.5f);
            }
        }
        else
        {
            makeGhost = false;
        }
    }
}
