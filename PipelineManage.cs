using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject ģ��;
    List<Pipeline> Pipelines = new List<Pipeline>();
    public float ���ʱ��;

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
            yield return new WaitForSeconds(���ʱ��);
        }
    }   
    void CreatePipeline()
    {
       if (Pipelines.Count < 3)
       {
          GameObject obj = Instantiate(ģ��, this.transform);
          Pipeline P = obj.GetComponent<Pipeline>();
          Pipelines.Add(P);
       }
    }

    private void Update()
    {
        
    }
}
