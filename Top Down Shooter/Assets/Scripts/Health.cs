using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float hearts;
    public int numOfHearts;

    public Image[] heartimages;
    public Sprite fullHeart;
    public Sprite emptyheart;


    void Start()
    {
        hearts = 3;
        numOfHearts = 3;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (hearts > numOfHearts)
        {
            hearts = numOfHearts;
        }

        for (int i =0;i<heartimages.Length;i++)
        {
            if (i < hearts)
            {
                heartimages[i].sprite = fullHeart;
            }
            else
            {
                heartimages[i].sprite = emptyheart;
            }
            if (i < numOfHearts)
            {
                heartimages[i].enabled = true;
            }
            else
            {
                heartimages[i].enabled = false;
            }
        }
    }
}
