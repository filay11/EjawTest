using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class AutoColorChanger : MonoBehaviour
{
    public GameData gameData;   // не очень хорошо
    public GeometryObjectModel objectModel;

    private string nameOfGameData = "GameData";
    void Start()
    {
        gameData = Resources.Load<GameData>($"GameData/{nameOfGameData}");
        if (gameData == null) 
        {            
            return;
        }
        
        objectModel = gameObject.GetComponent<GeometryObjectModel>();
        if (objectModel == null)
        {            
            return;
        }
        
        Observable.Timer(System.TimeSpan.FromSeconds(gameData.ObservableTime))
            .Repeat()
            .Subscribe(_ =>
            {
                objectModel.ChangeColorRandomly();                
            }).AddTo(this);        
    }    
}
