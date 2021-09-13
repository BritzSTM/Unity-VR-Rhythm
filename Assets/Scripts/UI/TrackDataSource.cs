using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;

public class TrackDataSource : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField] private TrackDataBaseSO _trackDataBaseSO;
    [SerializeField] private RecyclableScrollRect _recyclableScrollRect;

    private List<TrackDescSO> _contactList = new List<TrackDescSO>();

    private void Awake()
    {
        foreach(var trackSO in _trackDataBaseSO.trackDatas)
        {
            _contactList.Add(trackSO);
        }

        _recyclableScrollRect.DataSource = this;
    }

    public int GetItemCount() => _contactList.Count;

    public void SetCell(ICell cell, int index)
    {
        var item = cell as TrackCell;
        item.ConfigureCell(_contactList[index], index);
    }
}
