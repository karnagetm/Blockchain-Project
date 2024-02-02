using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainAssignment
{
    public partial class BlockchainApp : Form
    {
        Blockchain blockchain;

        public BlockchainApp()
        {
            InitializeComponent();
            blockchain = new Blockchain();
            richTextBox1.Text = "Blockchain Initialised";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox1.Text, out int index))
                richTextBox1.Text = blockchain.getBlock(index);
            else
                richTextBox1.Text = "Not a number";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String privKey;
            Wallet.Wallet myNewWallet = new Wallet.Wallet(out privKey);
            publicKey.Text = myNewWallet.publicID;
            this.privKey.Text = privKey;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Wallet.Wallet.ValidatePrivateKey(privKey.Text, publicKey.Text))
            {
                richTextBox1.Text = "Keys are valid";
            } 
            else
            {
                richTextBox1.Text = "Keys are invalid";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Transaction newTransaction = new Transaction(publicKey.Text, receiver.Text, Double.Parse(amount.Text), Double.Parse(fee.Text), privKey.Text);
            blockchain.transactionPool.Add(newTransaction);
            richTextBox1.Text = newTransaction.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Block newBlock = new Block(blockchain.getLastBlock(), blockchain.GetPendingTransactions(), publicKey.Text);
            blockchain.Blocks.Add(newBlock);
            richTextBox1.Text = newBlock.ToString();
        }

      
    }
}
