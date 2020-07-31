
using JetBrains.Annotations;
using UnityEngine;


/// <summary>
/// Abstract class that forms base for implementing singleton pattern.
/// <para>To enforce using singleton pattern you must implement OnAwake() method in your sub-class instead of using Awake() otherwise the singleton behaviour will break.</para>
/// </summary>


public abstract class Singleton<T> : Singleton where T : MonoBehaviour
{
    #region  Fields
    [CanBeNull]
    private static T _instance;

    [NotNull]
    // ReSharper disable once StaticMemberInGenericType
    private static readonly object Lock = new object();

    [SerializeField]
    [Tooltip("Should this singleton persist across scene loads?.")]
    private bool _persistent = true;

    private bool some;
   
    #endregion



    #region  Properties
    [NotNull]
    public static T instance                 ///<summary> The instance of this singleton class.</summary>
    {
        get
        {
            if (Quitting)
            {
                //Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] Instance will not be returned because the application is quitting.");
                // ReSharper disable once AssignNullToNotNullAttribute
                return null;
            }


            return _instance;
        }
    }

    #endregion

    #region  Methods
    protected virtual void Awake()
    {


        lock (Lock)
        {
             
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                var instances = FindObjectsOfType<Singleton<T>>();
                var count = instances.Length;
                //Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] There should never be more than one {nameof(Singleton)} of type {typeof(T)} in the scene, but {count} were found. The {typeof(T)} instance on \"{instances[0].name}\" gameobject will be used, and all others will be destroyed.");
                //Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] The extra instances found are on the following gameobjects :");

                for (int a  = 1; a < count; a++ )
                {
                    Debug.Log(a + ") : " + instances[a].name );
                }
             
                return; 
            }

            else
            {
                _instance = GetComponent<T>();
            }

            if (_persistent) DontDestroyOnLoad(this.gameObject);

            OnAwake();
        }


        
    }


    protected abstract void OnAwake();
    #endregion
}

public abstract class Singleton : MonoBehaviour
{
    #region  Properties
    public static bool Quitting { get; private set; }

    /*
    /// <summary>Should this singleton instance be allowed to access in any other platform than the one specified.</summary>
    [SerializeField]
    [Tooltip("Should this singleton instance be allowed to access in any other platform than the one specified.")]
    public bool platformBlocking = false;

    /// <summary>The platform on which this singleton only works.</summary>
    [SerializeField]
    [Tooltip("The platform on which this singleton only works.")]
    public RuntimePlatform limitedPlatform;

    /// <summary>The message to be logged to console when the singleton is accessed in any other platform than the one specified in the \"Limited Platform\" variable.</summary>
    [SerializeField]
    [Tooltip("The message to be logged to console when the singleton is accessed in any other platform than the one specified in the \"Limited Platform\" variable.")]
    public string platformErrorMessage;
    */

    #endregion


    #region  Methods
    private void OnApplicationQuit()
    {
        Quitting = true;
    }
    #endregion
}
