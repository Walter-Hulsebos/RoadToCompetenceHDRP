namespace Tutorials
{
    #if UNITY_EDITOR
    using System;

    using UnityEditor;
    using UnityEditor.UIElements;

    using UnityEngine;
    using UnityEngine.UIElements;

    using static UnityEditor.EditorPrefs;

    internal enum SettingType : Int32
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
        Gt,
        LT,
        Ge,
        Le,
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
        
        [ContextMenu(itemName: "CheckHasChangedToCorrectValue")]
        public void CheckHasChangedToCorrectValue()
        {
            Debug.Log(message: HasChangedToCorrectValue() ? "Setting has changed to correct value." : "Setting has not changed to correct value.");
        }
        
        public Boolean HasChangedToCorrectValue()
        {
            return settingType switch
            {
                SettingType.Boolean => booleanCompare switch
                {
                    BoolCompare.Equal    => GetBool(key: settingName) == boolean,
                    BoolCompare.NotEqual => GetBool(key: settingName) != boolean,
                    _                    => throw new ArgumentOutOfRangeException(),
                },
                SettingType.Floating => floatingCompare switch
                {
                    NumCompare.Equal    => Mathf.Approximately(a: GetFloat(key: settingName), b: floating),
                    NumCompare.NotEqual => !Mathf.Approximately(a: GetFloat(key: settingName), b: floating),
                    NumCompare.Gt       => GetFloat(key: settingName) >  floating,
                    NumCompare.LT       => GetFloat(key: settingName) <  floating,
                    NumCompare.Ge       => GetFloat(key: settingName) >= floating,
                    NumCompare.Le       => GetFloat(key: settingName) <= floating,
                    _                   => throw new ArgumentOutOfRangeException(),
                },
                SettingType.Integer => integerCompare switch
                {
                    NumCompare.Equal    => GetInt(key: settingName) == integer,
                    NumCompare.NotEqual => GetInt(key: settingName) != integer,
                    NumCompare.Gt       => GetInt(key: settingName) >  integer,
                    NumCompare.LT       => GetInt(key: settingName) <  integer,
                    NumCompare.Ge       => GetInt(key: settingName) >= integer,
                    NumCompare.Le       => GetInt(key: settingName) <= integer,
                    _                   => throw new ArgumentOutOfRangeException(),
                },
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
    
    // [CustomEditor(inspectedType: typeof(HasChangedSetting))]
    // public class HasChangedSettingEditor : Editor
    // {
    //     private SerializedProperty _settingNameProp;
    //     private SerializedProperty _settingTypeProp;
    //     private SerializedProperty _booleanCompareProp;
    //     private SerializedProperty _booleanProp;
    //     private SerializedProperty _floatingCompareProp;
    //     private SerializedProperty _floatingProp;
    //     private SerializedProperty _integerCompareProp;
    //     private SerializedProperty _integerProp;
    //
    //     private VisualElement _root;
    //
    //     private void OnEnable()
    //     {
    //         _settingNameProp     = serializedObject.FindProperty(propertyPath: "settingName");
    //         _settingTypeProp     = serializedObject.FindProperty(propertyPath: "settingType");
    //         _booleanCompareProp  = serializedObject.FindProperty(propertyPath: "booleanCompare");
    //         _booleanProp         = serializedObject.FindProperty(propertyPath: "boolean");
    //         _floatingCompareProp = serializedObject.FindProperty(propertyPath: "floatingCompare");
    //         _floatingProp        = serializedObject.FindProperty(propertyPath: "floating");
    //         _integerCompareProp  = serializedObject.FindProperty(propertyPath: "integerCompare");
    //         _integerProp         = serializedObject.FindProperty(propertyPath: "integer");
    //
    //         CreateUIElements();
    //     }
    //
    //     private void CreateUIElements()
    //     {
    //         _root = new VisualElement
    //         {
    //             style =
    //             {
    //                 flexDirection = FlexDirection.Column,
    //                 marginTop     = 10,
    //             },
    //         };
    //         
    //         // Setting Name
    //         TextField __settingNameField = new (label: "Setting Name");
    //         __settingNameField.BindProperty(property: _settingNameProp);
    //         _root.Add(child: __settingNameField);
    //
    //         // Setting Type
    //         EnumField __settingTypeField = new (label: "Setting Type", defaultValue: (SettingType)_settingTypeProp.enumValueIndex);
    //         __settingTypeField.RegisterValueChangedCallback(callback: evt =>
    //                                                         {
    //                                                             _settingTypeProp.enumValueIndex = Convert.ToInt32(evt.newValue);
    //                                                             CreateUIElements(); // Recreate UI elements when setting type changes
    //                                                         }
    //                                                        );
    //         _root.Add(child: __settingTypeField);
    //
    //         SettingType __settingType = (SettingType)_settingTypeProp.enumValueIndex;
    //
    //         switch (__settingType)
    //         {
    //             case SettingType.Boolean:
    //                 // Boolean Compare
    //                 EnumField __booleanCompareField = new (label: "Boolean Compare", defaultValue: (BoolCompare)_booleanCompareProp.enumValueIndex);
    //                 __booleanCompareField.RegisterValueChangedCallback(callback: evt => _booleanCompareProp.enumValueIndex = Convert.ToInt32(evt.newValue));
    //                 _root.Add(child: __booleanCompareField);
    //
    //                 // Boolean Value
    //                 Toggle __booleanToggle = new (label: "Boolean Value");
    //                 __booleanToggle.BindProperty(property: _booleanProp);
    //                 _root.Add(child: __booleanToggle);
    //                 break;
    //             case SettingType.Floating:
    //                 // Floating Compare
    //                 EnumField __floatingCompareField = new (label: "Floating Compare", defaultValue: (NumCompare)_floatingCompareProp.enumValueIndex);
    //                 __floatingCompareField.RegisterValueChangedCallback(callback: evt => _floatingCompareProp.enumValueIndex = Convert.ToInt32(evt.newValue));
    //                 _root.Add(child: __floatingCompareField);
    //
    //                 // Floating Value
    //                 FloatField __floatingField = new (label: "Floating Value");
    //                 __floatingField.BindProperty(property: _floatingProp);
    //                 _root.Add(child: __floatingField);
    //                 break;
    //             case SettingType.Integer:
    //                 // Integer Compare
    //                 EnumField __integerCompareField = new (label: "Integer Compare", defaultValue: (NumCompare)_integerCompareProp.enumValueIndex);
    //                 __integerCompareField.RegisterValueChangedCallback(callback: evt => _integerCompareProp.enumValueIndex = Convert.ToInt32(evt.newValue));
    //                 _root.Add(child: __integerCompareField);
    //
    //                 // Integer Value
    //                 IntegerField __integerField = new (label: "Integer Value");
    //                 __integerField.BindProperty(property: _integerProp);
    //                 _root.Add(child: __integerField);
    //                 break;
    //             default:
    //                 break;
    //         }
    //
    //         // Button
    //         Button __checkButton = new (clickEvent: CheckButtonClicked)
    //         {
    //             text = "Check Has Changed To Correct Value",
    //         };
    //         _root.Add(child: __checkButton);
    //
    //         _root.Bind(obj: serializedObject);
    //     }
    //
    //     private void CheckButtonClicked()
    //     {
    //         HasChangedSetting __hasChangedSetting = (HasChangedSetting)target;
    //         __hasChangedSetting.CheckHasChangedToCorrectValue();
    //     }
    //
    //     public override VisualElement CreateInspectorGUI()
    //     {
    //         CreateUIElements();
    //         return _root;
    //     }
    // }
    
    #endif
}
