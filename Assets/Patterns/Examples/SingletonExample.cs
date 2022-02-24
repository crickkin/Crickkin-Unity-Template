using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonExample : SingletonMonobehaviour<SingletonExample>
{
    // Awake is called right after creation when Singleton Settings are ready
    protected override void SingletonAwakened()
    {
        
    }

    // Start is called before the first frame update after Singleton Settings
    protected override void SingletonStarted()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
