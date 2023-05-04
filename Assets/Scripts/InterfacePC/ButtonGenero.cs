using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;

public class ButtonGenero: MonoBehaviour
{
    public AudioClip messageClip;
    public AudioSource messageSound;
    private void OnTriggerEnter(Collider other)
    {
    
        GameObject avatar = GameObject.FindWithTag("Player");
        GameObject avatarHombre = avatar.transform.Find("Avatar").gameObject;
        GameObject avatarMujer = avatar.transform.Find("AvatarMujer").gameObject;
        Debug.Log("CAMBIO GENERO," + avatarHombre.active+", "+ avatarMujer.active);
        messageSound.PlayOneShot(messageClip);
        if (avatarHombre.active)
        {
            Debug.Log("CAMBIO MUJER");
            avatarHombre.SetActive(false);
            avatarMujer.SetActive(true);
        }else if (avatarMujer.active)
        {
            Debug.Log("CAMBIO HOMBRE");
            avatarHombre.SetActive(true);
            avatarMujer.SetActive(false);
        }
    }
}
