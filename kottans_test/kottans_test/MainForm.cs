using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kottans_test
{
    public partial class Form1 : Form
    {
        static CardChecker cardChecker = new CardChecker();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";
            string cardNumber = cardChecker.VerifyAndNormalizeInput(maskedTextBox1.Text);
            if (cardNumber != "")
            {
                result += "Card issuer is "+cardChecker.GetCreditCardVendor(cardNumber)+"\n";
                if (cardChecker.IsCreditCardNumberValid(cardNumber))
                {
                    result += "Luhn check shows that card number is valid\n";
                }
                else result += "Luhn check shows that card number is invalid\n";
                result += "Next valid card number is - " + cardChecker.GenerateNextCreditCardNumber(cardNumber);
            }
            else result = "card number needs to be between 12 and 19 digits";
            label3.Text = result;
        }
    }
}
