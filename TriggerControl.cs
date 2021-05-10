using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{

    [SerializeField] GameObject player; // YANÝ  Unity kýsmýnda TriigerControl Scriptine bir player adýnda boþ bir obje atamýþ oldum. Daha sonra Unity de GERÇEK PLAYER 'IMIZI  bu boþ player Objesine SÜRÜKLE.

    

    private void OnTriggerEnter2D(Collider2D collision)  // *********************** ÇOK AMA ÇOK ÖNEMLÝ *************************** Player ' ýn Collider'ý ile Zemin'in Collider 'ý ile temas etiiði anda BU METOT DEVREYE GÝRÝYOR.
    {
        // Debug.Log("Trigger gerçekleþti.");  // YANÝ TEMAS GERÇEKLEÞTÝ.  Trigger : Hareket, Temas, Tetiklemek

        player.GetComponent<PlayerController>().onGround = true; // player boþ objemize biz Gerçek Player'ýmýzý SÜRÜKLEMÝÞTÝK yani Player nesnesine ait ne var bu boþ nesneye KALITMIÞ OLDUK.
                                                                 // KODUN KISACA AÇIKLAMASI : bu objeye bileþen al yani onGround özelligini al, aldýn mý evet çok güzel buna true deðerini ATA.
    }
    private void OnTriggerExit2D(Collider2D collision) // Unity de baþlattým ve Player'ým boþluk tuþuna bastýgýmda zýplýyo ya yani TEMAS GERÇEKLEÞMÝYOR.YANÝ TEMAS GERÇEKLEÞMEDÝGÝ ANDAKÝ KODLARIMIZI BURAYA YAZARIZ.
    {
        //  Debug.Log("Trigger çýktý.");

        player.GetComponent<PlayerController>().onGround = false;
    }



}
