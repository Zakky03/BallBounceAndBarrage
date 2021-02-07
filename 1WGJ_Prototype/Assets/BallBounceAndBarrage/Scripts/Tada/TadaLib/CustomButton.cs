using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TadaLib
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class CustomButton : MonoBehaviour
    {
        private UnityEngine.UI.Button button_;

        public bool IsPushed { private set; get; }

        // Start is called before the first frame update
        void Start()
        {
            button_ = GetComponent<UnityEngine.UI.Button>();
            //button_.onClick.AddListener(OnButtonClicked);
            IsPushed = false;
        }

        public void OnButtonClicked()
        {
            IsPushed = true;
        }

        public void OnButtonReleased()
        {
            IsPushed = false;
        }
    }
}