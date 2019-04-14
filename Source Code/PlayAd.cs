using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class PlayAd : MonoBehaviour
{

    public bool playAdv = false;

    MenuCamController mcC;
    GameController gC;

    public Transform endCamPos;

    private void Start()
    {
        mcC = FindObjectOfType<MenuCamController>();
        gC = GetComponent<GameController>();
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("", new ShowOptions() { resultCallback = HandleAdResult});
        }
    }

    void Update()
    {
        if(playAdv)
        {
            StartCoroutine(WaitAd());
        }
    }

    public IEnumerator WaitAd()
    {
        playAdv = false;
        yield return new WaitForSeconds(2.5f);
        ShowAd();
    }

    private void HandleAdResult(ShowResult result)
    {
        switch(result)
        {
            case ShowResult.Finished:
                Debug.Log("Player gains reward");
                mcC.setMount(endCamPos);
                StartCoroutine(gC.WaitPlayerSpawn());
                break;
            case ShowResult.Skipped:
                Debug.Log("Player did not watch whole ad");
                mcC.setMount(endCamPos);
                StartCoroutine(gC.WaitPlayerSpawn());
                break;
            case ShowResult.Failed:
                // Debug.Log("Player failed to launch ad");
                mcC.setMount(endCamPos);
                StartCoroutine(gC.WaitPlayerSpawn());
                break;
        }
    }

	
}
