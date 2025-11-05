using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffects : MonoBehaviour
{
    public static SpecialEffects specialEffects { get; private set; }
    [SerializeField]
    private GameObject smokeVFX;
    private AudioClip coinSFX;
    private AudioClip bonusSFX;
    private void Start()
    {
        smokeVFX = Resources.Load<GameObject>("SmokeVFX");
        coinSFX = Resources.Load<AudioClip>("CoinSFX");
        bonusSFX = Resources.Load<AudioClip>("BonusSFX");
    }
    public void CoinSFX()
    {
        GameObject speaker = new GameObject();
        speaker.gameObject.AddComponent<AudioSource>();
        speaker.gameObject.GetComponent<AudioSource>().clip = coinSFX;
        speaker.gameObject.GetComponent<AudioSource>().Play();
        float sfxLeangth = coinSFX.length;
        Destroy(speaker.gameObject, sfxLeangth);
    }
    public void BonusSFX()
    {
        GameObject speaker = new GameObject();
        speaker.gameObject.AddComponent<AudioSource>();
        speaker.gameObject.GetComponent<AudioSource>().clip = bonusSFX;
        speaker.gameObject.GetComponent<AudioSource>().Play();
        float sfxLeangth = bonusSFX.length;
        Destroy(speaker.gameObject, sfxLeangth);
    }
    private void Awake()
    {
        if (specialEffects != null && specialEffects != this)
        {
            Destroy(this);
        }
        else
        {
            specialEffects = this;
        }
    }
    public void CreateSmoke(Transform _transform)
    {
        GameObject smoke = Instantiate(smokeVFX, _transform.position,
        Quaternion.identity);
        Destroy(smoke.gameObject,
        smoke.GetComponent<ParticleSystem>().main.duration);
    }

}
