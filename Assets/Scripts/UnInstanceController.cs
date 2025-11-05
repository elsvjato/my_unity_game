using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnInstanceController : MonoBehaviour
{
    [SerializeField]
    private GameObject instanceIcon; 
    private Stack<GameObject> instanceStack; 
    private float offset = 40f; 
    [SerializeField]
    [Range(1, 99)]
    private int instanceMaxCount = 4; 

    void Start()
    {
        instanceStack = new Stack<GameObject>();
        CreateInstance(instanceMaxCount);
    }

    public void CreateInstance()
    {
        if (instanceStack.Count < instanceMaxCount)
        {
            Vector2 newPosition = new Vector2(
                transform.position.x + (instanceStack.Count * offset),
                transform.position.y
            );

            GameObject newInstance = Instantiate(instanceIcon, newPosition, Quaternion.identity);
            newInstance.transform.parent = transform; 
            instanceStack.Push(newInstance);

            UpdateInstancePositions();
        }
    }

    public void CreateInstance(int _number)
    {
        for (int i = 0; i < _number; i++)
        {
            CreateInstance();
        }
    }


    public void DestroyInstance()
    {
        if (instanceStack.Count > 0)
        {
            Destroy(instanceStack.Pop().gameObject);
            UpdateInstancePositions();

            Debug.Log("you lost one life. remaining: " + instanceStack.Count);
        }
        else
        {
            Debug.LogWarning("you lost(. try again");
            StartCoroutine(ReloadScene());
        }
    }


    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1f); //waiting before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void UpdateInstancePositions()
    {
        int index = 0;
        foreach (GameObject heart in instanceStack)
        {
            heart.transform.position = new Vector3(
                transform.position.x + (index * offset),
                transform.position.y,
                transform.position.z
            );
            index++;
        }
    }

    public int InstanceCount => instanceStack.Count;
}