using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject prefab_ziemia;
    public GameObject prefab_podZiemią;
    public GameObject prefab_nadZiemią;
    public GameObject prefab_nadZiemią_chwast;
    public GameObject prefab_potwór;
    public GameObject prefab_woda_particle;

    void Start()
    {
        Generate(prefab_ziemia);
    }
    
    public void Generate(GameObject obiekt)
    {
        int poprzedniawartość = 0;
        for (int i = 0; i <= 300; i++)
        {
            int wylosowana_wysokość = poprzedniawartość + Random.Range(-2, 4) & ~1;

            GameObject stworzony_obiekt = Instantiate(prefab_ziemia, this.gameObject.transform) as GameObject;
            stworzony_obiekt.transform.position = new Vector2(i * 2, wylosowana_wysokość);

            int losowanie_potwora = Random.Range(0, 11);

            if (losowanie_potwora == 1)
            {
                GameObject stworzony_potwór = Instantiate(prefab_potwór, this.gameObject.transform) as GameObject;
                stworzony_potwór.transform.position = stworzony_obiekt.transform.position +new Vector3(0, +4);
            }



            int losowanieChwasta=1;
            losowanieChwasta = Random.Range(0, 2);
           

            if (losowanieChwasta == 1)
            {
                GameObject stworzony_obiekt3 = Instantiate(prefab_nadZiemią, this.gameObject.transform) as GameObject;
                stworzony_obiekt3.transform.position = new Vector2(i * 2, wylosowana_wysokość + 10);
            }
            else if(losowanieChwasta == 0)
            {
                GameObject stworzony_obiekt3 = Instantiate(prefab_nadZiemią_chwast, this.gameObject.transform) as GameObject;
                stworzony_obiekt3.transform.position = new Vector2(i * 2, wylosowana_wysokość + 10);
            }


            int losowanieWody = 1;
            losowanieWody = Random.Range(0, 8);
            Debug.Log(losowanieWody);

            if (losowanieWody == 1)
            {
                GameObject stworzony_obiekt4 = Instantiate(prefab_woda_particle, this.gameObject.transform) as GameObject;
                stworzony_obiekt4.transform.position = new Vector2(i * 2, wylosowana_wysokość + 9.5f);
            }




            poprzedniawartość = wylosowana_wysokość;
            for (int x = 0; x > (-20) - wylosowana_wysokość/2; x--)
            {
                GameObject stworzony_obiekt2 = Instantiate(prefab_podZiemią, this.gameObject.transform) as GameObject;
                stworzony_obiekt2.transform.position = stworzony_obiekt.transform.position + new Vector3(0, x * 2-2);
            }
            for (int x = 0; x < (20) - wylosowana_wysokość / 2; x++)
            {
                GameObject stworzony_obiekt2 = Instantiate(prefab_podZiemią, this.gameObject.transform) as GameObject;
                stworzony_obiekt2.transform.position = stworzony_obiekt.transform.position + new Vector3(0, (x * 2) + 12);
            }
        }




    }
}
