using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUE : MonoBehaviour
{

    [SerializeField] private GameObject FTUEPrefab;

    // Start is called before the first frame update
    void Start()
    {
        FTUEPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenFTUEPopup() {

        FTUEPrefab.SetActive(true);

    }

    public void CloseFTUEPopup() {

        FTUEPrefab.SetActive(false);

    }
}
