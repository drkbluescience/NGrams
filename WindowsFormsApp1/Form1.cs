using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ExeNgrams();
            stopwatch.Stop();

            string executionTime = $"Execution Time: {stopwatch.ElapsedMilliseconds} ms";
            label5.Text = executionTime;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void ExeNgrams()
        {

            string[] dictionary1 = System.IO.File.ReadAllLines(@"BİLİM İŞ BAŞINDA.txt", System.Text.Encoding.GetEncoding("iso-8859-9"));
            string[] dictionary2 = System.IO.File.ReadAllLines(@"BOZKIRDA.txt", System.Text.Encoding.GetEncoding("iso-8859-9"));
            string[] dictionary3 = System.IO.File.ReadAllLines(@"DEĞİŞİM.txt", System.Text.Encoding.GetEncoding("iso-8859-9"));
            string[] dictionary4 = System.IO.File.ReadAllLines(@"DENEMELER.txt", System.Text.Encoding.GetEncoding("iso-8859-9"));
            string[] dictionary5 = System.IO.File.ReadAllLines(@"UNUTULMUŞ DİYARLAR.txt", System.Text.Encoding.GetEncoding("iso-8859-9"));

            string diction1 = string.Join(" ", dictionary1);
            string diction2 = string.Join(" ", dictionary2);
            string diction3 = string.Join(" ", dictionary3);
            string diction4 = string.Join(" ", dictionary4);
            string diction5 = string.Join(" ", dictionary5);

            List<string> dict1 = ParseDictionary(diction1);
            List<string> dict2 = ParseDictionary(diction2);
            List<string> dict3 = ParseDictionary(diction3);
            List<string> dict4 = ParseDictionary(diction4);
            List<string> dict5 = ParseDictionary(diction5);

            List<string> unigram1 = GenerateNgrams(1, dict1);
            List<string> bigram1 = GenerateNgrams(2, dict1);
            List<string> trigram1 = GenerateNgrams(3, dict1);

            List<string> unigram2 = GenerateNgrams(1, dict2);
            List<string> bigram2 = GenerateNgrams(2, dict2);
            List<string> trigram2 = GenerateNgrams(3, dict2);


            List<string> unigram3 = GenerateNgrams(1, dict3);
            List<string> bigram3 = GenerateNgrams(2, dict3);
            List<string> trigram3 = GenerateNgrams(3, dict3);


            List<string> unigram4 = GenerateNgrams(1, dict4);
            List<string> bigram4 = GenerateNgrams(2, dict4);
            List<string> trigram4 = GenerateNgrams(3, dict4);


            List<string> unigram5 = GenerateNgrams(1, dict5);
            List<string> bigram5 = GenerateNgrams(2, dict5);
            List<string> trigram5 = GenerateNgrams(3, dict5);

            label4.Text = "BİLİM İŞ BAŞINDA";
            textboxFill(unigram1, bigram1, trigram1);

        }
        public void textboxFill(List<string> unigram, List<string> bigram, List<string> trigram)
        {

            foreach (var item in unigram)
            {
                textBox1.Text += item + Environment.NewLine;
            }

            foreach (var item in bigram)
            {
                textBox2.Text += item + Environment.NewLine;
            }
            foreach (var item in trigram)
            {
                textBox3.Text += item + Environment.NewLine;
            }
        }
        public List<string> ParseDictionary(string dict)
        {
            List<string> dictionary = new List<string>();
            string[] splitDictionary = dict.Split(' ');
            for (int i = 0; i < splitDictionary.Length; i++)
            {
                string value = splitDictionary[i];
                value = value.Trim();
                value = value.Replace("\n", "");
                if (!string.IsNullOrEmpty(value) && !value.Any(char.IsDigit))
                {
                    dictionary.Add(TrimPunctuation(value));
                }
            }
            return dictionary;
        }
        static string TrimPunctuation(string value)
        {
            // Count start punctuation.
            int removeFromStart = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsPunctuation(value[i]))
                {
                    removeFromStart++;
                }
                else
                {
                    break;
                }
            }

            // Count end punctuation.
            int removeFromEnd = 0;
            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (char.IsPunctuation(value[i]))
                {
                    removeFromEnd++;
                }
                else
                {
                    break;
                }
            }
            // No characters were punctuation.
            if (removeFromStart == 0 &&
                removeFromEnd == 0)
            {
                return value;
            }
            // All characters were punctuation.
            if (removeFromStart == value.Length &&
                removeFromEnd == value.Length)
            {
                return "";
            }
            // Substring.
            return value.Substring(removeFromStart,
                value.Length - removeFromEnd - removeFromStart);
        }
        private List<string> GenerateNgrams(int N, List<string> tokens)
        {
            List<string> ngramList = new List<string>();

            //GENERATE THE N-GRAMS
            for (int k = 0; k < (tokens.Count - N + 1); k++)
            {
                string s = "";
                int start = k;
                int end = k + N;
                for (int j = start; j < end; j++)
                {
                    s = s + " " + tokens[j];
                }
                //Add n-gram to a list
                ngramList.Add(s);
            }
      
            return ngramList;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
}

