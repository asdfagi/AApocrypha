using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class ElVibrato
    {
        private static readonly List<char> valid = [
            'A', 'B', 'C', 'D', 'E', 'F',
            'G', 'H', 'I', 'J', 'K', 'L',
            'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'W', 'X',
            'Y', 'Z'
        ];
        private static readonly Dictionary<int, string> keyWordPairs = new Dictionary<int, string>
        {
            {0, "HO"},
            {1, "PA"},
            {2, "ZAK"},
            {3, "ZEVE"},
            {4, "NI"},
            {5, "NOK"},
            {6, "KUZ"},
            {7, "GA"},
            {8, "NO"},
            {9, "CHA"},
            {10, "PU"},
            {11, "FU"},
            {12, "LA"},
            {13, "STA"},
            {14, "SOM"},
            {15, "BU"},
            {16, "AN"},
            {17, "CHO"},
            {18, "TA"},
            {19, "KRO"},
            {20, "BE"},
        };
        private static readonly Dictionary<char, int> letterValues = new Dictionary<char, int>
        {
            {'A', 1},
            {'B', 2},
            {'C', 3},
            {'D', 4},
            {'E', 5},
            {'F', 6},
            {'G', 7},
            {'H', 8},
            {'I', 9},
            {'J', 10},
            {'K', 11},
            {'L', 12},
            {'M', 13},
            {'N', 14},
            {'O', 15},
            {'P', 16},
            {'Q', 17},
            {'R', 18},
            {'S', 19},
            {'T', 20},
            {'U', 21},
            {'V', 22},
            {'W', 23},
            {'X', 24},
            {'Y', 25},
            {'Z', 26},
        };

        // TRANSLATE WORD BLOCK
        // EXPLANATION AND EXAMPLES: wiki.kingdomofloathing.com/El_Vibrato_language#Translation
        private static string TranslateWord(string input)
        {
            if (input == null || input.Length < 2 || input.Length > 4) { return input; }
            int blockValue = 0;
            foreach (char ch in input.ToUpper())
            {
                if (!valid.Contains(ch)) { return input.ToUpper(); }
                blockValue += letterValues[ch];
            }
            return keyWordPairs[blockValue % 21];
        }

        public static string Translate(string input)
        {
            // SPLIT INPUT INTO SENTENCE BLOCKS
            List<string> sentenceGroup = [];
            string sentenceBuffer = "";
            foreach (char character1 in input.ToUpper())
            {
                if (valid.Contains(character1))
                {
                    sentenceBuffer += character1;
                }
                else
                {
                    sentenceGroup.Add(sentenceBuffer);
                    sentenceGroup.Add(character1.ToString());
                    sentenceBuffer = "";
                }
            }
            if (sentenceBuffer != "")
            {
                sentenceGroup.Add(sentenceBuffer);
                sentenceBuffer = "";
            }

            // DEBUG - PRINT SENTENCE BLOCKS
            /*Debug.Log("El Vibrato | sentence blocks of " + input);
            foreach (string sentence in sentenceGroup)
            {
                Debug.Log(sentence);
            }*/

            // SPLIT SENTENCE BLOCKS INTO WORD BLOCKS
            bool wordBlockSorted = false;
            List<string> wordBlockGroup = [];
            if (sentenceGroup.Count <= 0) { return "ANZEVE BELA"; } // SYNTAX ERROR
            if (sentenceGroup.Count == 1)
            {
                if (sentenceGroup[0].Length <= 4) { 
                    wordBlockGroup.Add(sentenceGroup[0]);
                    wordBlockSorted = true;
                }
            }
            if (!wordBlockSorted)
            {
                foreach (string sentence in sentenceGroup)
                {
                    int groupSize = 0;
                    string groupBuffer = "";
                    int totalSize = 0;
                    if (sentence.Length <= 4) { 
                        wordBlockGroup.Add(sentence);
                        continue;
                    }
                    foreach (char ch in sentence)
                    {
                        groupBuffer += ch;
                        groupSize++;
                        totalSize++;
                        if (groupSize >= 3 && totalSize != sentence.Length - 1)
                        {
                            wordBlockGroup.Add(groupBuffer);
                            groupBuffer = "";
                            groupSize = 0;
                        }
                        if (groupSize >= 4 && totalSize == sentence.Length)
                        {
                            wordBlockGroup.Add(groupBuffer);
                            groupBuffer = "";
                            groupSize = 0;
                        }
                        if (groupSize >= 2 && totalSize == sentence.Length)
                        {
                            wordBlockGroup.Add(groupBuffer);
                            groupBuffer = "";
                            groupSize = 0;
                        }
                    }
                }
                wordBlockSorted = true;
            }

            // DEBUG - PRINT WORD BLOCKS
            /*Debug.Log("El Vibrato | word blocks of " + input);
            foreach (string wordBlock in wordBlockGroup)
            {
                Debug.Log(wordBlock);
            }*/

            // TRANSLATE AND RETURN
            string output = "";
            foreach (string wordBlock in wordBlockGroup)
            {
                output += TranslateWord(wordBlock);
            }
            //Debug.Log(output);
            return output;
        }

        public static void TranslationTester()
        {
            Debug.Log("El Vibrato Translation Tester | initialized!");
            Debug.Log("1 | input: repair - expected: TAGA - result: " + Translate("repair"));
            Debug.Log("1 | input: buff - expected: SOM - result: " + Translate("buff"));
            Debug.Log("1 | input: drone - expected: ANKRO - result: " + Translate("drone"));
            Debug.Log("1 | input: function not possible. - expected: BEFUNO GA NOCHACHO. - result: " + Translate("function not possible."));
            Debug.Log("1 | input: (empty string) - expected: ANZEVE BELA - result: " + Translate(""));
            Debug.Log("1 | input: modification field - expected: FUZEVEZEVECHO BEAN - result: " + Translate("modification field"));
            Debug.Log("1 | input: FUNCTION ACCEPTED: BUILD. - expected: BEFUNO GABECHA: FUAN. - result: " + Translate("FUNCTION ACCEPTED: BUILD."));
            //Debug.Log("1 | input:  - expected:  - result: " + Translate(""));
        }
    }
}
