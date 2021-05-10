using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{

    [SerializeField] GameObject effect;
    //[SerializeField] Text scoreValueText; // D��man� �ld�rd�g�m�zdede puan alabilmemizi sa�lar.


   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!(collision.gameObject.tag=="Player")) // �arpt�g�m objenin Etiketi Player Degilse bana �u i�lemleri yap : 
        {
            Destroy(gameObject); // Destroy(gameObject) ne i�e yar�yor ?  ArrowController Scriptine Hangi obje sahipse  O objedir. \\\\\\  YAN� okumuzun �ARPTIGI OBJE PLAYER DE��LSE OKUMUZ KAYBOLACAK.
            // Unity k�sm�nda Player Inspectrounda Tag 'i Player OLARAK SE�MEN LAZIM BU KODUN ��LET�LEB�LMES� ���N.

            if(collision.gameObject.CompareTag("Enemy")) // �arpm�� oldugum obje eger Enemy(D��man) ise : 
            {
                Destroy(collision.gameObject); // �arpm�� oldugum objeyi YOK ET.    
                GameObject.Find("Level Manager").GetComponent<levelManager>().AddScore(100);
                Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity); // Effect objem �arpt�g�m objenin �zerinde olu��sun                                                      
            }
        }
    }

    private void OnBecameInvisible()// OnBecameInvisible = G�r�nmez oldugu anda devreye giren Metottur.
    {
        Destroy(gameObject); // Yani ok g�r�nmez oldugunda yok olsun tamamen. [ gameObject : Bu scripte sahip olan objedir(yani Arrow 'dur.) ] Yani okum Scene ' den c�kt�g�nda Hierarcy 'den clonlanm�s olan Arrow silinmi� oldu.
    }

}
