using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BlockchainAssignment
{
    public partial class BlockchainApp : Form
    {
        // Global blockchain object
        private Blockchain blockchain;

        // Default App Constructor
        public BlockchainApp()
        {
            // Initialise UI Components
            InitializeComponent();

            // Populate mining preferences ComboBox
            miningPreferenceComboBox.Items.Add("Greedy");
            miningPreferenceComboBox.Items.Add("Altruistic");
            miningPreferenceComboBox.Items.Add("Random");
            miningPreferenceComboBox.Items.Add("Address Preference");

            miningPreferenceComboBox.SelectedIndex = 0; // Set default selection
            // Create a new blockchain 
            blockchain = new Blockchain();
            // Update UI with an initalisation message
            UpdateText("New blockchain initialised!");
        }

        /* PRINTING */
        // Helper method to update the UI with a provided message
        private void UpdateText(String text)
        {
            output.Text = text;
        }

        // Print entire blockchain to UI
        private void ReadAll_Click(object sender, EventArgs e)
        {
            UpdateText(blockchain.ToString());
        }

        // Print Block N (based on user input)
        private void PrintBlock_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(blockNo.Text, out int index))
                UpdateText(blockchain.GetBlockAsString(index));
            else
                UpdateText("Invalid Block No.");
        }

        // Print pending transactions from the transaction pool to the UI
        private void PrintPendingTransactions_Click(object sender, EventArgs e)
        {
            UpdateText(String.Join("\n", blockchain.transactionPool));
        }

        /* WALLETS */
        // Generate a new Wallet and fill the public and private key fields of the UI
        private void GenerateWallet_Click(object sender, EventArgs e)
        {
            Wallet.Wallet myNewWallet = new Wallet.Wallet(out string privKey);

            publicKey.Text = myNewWallet.publicID;
            privateKey.Text = privKey;
        }

        // Validate the keys loaded in the UI by comparing their mathematical relationship
        private void ValidateKeys_Click(object sender, EventArgs e)
        {
            if (Wallet.Wallet.ValidatePrivateKey(privateKey.Text, publicKey.Text))
                UpdateText("Keys are valid");
            else
                UpdateText("Keys are invalid");
        }

        // Check the balance of current user
        private void CheckBalance_Click(object sender, EventArgs e)
        {
            UpdateText(blockchain.GetBalance(publicKey.Text).ToString() + " Assignment Coin");
        }


        /* TRANSACTION MANAGEMENT */
        // Create a new pending transaction and add it to the transaction pool
        private void CreateTransaction_Click(object sender, EventArgs e)
        {
            Transaction transaction = new Transaction(publicKey.Text, reciever.Text, Double.Parse(amount.Text), Double.Parse(fee.Text), privateKey.Text);
            /* TODO: Validate transaction */
            blockchain.transactionPool.Add(transaction);
            UpdateText(transaction.ToString());
        }

        /* BLOCK MANAGEMENT */
        // Conduct Proof-of-work in order to mine transactions from the pool and submit a new block to the Blockchain
        private void NewBlock_Click(object sender, EventArgs e)
        {
            string preference = miningPreferenceComboBox.SelectedItem.ToString();
            string minerAddress = publicKey.Text; // Assuming publicKey.Text contains the miner's public address

            // Retrieve pending transactions to be added to the newly generated Block
            List<Transaction> transactions = blockchain.GetPreferredTransactions(preference, minerAddress);

            // Create and append the new block - requires a reference to the previous block, a set of transactions and the miners public address (For the reward to be issued)
            Block newBlock = new Block(blockchain.GetLastBlock(), transactions, publicKey.Text);
            blockchain.blocks.Add(newBlock);

            UpdateText(blockchain.ToString());

            // Update the UI
            UpdateText("New block mined with " + preference + " preference");


        }


        /* BLOCKCHAIN VALIDATION */
        // Validate the integrity of the state of the Blockchain
        private void Validate_Click(object sender, EventArgs e)
        {
            // CASE: Genesis Block - Check only hash as no transactions are currently present
            if(blockchain.blocks.Count == 1)
            {
                if (!Blockchain.ValidateHash(blockchain.blocks[0])) // Recompute Hash to check validity
                    UpdateText("Blockchain is invalid");
                else
                    UpdateText("Blockchain is valid");
                return;
            }

            for (int i=1; i<blockchain.blocks.Count-1; i++)
            {
                if(
                    blockchain.blocks[i].prevHash != blockchain.blocks[i - 1].hash || // Check hash "chain"
                    !Blockchain.ValidateHash(blockchain.blocks[i]) ||  // Check each blocks hash
                    !Blockchain.ValidateMerkleRoot(blockchain.blocks[i]) // Check transaction integrity using Merkle Root
                )
                {
                    UpdateText("Blockchain is invalid");
                    return;
                }
            }
            UpdateText("Blockchain is valid");
        }

        private void currentWalletLabel_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     

    }
}