using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameController Gamecon;
    public List<List<GameObject>> Imagelists;
    public List<int> Valuelist;
    public List<GameObject> Ammoimages = new List<GameObject>();
    public List<GameObject> Pebbleimages = new List<GameObject>();
    public List<GameObject> Keyimages = new List<GameObject>();
    void Start()
    {
        Imagelists = new List<List<GameObject>>();
        Imagelists.Add(Ammoimages);
        Imagelists.Add(Pebbleimages);
        Imagelists.Add(Keyimages);
        Valuelist.Add(Gamecon.getAmmo());
        Valuelist.Add(Gamecon.getPebbles());
        Valuelist.Add(Gamecon.getKeys());


    }

    // Update is called once per frame
    void Update()
    {
        Valuelist[0] = Gamecon.getAmmo();
        Valuelist[1] = Gamecon.getPebbles();
        Valuelist[2] = Gamecon.getKeys();
        for (int Listindex = 0; Listindex < 3; Listindex++)
        {
            for (int index = 0; index < Imagelists[Listindex].Count; index++)
            {
                if (index + 1 <= Valuelist[Listindex])
                {
                    Imagelists[Listindex][index].SetActive(true);
                }
                else
                {
                    Imagelists[Listindex][index].SetActive(false);
                }
            }
        }
    }
}
