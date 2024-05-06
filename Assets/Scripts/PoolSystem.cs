using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    public static PoolSystem Instance { get; private set; }
    private Dictionary<string, List<GameObject>> allPool = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        Instance = this;
    }

    #region Non-Genreic
    public GameObject LoadObject(string objectPath, Vector3 position, Transform parent = null)
    {
        var key = objectPath;

        if (!allPool.ContainsKey(key))
        {
            var newObject = Instantiate(Resources.Load<GameObject>(objectPath), position, Quaternion.identity, parent);
            var newPool = new List<GameObject>();
            newPool.Add(newObject);
            allPool.Add(key, newPool);
            return newObject;
        }

        var pool = allPool[key];
        for (int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                pool[i].transform.SetParent(parent);
                pool[i].transform.position = position;
                return pool[i];
            }
        }

        var addObject = Instantiate(Resources.Load<GameObject>(objectPath), position, Quaternion.identity, parent);
        pool.Add(addObject);
        return addObject;
    }

    public GameObject LoadObject(GameObject obj, Vector3 position, Transform parent = null)
    {       
        if (!allPool.ContainsKey(obj.name))
        {
            var newObject = Instantiate(obj, position, Quaternion.identity, parent);
            var newPool = new List<GameObject>();
            newPool.Add(newObject);
            allPool.Add(obj.name, newPool);
            return newObject;
        }

        var pool = allPool[obj.name];
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                pool[i].transform.SetParent(parent);
                pool[i].transform.position = position;
                return pool[i];
            }
        }

        var addObject = Instantiate(obj, position, Quaternion.identity, parent);
        pool.Add(addObject);
        return addObject;
    }
    #endregion

    #region Generic
    public T LoadObject<T>(string objectPath, Vector3 position, Transform parent = null) where T : Component
    {
        var key = objectPath;

        if (!allPool.ContainsKey(key))
        {
            var newObject = Instantiate(Resources.Load<GameObject>(objectPath), position, Quaternion.identity, parent);
            var newPool = new List<GameObject>();
            newPool.Add(newObject);
            allPool.Add(key, newPool);
            if (newObject.TryGetComponent<T>(out var returnType)) return returnType;
            else 
            {
                var message = new string[] { $"Can't find component {newObject.name} in GameObjetc", objectPath };
                Debug.LogError(message);
                return default(T);
            }
        }

        var pool = allPool[key];
        GameObject disableObject = null;
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                disableObject = pool[i];
                break;
            }
        }

        if (disableObject != null)
        {
            disableObject.transform.position = position;
            disableObject.SetActive(true);
            disableObject.transform.SetParent(parent);
            if (disableObject.TryGetComponent<T>(out var type)) return type;
            else 
            {
                var message = new string[] { $"Can't find component {disableObject.name} in GameObjetc", objectPath };
                Debug.LogError(message);
                return default(T);
            }
        }

        var addObject = Instantiate(Resources.Load<GameObject>(objectPath), position, Quaternion.identity, parent);
        pool.Add(addObject);
        if(addObject.TryGetComponent<T>(out var newType)) return newType;
        else 
        {
            var message = new string[] { "Can't find component in GameObjetc", objectPath };
            Debug.LogError(message);
            return default(T);
        }      
    }

    public T LoadObject<T>(T obj, Vector3 position, Transform parent = null) where T : Component
    {       
        if (!allPool.ContainsKey(obj.name))
        {
            var newObject = Instantiate(obj, position, Quaternion.identity, parent);
            var newPool = new List<GameObject>();
            newPool.Add(newObject.gameObject);
            allPool.Add(obj.name, newPool);
            if(newObject.TryGetComponent<T>(out var returnType)) return returnType;
            else
            {
                var message = new string[] { "Can't find component in GameObjetc", obj.name };
                Debug.LogError(message);
                return default(T);
            }
        }

        var pool = allPool[obj.name];
        GameObject disableObject = null;
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                disableObject = pool[i];
                break;
            }
        }

        if (disableObject != null)
        {
            disableObject.transform.position = position;
            disableObject.SetActive(true);
            disableObject.transform.SetParent(parent);
            if (disableObject.TryGetComponent<T>(out var type)) return type;
            else
            {
                var message = new string[] { "Can't find component in GameObjetc", obj.name };
                Debug.LogError(message);
                return default(T);
            }
        }

        var addObject = Instantiate(obj, position, Quaternion.identity, parent);
        pool.Add(addObject.gameObject);
        if(addObject.TryGetComponent<T>(out var newType)) return newType;
        else
        {
            var message = new string[] { "Can't find component in GameObjetc", obj.name };
            Debug.LogError(message);
            return default(T);
        }
    }

    public T LoadObject<T>(GameObject obj, Vector3 position, Transform parent = null) where T : Component
    {
        if (!allPool.ContainsKey(obj.name))
        {
            var newObject = Instantiate(obj, position, Quaternion.identity, parent);
            var newPool = new List<GameObject>();
            newPool.Add(newObject.gameObject);
            allPool.Add(obj.name, newPool);
            if (newObject.TryGetComponent<T>(out var returnType)) return returnType;
            else
            {
                var message = new string[] { "Can't find component in GameObjetc", obj.name };
                Debug.LogError(message);
                return default(T);
            }
        }

        var pool = allPool[obj.name];
        GameObject disableObject = null;
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                disableObject = pool[i];
                break;
            }
        }

        if (disableObject != null)
        {
            disableObject.transform.position = position;
            disableObject.SetActive(true);
            disableObject.transform.SetParent(parent);
            if (disableObject.TryGetComponent<T>(out var type)) return type;
            else
            {
                var message = new string[] { "Can't find component in GameObjetc", obj.name };
                Debug.LogError(message);
                return default(T);
            }
        }

        var addObject = Instantiate(obj, position, Quaternion.identity, parent);
        pool.Add(addObject.gameObject);
        if (addObject.TryGetComponent<T>(out var newType)) return newType;
        else
        {
            var message = new string[] { "Can't find component in GameObjetc", obj.name };
            Debug.LogError(message);
            return default(T);
        }
    }
    #endregion
}