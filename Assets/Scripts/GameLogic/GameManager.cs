using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SelectedBuffer))]
public class GameManager : GenericSingletonClass<GameManager>
{
    public SelectedBuffer SelectedBuffer;
    public GameObject Frame;
    
    public GameStates GameStates;

    public override void Awake()
    {
        SelectedBuffer = GetComponent<SelectedBuffer>();
    }

}
public enum GameStates
{
    Start,
    Play,
    Stop,
    End
}
