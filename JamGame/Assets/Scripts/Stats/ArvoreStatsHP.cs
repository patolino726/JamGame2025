using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArvoreStatsHP : MonoBehaviour
{

    public float Hp_Arvore;
    public float Max_HP_Arvore;

    void Death()
    {
        if (Hp_Arvore <= 0 )
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Hp_Arvore = Max_HP_Arvore;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }
}
