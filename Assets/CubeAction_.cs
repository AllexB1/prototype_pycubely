using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using System.Linq;

public class CubeAction_ : MonoBehaviour {

        [TextArea(3, 10)]
        public string template;
        private Dictionary<string, Transform> dropzones;


        private string TranslateChild(string name, string templateIdent)
        {
            Transform child = dropzones[name];
            foreach (Transform t in child)
            {
                var ca = t.GetComponent<CubeAction_>();
                if (ca)
                {
                    string translated = ca.Translate();
                    string[] lines = Regex.Split(translated, @"[\r]?\n");
                    string[] identedLines = lines.Where(x => x.Trim().Length > 0).Select((x, i) => i == 0 ? x : templateIdent + x).ToArray<string>();
                    return String.Join(Environment.NewLine, identedLines);
                }
            }
            return "";
        }

        internal string Translate(string translated=null)
        {
            if(translated == null)
            {
                translated = template;
            }

            translated = translated.Replace("${id}", System.String.Format("\"{0}\"", this.gameObject.GetInstanceID()));

            foreach (Match m in Regex.Matches(translated, @"([ ]*)(\$\{(\w+)\})"))
            {
                string toTranslate = m.Groups[2].Value;
                string dropzoneName = m.Groups[3].Value;
                string templateIdent = m.Groups[1].Value;
                translated = translated.Replace(toTranslate, this.TranslateChild(dropzoneName, templateIdent));
            }
            return translated;
        }

        public void Run()
        {
            Run(false);
        }

        public void Run(bool noWait)
        {
            //GameObject pyio = GameObject.Find("PyInterpreter");
           // PythonScript ps = pyio.GetComponent<PythonScript>();
            //ps.Stop();
            //ps.LoadScript(Translate(), transform);
            //ps.Play(false, 0, noWait);
        }
}
