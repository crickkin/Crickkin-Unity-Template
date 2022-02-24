using System;
using UnityEngine;

public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    public static bool IsAwakened { get; private set; }
    public static bool IsStarted { get; private set; }
    public static bool IsDestroyed { get; private set; }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                if (IsDestroyed) return null;

                _instance = FindExistingInstance() ?? CreateNewInstance();
            }

            return _instance;
        }
    }

    #region Singleton Implementation
    private static T _instance;

    private static T FindExistingInstance()
    {
        T[] existingInstances = FindObjectsOfType<T>();

        if (existingInstances == null || existingInstances.Length == 0) return null;

        return existingInstances[0];
    }

    private static T CreateNewInstance()
    {
        var containerGO = new GameObject("__" + typeof(T).Name + "(Singleton)");
        return containerGO.AddComponent<T>();
    }
    #endregion

    #region Singleton Life-time Management
    protected virtual void SingletonAwakened() { }

    protected virtual void SingletonStarted() { }

    protected virtual void SingletonDestroyed() { }

    protected virtual void NotifyInstanceRepeated()
    {
        Component.Destroy(this.GetComponent<T>());
    }

    #endregion

    #region Unity3d Messages - DO NOT OVERRRIDE / IMPLEMENT THESE METHODS in child classes!
    void Awake()
    {
        T thisInstance = this.GetComponent<T>();

        // Initialize the singleton if the script is already in the scene in a GameObject
        if (_instance == null)
        {
            _instance = thisInstance;
            DontDestroyOnLoad(_instance.gameObject);

        }

        else if (thisInstance != _instance)
        {
            PrintWarn(string.Format(
                "Found a duplicated instance of a Singleton with type {0} in the GameObject {1}",
                this.GetType(), this.gameObject.name));

            NotifyInstanceRepeated();

            return;
        }


        if (!IsAwakened)
        {
            PrintLog(string.Format(
                "Awake() Singleton with type {0} in the GameObject {1}",
                this.GetType(), this.gameObject.name));

            SingletonAwakened();
            IsAwakened = true;
        }

    }

    void Start()
    {
        // do not start it twice
        if (IsStarted) return;

        PrintLog(string.Format(
            "Start() Singleton with type {0} in the GameObject {1}",
            this.GetType(), this.gameObject.name));

        SingletonStarted();
        IsStarted = true;
    }

    void OnDestroy()
    {
        // Here we are dealing with a duplicate so we don't need to shut the singleton down
        if (this != _instance) return;

        // Flag set when Unity sends the message OnDestroy to this Component.
        // This is needed because there is a chance that the GO holding this singleton
        // is destroyed before some other object that also access this singleton when is being destroyed.
        // As the singleton instance is null, that would create both a new instance of this
        // MonoBehaviourSingleton and a brand new GO to which the singleton instance is attached to..
        //
        // However as this is happening during the Unity app shutdown for some reason the newly created GO
        // is kept in the scene instead of being discarded after the game exists play mode.
        // (Unity bug?)
        IsDestroyed = true;

        PrintLog(string.Format(
            "Destroy() Singleton with type {0} in the GameObject {1}",
            this.GetType(), this.gameObject.name));
        SingletonDestroyed();
    }

    #endregion

    #region Debug Methods (available in child classes)
    [Header("Debug")]
    /// <summary>
    ///  Set this to true either by code or in the inspector to print trace log messages
    /// </summary>
    public bool PrintTrace = false;

    protected void PrintLog(string str, params object[] args)
    {
        Print(UnityEngine.Debug.Log, PrintTrace, str, args);
    }

    protected void PrintWarn(string str, params object[] args)
    {
        Print(UnityEngine.Debug.LogWarning, PrintTrace, str, args);
    }

    protected void PrintError(string str, params object[] args)
    {
        Print(UnityEngine.Debug.LogError, PrintTrace, str, args);
    }

    private void Print(Action<string> call, bool doPrint, string str, params object[] args)
    {
        if (doPrint)
        {
            call(string.Format(
                "<b>[{0}][{1}] {2} </b>",
                Time.frameCount,
                this.GetType().Name.ToUpper(),
                string.Format(str, args)
                )
                 );
        }
    }

    #endregion
}
