using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player {
    public class PlayerSodaUIController : PlayerComponent {
        /* Fields */
        [Header("UI")]
        [SerializeField] Canvas canvas;
        [SerializeField] Image amountImage;

        [Header("Components")]
        [SerializeField] PlayerSodaManager sodaManager;

        //-------------------------------------------------------------------
        /* Properties */

        //-------------------------------------------------------------------
        /* Events */
        void Awake()
        {

        }

        void Update()
        {
            canvas.transform.rotation = Quaternion.identity;
        }

        void FixedUpdate()
        {
            amountImage.fillAmount = sodaManager.PowerRate;
        }

        //-------------------------------------------------------------------
        /* Methods */

    }
}