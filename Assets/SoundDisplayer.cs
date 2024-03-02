using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDisplayer : MonoBehaviour
{
    public static SoundDisplayer instance;
    [SerializeField] Transform daParent, cam;
    [SerializeField] GameObject indicator;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of SoundDisplayer Found");
            return;
        }
        instance = this;
    }

    public void TranslateSound(Vector3 pos)
    {
        GameObject newIndicator = Instantiate(indicator, daParent);

        Vector3 dir = transform.position - pos;

        Quaternion newRot = Quaternion.LookRotation(dir);
        newRot.z = -newRot.y;
        newRot.x = 0;
        newRot.y = 0;

        Vector3 north = new Vector3(0, 0, cam.transform.eulerAngles.y);
        newIndicator.transform.localRotation = newRot * Quaternion.Euler(north);
        Destroy(newIndicator, 2f);
    }
}
