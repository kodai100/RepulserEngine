using System.Collections;
using UnityEngine;

namespace ProjectBlue.RepulserEngine
{
    public class ScreenResolutionManager : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.5f);

            Screen.SetResolution(1280, 720, false);
        }
    }
}