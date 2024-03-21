namespace Tutorials
{
    #if UNITY_EDITOR
    using System;
    using UnityEngine;
    using static UnityEditor.EditorPrefs;

    internal enum SettingType
    {
        Boolean,
        Floating,
        Integer,
    }
    
    internal enum BoolCompare
    {
        Equal,
        NotEqual,
    }

    internal enum NumCompare
    {
        Equal,
        NotEqual,
        GT,
        LT,
        GE,
        LE,
    }
    
    [CreateAssetMenu(fileName = "HasChangedSetting", menuName = "Tutorials/HasChangedSetting")]
    public sealed class HasChangedSetting : ScriptableObject
    {
        [SerializeField] private String      settingName;
        [SerializeField] private SettingType settingType;
        
        [Space]
        
        [SerializeField] private BoolCompare booleanCompare;
        [SerializeField] private Boolean     boolean;
        
        [Space]
        
        [SerializeField] private NumCompare  floatingCompare;
        [SerializeField] private Single      floating;
        
        [Space]
        
        [SerializeField] private NumCompare  integerCompare;
        [SerializeField] private Int32       integer;
        
        public Boolean HasChangedToCorrectValue()
        {
            return settingType switch
            {
                SettingType.Boolean => booleanCompare switch
                {
                    BoolCompare.Equal    => GetBool(settingName) == boolean,
                    BoolCompare.NotEqual => GetBool(settingName) != boolean,
                    _                    => throw new ArgumentOutOfRangeException(),
                },
                SettingType.Floating => floatingCompare switch
                {
                    NumCompare.Equal    => Mathf.Approximately(GetFloat(settingName), floating),
                    NumCompare.NotEqual => !Mathf.Approximately(GetFloat(settingName), floating),
                    NumCompare.GT       => GetFloat(settingName) >  floating,
                    NumCompare.LT       => GetFloat(settingName) <  floating,
                    NumCompare.GE       => GetFloat(settingName) >= floating,
                    NumCompare.LE       => GetFloat(settingName) <= floating,
                    _                   => throw new ArgumentOutOfRangeException(),
                },
                SettingType.Integer => integerCompare switch
                {
                    NumCompare.Equal    => GetInt(settingName) == integer,
                    NumCompare.NotEqual => GetInt(settingName) != integer,
                    NumCompare.GT       => GetInt(settingName) >  integer,
                    NumCompare.LT       => GetInt(settingName) <  integer,
                    NumCompare.GE       => GetInt(settingName) >= integer,
                    NumCompare.LE       => GetInt(settingName) <= integer,
                    _                   => throw new ArgumentOutOfRangeException(),
                },
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
    
    #endif
}
