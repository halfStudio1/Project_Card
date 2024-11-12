using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResTester : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        ResMgr.Instance.LoadAssetAsync<AudioClip>("Royal Days", (clip) =>
        {
            audioSource.clip = clip;
            audioSource.Play();
        });

        ResMgr.Instance.UnloadSceneAsync("Royal Days");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            ResMgr.Instance.LoadSceneAsync("TestScene_1");
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            ResMgr.Instance.UnloadSceneAsync("TestScene_1");
        }
    }
}
