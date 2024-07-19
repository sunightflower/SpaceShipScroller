using System;
using UI.Dialogs;
using UnityEngine;
using System.Collections.Generic;

namespace UI
{
    public class DialogManager
    {
        private const string PrefabsFilePath = "Dialogs/";

        private static readonly Dictionary<Type, string> PrefabsDictionary = new()
        {
            {typeof(YouLoseDialog),"YouLoseDialog"},
            {typeof(YouWinDialog),"YouWinDialog"},
            {typeof(PurchaseItemDialog),"PurchaseItemDialog"},
            {typeof(MessageDialog),"MessageDialog"},
            {typeof(LoadingDialog),"LoadingDialog"},

            {typeof(SelectLevelDialog),"MenuDialogs/SelectLevelDialog"},
            {typeof(MenuDialog),"MenuDialogs/MenuDialog"},
            {typeof(ScoreTableDialog),"MenuDialogs/ScoreTableDialog"},
            {typeof(SettingsDialog),"MenuDialogs/SettingsDialog"},
            {typeof(CustomizeShipDialog),"MenuDialogs/CustomizeShipDialog"},
        };

        public static T ShowDialog<T>() where T : Dialog
        {
            var go = GetPrefabByType<T>();

            if (go == null)
            {
                Debug.LogError("Show window - object not found");
                return null;
            }

            return GameObject.Instantiate(go, GuiHolder);
        }

        private static T GetPrefabByType<T>() where T : Dialog
        {
            var prefabName = PrefabsDictionary[typeof(T)];
            if (string.IsNullOrEmpty(prefabName))       
                Debug.LogError("cant find prefab type of " + typeof(T) + "Do you added it in PrefabsDictionary?");
            

            var path = PrefabsFilePath + PrefabsDictionary[typeof(T)];
            var dialog = Resources.Load<T>(path);
            if (dialog == null)            
                Debug.LogError("Cant find prefab at path " + path);

            return dialog;
        }

        public static Transform GuiHolder
        {
            get { return ServiceLocator.Current.Get<GUIHolder>().transform; }
        }
    }
}

