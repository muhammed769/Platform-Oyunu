using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{

    [SerializeField] GameObject player; // YAN�  Unity k�sm�nda TriigerControl Scriptine bir player ad�nda bo� bir obje atam�� oldum. Daha sonra Unity de GER�EK PLAYER 'IMIZI  bu bo� player Objesine S�R�KLE.

    

    private void OnTriggerEnter2D(Collider2D collision)  // *********************** �OK AMA �OK �NEML� *************************** Player ' �n Collider'� ile Zemin'in Collider '� ile temas etii�i anda BU METOT DEVREYE G�R�YOR.
    {
        // Debug.Log("Trigger ger�ekle�ti.");  // YAN� TEMAS GER�EKLE�T�.  Trigger : Hareket, Temas, Tetiklemek

        player.GetComponent<PlayerController>().onGround = true; // player bo� objemize biz Ger�ek Player'�m�z� S�R�KLEM��T�K yani Player nesnesine ait ne var bu bo� nesneye KALITMI� OLDUK.
                                                                 // KODUN KISACA A�IKLAMASI : bu objeye bile�en al yani onGround �zelligini al, ald�n m� evet �ok g�zel buna true de�erini ATA.
    }
    private void OnTriggerExit2D(Collider2D collision) // Unity de ba�latt�m ve Player'�m bo�luk tu�una bast�g�mda z�pl�yo ya yani TEMAS GER�EKLE�M�YOR.YAN� TEMAS GER�EKLE�MED�G� ANDAK� KODLARIMIZI BURAYA YAZARIZ.
    {
        //  Debug.Log("Trigger ��kt�.");

        player.GetComponent<PlayerController>().onGround = false;
    }



}
