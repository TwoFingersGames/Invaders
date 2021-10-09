using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{

    public GameObject Frame;
    private void OnMouseDown()
    {
        if (TryGetComponent(out Planet planet))
        {
            if(planet.TryGetComponent(out Player player))
            {
                GameManager.Instance.SelectedBuffer.StartPlanets.Add(planet);
                GameManager.Instance.SelectedBuffer.SelectFrames.Add(Instantiate(GameManager.Instance.Frame, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform));
            }
            else
            {
                GameManager.Instance.SelectedBuffer.TargetPlanet = planet;
                
                foreach (var startPlanet in GameManager.Instance.SelectedBuffer.StartPlanets)
                {
                    startPlanet.TryGetComponent(out UnitSpawner spawner);
                    spawner.CreateUnits();
                }
                foreach (var item in GameManager.Instance.SelectedBuffer.SelectFrames)
                {
                    Destroy(item.gameObject);
                }
                GameManager.Instance.SelectedBuffer.StartPlanets.Clear();
            }
        }
    }
}