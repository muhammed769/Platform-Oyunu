using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{

    [SerializeField] GameObject effect;
    //[SerializeField] Text scoreValueText; // Düþmaný öldürdügümüzdede puan alabilmemizi saðlar.


   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(collision.gameObject.tag=="Player")) // Çarptýgým objenin Etiketi Player Degilse bana þu iþlemleri yap : 
        {
            Destroy(gameObject); // Destroy(gameObject) ne iþe yarýyor ?  ArrowController Scriptine Hangi obje sahipse  O objedir. \\\\\\  YANÝ okumuzun ÇARPTIGI OBJE PLAYER DEÐÝLSE OKUMUZ KAYBOLACAK.
            // Unity kýsmýnda Player Inspectrounda Tag 'i Player OLARAK SEÇMEN LAZIM BU KODUN ÝÞLETÝLEBÝLMESÝ ÝÇÝN.

            if(collision.gameObject.CompareTag("Enemy")) // Çarpmýþ oldugum obje eger Enemy(Düþman) ise : 
            {
                Destroy(collision.gameObject); // Çarpmýþ oldugum objeyi YOK ET.    
                GameObject.Find("Level Manager").GetComponent<levelManager>().AddScore(100);
                Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity); // Effect objem çarptýgým objenin üzerinde oluþþsun                                                      
            }
        }
    }

    private void OnBecameInvisible()// OnBecameInvisible = Görünmez oldugu anda devreye giren Metottur.
    {
        Destroy(gameObject); // Yani ok görünmez oldugunda yok olsun tamamen. [ gameObject : Bu scripte sahip olan objedir(yani Arrow 'dur.) ] Yani okum Scene ' den cýktýgýnda Hierarcy 'den clonlanmýs olan Arrow silinmiþ oldu.
    }

}
