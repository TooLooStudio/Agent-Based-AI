using UnityEngine;

namespace TooLoo.AI
{
    public class DataManager : MonoBehaviour
    {
        [Header("Resources Sub Folder")]
        public string loadFolder = "";

        private void Awake()
        {
            Load();
        }

        private void Load()
        {
            ActionLogic.Load(loadFolder);
        }
    }
}