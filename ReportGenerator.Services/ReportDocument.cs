using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Services
{
    public class ReportDocument
    {
        
        private readonly Dictionary<string, string> _reportData = new Dictionary<string, string>();

        public static List<string> SplitText(string originalText, int maxNoChars = 254)
        {
            var chunks = new List<string>();

            for (int i = 0; i < originalText.Length; i += maxNoChars)
            {
                if (i + maxNoChars > originalText.Length)
                {
                    maxNoChars = originalText.Length - i;
                }

                chunks.Add(originalText.Substring(i, maxNoChars));
            }

            return chunks;
        }

        public void AttachTextToTags(IList<string> orderedTags, string text, bool splitText = false)
        {
            var textChunks = new List<string>() { text };

            if (splitText)
            {
                textChunks = SplitText(text);
            }

            //if (textChunks.Count != orderedTags.Count())
            //{
            //    throw new ArgumentException("Text chunks count not equal to tags count");
            //}

            for (int tagIndex = 0; tagIndex < orderedTags.Count(); tagIndex++)
            {
                var currentTextChunk = string.Empty;

                if (textChunks.Count > tagIndex)
                {
                    currentTextChunk = textChunks[tagIndex];
                }

                _reportData.Add(orderedTags[tagIndex], currentTextChunk);
            }
        }

        private void FindAndReplace(Application wordApp, object ToFindText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllforms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref ToFindText,
                ref matchCase,
                ref matchWholeWord,
                ref matchWildCards,
                ref matchSoundLike,
                ref nmatchAllforms,
                ref forward,
                ref wrap,
                ref format,
                ref replaceWithText,
                ref replace,
                ref matchKashida,
                ref matchDiactitics,
                ref matchAlefHamza,
                ref matchControl);
        }

        public void CreateWordDocument(object templateFilename, object newFilename)
        {
            Application wordApp = new Application();
            object missing = Missing.Value;
            Document myWordDoc = null;

            if (File.Exists((string)templateFilename))
            {
                object readOnly = false;
                object isVisible = false;
                wordApp.Visible = false;

                myWordDoc = wordApp.Documents.Open(ref templateFilename,
                    ref missing,
                    ref readOnly,
                    ref missing,
                    ref missing,
                    ref missing,
                    ref missing,
                    ref missing,
                    ref missing,
                    ref missing,
                    ref missing,
                    ref missing,
                    ref missing,
                    ref missing,
                    ref missing,
                    ref missing);

                myWordDoc.Activate();

                //find and replace


                foreach (var reportDatum in _reportData)
                {
                    FindAndReplace(wordApp, reportDatum.Key, reportDatum.Value);
                }

                object replaceAll = WdReplace.wdReplaceAll;

                foreach (Section section in myWordDoc.Sections)
                {
                    foreach (HeaderFooter header in section.Headers)
                    {
                        Microsoft.Office.Interop.Word.Range headerRange = header.Range;

                        headerRange.Find.Text = TemplateTags.ReportNum;
                        headerRange.Find.Replacement.Text = _reportData[TemplateTags.ReportNum];
                        headerRange.Find.Execute(ref missing,
                            ref missing,
                            ref missing,
                            ref missing,
                            ref missing,
                            ref missing,
                            ref missing,
                            ref missing,
                            ref missing,
                            ref missing,
                            ref replaceAll,
                            ref missing,
                            ref missing,
                            ref missing,
                            ref missing);
                    }
                }
            }

            else
            {
                Console.WriteLine("File not Found!");
            }

            //Save as
            myWordDoc.SaveAs2(ref newFilename,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing,
                ref missing);

            myWordDoc.Close();
            wordApp.Quit();
            Console.WriteLine("File Created!");
        }
    }
}
