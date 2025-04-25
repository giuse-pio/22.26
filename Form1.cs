using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _22._26
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                return;
            }
            else if (e.KeyChar == 13)
            {
                btnConv_Click(sender, e);
                e.Handled = true;
            }
            else if (char.IsDigit(e.KeyChar))
            {
                return;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnConv_Click(object sender, EventArgs e)
        {
            if (txtNum.Text == "")
            {
                return;
            }
            var numConv = converti(Convert.ToInt32(txtNum.Text));
            lstConv.Items.Add(txtNum.Text + "\t" + numConv);
            Console.WriteLine(txtNum.Text + "\t" + numConv);
            txtNum.Text = "";
            txtNum.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtNum.Text = "";
            lstConv.Items.Clear();
            txtNum.Focus();
        }
        string converti(int num)
        {
            List<int> cifre = new List<int>();
            int numConv = num;
            while (numConv > 0)
            {
                int cifra = numConv % 10;
                numConv = numConv / 10;
                cifre.Add(cifra);
            }
            string milioni;
            switch (cifre.Count)
            {
                case 1:
                    return convDU(0, cifre[0]);
                case 2:
                    return convDU(cifre[1], cifre[0]);
                case 3:
                    return convCDU(cifre[2], cifre[1], cifre[0]);
                case 4:
                    return
                        convM(0, 0, cifre[3]) +
                        convCDU(cifre[2], cifre[1], cifre[0]);
                case 5:
                    return
                        convM(0, cifre[4], cifre[3]) +
                        convCDU(cifre[2], cifre[1], cifre[0]);
                case 6:
                    return
                        convM(cifre[5], cifre[4], cifre[3]) +
                        convCDU(cifre[2], cifre[1], cifre[0]);
                case 7:
                    if (cifre[6] == 1)
                        milioni = "un milione";
                    else
                        milioni = convDU(0, cifre[6]) + " milioni";
                    return
                        milioni +
                        convM(cifre[5], cifre[4], cifre[3]) +
                        convCDU(cifre[2], cifre[1], cifre[0]);
                case 8:
                    milioni = convDU(cifre[7], cifre[6]) + " milioni";
                    return
                        milioni +
                        convM(cifre[5], cifre[4], cifre[3]) +
                        convCDU(cifre[2], cifre[1], cifre[0]);
                default:
                    return "";
            }
        }
        string convDU(int dec, int uni)
        {
            string[] testiNum0_19 = { "zero", "uno", "due", "tre", "quattro", "cinque",
                "sei", "sette", "otto", "nove", "dieci", "undici", "dodici", "tredici",
                "quattordici", "quindici", "sedici", "diciassette", "diciotto", "diciannove" };
            string[] testiDecine = { "venti", "trenta", "quaranta", "cinquanta", "sessanta",
                "settanta", "ottanta", "novanta" };
            int valore = dec * 10 + uni;
            if (valore < 20)
            {
                return testiNum0_19[valore];
            }
            else if (uni == 1 || uni == 8)
            {
                return testiDecine[dec].Substring(0, testiDecine[dec].Length - 1) + testiNum0_19[uni];
            }
            else
            {
                return testiDecine[dec] + testiNum0_19[uni];
            }
        }
        string convCDU(int cent, int dec, int uni)
        {
            string[] testiCentinaia = { "cento", "duecento", "trecento", "quattrocento",
                "cinquecento", "seicento", "settecento", "ottocento", "novecento" };
            return testiCentinaia[cent] +
                convDU(dec, uni);
        }
        string convM(int cent, int dec, int uni)
        {
            if (cent == 0 && dec == 0 && uni == 0)
                return "";
            else if (cent == 0 && dec == 0 && uni == 1)
                return "mille";
            else
                return convCDU(cent, dec, uni) + " mila";
        }
    }
}
