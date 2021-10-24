using UnityEngine;

public class AudioMonsterTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<AudioManager>().Play("Spatial");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
