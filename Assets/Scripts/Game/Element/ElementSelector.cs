using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Play.Element;
using Extensions;

using System.Reflection;

namespace Play
{
    // 要素オブジェクトの選択と要素の移動用クラス
    public class ElementSelector : MonoBehaviour
    {
        // 選択しているオブジェクト
        [SerializeField, ReadOnly]
        private ElementObject _selectObject = null;

        // TODO:選択したオブジェクトの要素プレハブ
        [SerializeField]
        private Text _elementText;
        [SerializeField, ReadOnly]
        private Text[] _textList = null;

        // 選択している要素のインデックス
        private int _selectElement = -1;

        void Start()
        {
            // TODO: テキストリスト作成
            int num = (int)ElementType.length;
            _textList = new Text[num];
        }

        void Update()
        {
            // 選択
            SelectObject();

            // 解除
            if (Input.GetMouseButtonDown(1))
            {
                ObjectRelease();
            }

            if (Input.GetMouseButtonDown(2))
            {
                // TODO: 仮で選択を進める
                var select = _selectElement;
                _selectElement = SearchSelectElement(select);
                SelectUpdate();
            }
        }

        /// <summary>
        /// オブジェクトを選択
        /// </summary>
        private void SelectObject()
        {
            // オブジェクトを選択していない
            var select = GetClickObject();
            if (select)
            {
                // 選択したオブジェクトがある
                var elementObj = select.GetComponent<ElementObject>();
                if (elementObj)
                {
                    SelectElementObject(elementObj);
                }
            }
        }

        /// <summary>
        /// 要素オブジェクトを選択したときの処理
        /// </summary>
        /// <param name="elementObj"></param>
        private void SelectElementObject(ElementObject elementObj)
        {
            if (elementObj == null)
            {
                // エレメントオブジェクトがない場合は何もしない
                return;
            }

            if (_selectObject == null)
            {
                // 既に選択されていないときは選択
                _selectObject = elementObj;
                // TODO: テキスト追加
                AddText(_selectObject.ElementList);
            }
            else
            {
                // されている場合は要素の移動
                MoveElement(elementObj);
                // TODO: テキスト削除
                DestroyText();
            }

        }

        // TODO: 要素テキスト削除
        private void DestroyText()
        {
            // テキスト削除
            foreach (var child in transform.GetAllChild())
            {
                GameObject.Destroy(child);
            }
        }

        // TODO: 要素テキスト追加
        private void AddText(ElementBase[] elements)
        {
            // TODO:初期要素を選択状態に
            foreach (var element in _selectObject.ElementList)
            {
                if (element == null)
                {
                    continue;
                }
                _selectElement = (int)element.Type;
            }

            // テキスト削除
            float y = 0.0f;
            foreach (var element in elements)
            {
                if (element == null)
                {
                    continue;
                }

                var type = element.Type;

                // 子に要素追加
                var pos = new Vector3(0.0f, y, 0.0f);
                var text = GameObject.Instantiate(_elementText);
                this.gameObject.transform.SetChild(text.gameObject);
                text.transform.localPosition = pos;

                // テキスト変更
                text.text = type.ToString();

                _textList[(int)type] = text;

                y -= 30.0f;
            }

            // 選択更新時処理
            SelectUpdate();
        }

        // TODO: 選択の更新処理
        private void SelectUpdate()
        {
            if (IsSelectElementEmpty())
            {
                // 選択されている要素がない場合はなにもしない
                return;
            }

            foreach (var text in _textList)
            {
                if (text == null)
                {
                    continue;
                }
                text.fontSize = 25;
            }

            // TODO: 仮ででかくする
            _textList[_selectElement].fontSize = 30;
        }

        /// <summary>
        /// 選択されている要素がない？
        /// </summary>
        /// <returns></returns>
        private bool IsSelectElementEmpty()
        {
            if (_selectElement == -1)
            {
                // 見つからなかったら表示しない
                return true;
            }
            return false;
        }

        // 選択できる要素まで探す再起関数
        private int SearchSelectElement(int index)
        {
            int select = index;
            select++;
            if ((int)ElementType.length <= select)
            {
                select = 0;
            }

            if (_selectObject.ElementList[select] == null)
            {
                select = SearchSelectElement(select);
            }
            return select;
        }

        /// <summary>
        /// 要素の移動
        /// </summary>
        /// <param name="selectObj"></param>
        private void MoveElement(ElementObject selectObj)
        {
            if (IsSelectElementEmpty())
            {
                // 選択されている要素がない場合はなにもしない
                return;
            }

            var element = _selectObject.ElementList[_selectElement];
            // 要素のコピー
            selectObj.CopyComponent(element);

            selectObj.ElementUpdate();

            // 選択解除
            ObjectRelease();
        }

        /// <summary>
        /// 選択したオブジェクトを解除
        /// </summary>
        private void ObjectRelease()
        {

            _selectObject = null;
            _selectElement = -1;
        }

        /// <summary>
        /// 左クリックしたオブジェクトを取得 
        /// </summary>
        /// <returns></returns>
        private GameObject GetClickObject()
        {
            GameObject result = null;

            // 左クリックされた場所のオブジェクトを取得
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
                if (collition2d)
                {
                    result = collition2d.transform.gameObject;
                }
            }
            return result;
        }
    }
}