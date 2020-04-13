/** EWBase.cs
 *  Author:         bagaking <kinghand@foxmail.com>
 *  CreateTime:     2019/10/09 21:41:50
 *  Copyright:      (C) 2019 - 2029 bagaking, All Rights Reserved
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniKh.core;
using UniKh.extensions;
using UnityEditor;

namespace UniKh.editor {
    public abstract class EWBase<T> : EditorWindow where T : EWBase<T> {

        public static T GetWindow(string title = "UniKh - New Window") {
            var window = GetWindow<T>();
            window.titleContent = new GUIContent(title);
            window.Focus();
            window.Repaint();
            return window;
        }

        [System.NonSerialized]
        private bool _initiated = false;

        protected virtual void OnEnable() {
            _initiated = Initial();
            Repaint();
        }


        protected virtual void OnGUI() {
            if (!_initiated) return;
            
            HandleBasicEvent(Event.current);
            EditorUtils.Render.BeginSandBox();
            GUIProc(Event.current);
            EditorUtils.Render.EndSandBox();
        }

        public virtual bool Initial() { return true; }

        public abstract void GUIProc(Event e);


        public const float StandardItemHeight = 18;

        private bool _firstLayoutExecuted = false;

        public Vector2 MouseDownPos { get; private set; }

        public Vector2 MouseCurrentPos { get; private set; }

        public Vector2 MouseDelta => MouseCurrentPos - MouseDownPos;

        public bool MouseDown { get; private set; }

        public void HandleBasicEvent(Event e) {
            MouseCurrentPos = e.mousePosition;
            switch (e.type) {
                case EventType.MouseDown:
                    MouseDown = true;
                    MouseDownPos = MouseCurrentPos; break;
                case EventType.MouseUp:
                    MouseDown = false; break;
            }

        }

        public void SendWindowEvent(string eventName) {
            SendEvent(EditorGUIUtility.CommandEvent(eventName));
        }





    }
}
