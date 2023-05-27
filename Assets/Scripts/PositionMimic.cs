using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMimic : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform targetMan, targetWoman;

    private void Start()
    {
        ButtonGenero.onChangeGenre.AddListener(OnChangeGenre);
    }

    private void OnChangeGenre(int g)
    {
        target = g == 0 ? targetWoman : targetMan;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }
}
