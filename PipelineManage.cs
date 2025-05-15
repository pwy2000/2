using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject 模板;
    List<Pipeline> Pipelines = new List<Pipeline>();
    public float 间隔时间;

    void Start()
    {
       
    }

    Coroutine runner = null;
    public void Init()
    {
        for (int i = 0; i < Pipelines.Count; i++)
        {
            Destroy(Pipelines[i].gameObject);
        }
        Pipelines.Clear();
    }
    public void StartRun()
    {
        runner = StartCoroutine(GenetatePipelines());
    }

    public void Stop()
    {
        StopCoroutine(runner);
        for (int i = 0; i < Pipelines.Count; i++)
            Pipelines[i].enabled = false;
    }

    IEnumerator GenetatePipelines()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Pipelines.Count < 3)
                CreatePipeline();
            else
            {
                Pipelines[i].enabled = true;
                Pipelines[i].Init();
            }
            yield return new WaitForSeconds(间隔时间);
        }
    }   
    void CreatePipeline()
    {
       if (Pipelines.Count < 3)
       {
          GameObject obj = Instantiate(模板, this.transform);
          Pipeline P = obj.GetComponent<Pipeline>();
          Pipelines.Add(P);
       }
    }

    private void Update()
    {
        
    }
}
