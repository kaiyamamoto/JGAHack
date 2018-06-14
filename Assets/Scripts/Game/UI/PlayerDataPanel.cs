using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using System.Linq;


namespace Play
{
    public class PlayerDataPanel :MonoBehaviour
    {

       

        //アイコンセット
        [SerializeField, ReadOnly]
        private GameObject[] _icons;
        //変更用アイコンリスト
        [SerializeField, ReadOnly]
        private Sprite[] _iconImages;

        private void Awake()
        {
            //子供のセット
            _icons = transform.GetAllChild();
            //リソースからアイコンゲット
            _iconImages = EffectManager.Instance.GetConsoleIcons();
        }
        // Use this for initialization
        void Start()
        {
            IconReset();
        }

        public void SetIcon(int slotNum, string typeName ,Direction dir)
        {
            if (typeName == "DiectionTest")
            {
                switch (dir)
                {
                    case Direction.Back:
                        _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Direction_Down];
                        break;

                    case Direction.Left:
                        _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Direction_Left];
                        break;

                    case Direction.Right:
                        _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Direction_Right];
                        break;

                    case Direction.Front:
                        _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Direction_Up];
                        break;

                    default:

                        break;
                }
            }

            if (typeName == "TestShot")
            {
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Shot];
            }

            if (typeName == "Tackle")
            {
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Tackle];
            }

            if (typeName == "RideFloor")
            {
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.RideOn];
            }


            if (typeName == "SideMove")
            {
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Side];
            }

            if (typeName == "Stay")
            {
                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Stop];
            }

            if (typeName == "UpDownMove")
            {

                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Updown];
            }


            if (typeName == "Nodata")
            {

                _icons[slotNum].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Nodata];
            }
        }

        public void IconReset()
        {
          
            for (int i = 0; i < _icons.Length; i++)
            {
                _icons[i].GetComponent<Image>().sprite = _iconImages[(int)CONSOLE_ICON_ID.Nodata];
            }        
        }

    }
}