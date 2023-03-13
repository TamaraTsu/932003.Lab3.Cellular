using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TsurovaTamaraLab3Cellular
{
    public partial class Form1 : Form
    {
        private int generationNumber;
        private bool[] currentGeneration = new bool[NUM_CELLS + 2];
        private bool[] rules = new bool[8];

        const int NUM_CELLS = 20;

        Random random = new Random();
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < NUM_CELLS; i++)
            {
                dataGridView1.Columns.Add("", "");
            }
        }
        private void btStart_Click_1(object sender, EventArgs e)
        {
            StartGame();
        }

        private void Restart()
        {
            dataGridView1.Rows.Clear();
            generationNumber = 0;
            for (int i = 0; i < NUM_CELLS + 2; i++)
                currentGeneration[i] = false;
            for (int i = 0; i < 8; i++)
                rules[i] = false;
            StartGame();
        }

        private void StartGame()
        {
            for (int i = 0; i < NUM_CELLS; i++)
                if (random.NextDouble() <= 0.5)
                    currentGeneration[i + 1] = true;

            currentGeneration[0] = currentGeneration[NUM_CELLS];
            currentGeneration[NUM_CELLS + 1] = currentGeneration[1];

            SetRule();
            timer1.Start();
        }

        private void SetRule()
        {
            string binaryNumber = Convert.ToString((short)edRule.Value, 2);
            int length = binaryNumber.Length;

            for (int i = 0; i < 8 - length; i++)
                binaryNumber = '0' + binaryNumber;

            for (int i = 0; i < 8; i++)
                if (binaryNumber[i] == '1')
                    rules[i] = true;

        }

        private void NewGeneration()
        {
            bool[] tempGeneration = new bool[NUM_CELLS + 2];
            for (int i = 0; i < NUM_CELLS; i++)
            {
                var comb = GetCombination(i + 1);
                int numberOfRule = Convert.ToInt32(comb, 2);
                tempGeneration[i + 1] = rules[numberOfRule];
            }
            tempGeneration[0] = tempGeneration[NUM_CELLS];
            tempGeneration[NUM_CELLS + 1] = tempGeneration[1];

            currentGeneration = tempGeneration;
            generationNumber++;
        }

        private string GetCombination(int i)
        {
            string comb;
            if (currentGeneration[i - 1]) comb = "1";
            else comb = "0";
            if (currentGeneration[i]) comb += "1";
            else comb += "0";
            if (currentGeneration[i + 1]) comb += "1";
            else comb += "0";

            return comb;
        }

        private void CurrentGeneration()
        {
            dataGridView1.Rows.Add();
            for (int i = 0; i < NUM_CELLS; i++)
                if (currentGeneration[i + 1])
                    dataGridView1[i, generationNumber].Style.BackColor = Color.Blue;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            CurrentGeneration();
            NewGeneration();
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                Restart();
            }
            else timer1.Stop();

        }
    }
}

