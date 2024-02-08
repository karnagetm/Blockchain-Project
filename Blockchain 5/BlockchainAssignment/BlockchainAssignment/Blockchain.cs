using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    class Blockchain
    {
        // Holds the chain of blocks making up the blockchain
        public List<Block> blocks;

        // Limit on transactions within a single block
        private int transactionsPerBlock = 5;

        // Queue for transactions awaiting inclusion in a block
        public List<Transaction> transactionPool = new List<Transaction>();

        // Initializes blockchain with a genesis block
        public Blockchain()
        {
            blocks = new List<Block>()
            {
                new Block() // Instantiates the initial block of the chain
            };
        }

        // Fetches and returns a block's data as a string by index
        public String GetBlockAsString(int index)
        {
            // Validates the existence of the block at the given index
            if (index >= 0 && index < blocks.Count)
                return blocks[index].ToString(); // Formats the block's data as a string
            else
                return "No such block exists"; // Error message for invalid index
        }

        // Returns the last block added to the chain
        public Block GetLastBlock()
        {
            return blocks[blocks.Count - 1]; // Accesses the most recent block
        }

        // Processes and clears pending transactions for block inclusion
        public List<Transaction> GetPendingTransactions()
        {
            // Determines the batch size of transactions to process
            int n = Math.Min(transactionsPerBlock, transactionPool.Count);

            // Extracts the set of transactions to be included in the next block
            List<Transaction> transactions = transactionPool.GetRange(0, n);
            transactionPool.RemoveRange(0, n); // Clears these transactions from the pool

            // Returns the selected transactions for block inclusion
            return transactions;
        }

        // Validates a block's hash to ensure integrity
        public static bool ValidateHash(Block b)
        {
            String rehash = b.CreateHash(); // Recomputes the block's hash
            return rehash.Equals(b.hash); // Compares with the stored hash value
        }

        // Ensures the Merkle root's accuracy within a block
        public static bool ValidateMerkleRoot(Block b)
        {
            String reMerkle = Block.MerkleRoot(b.transactionList); // Recalculates the Merkle root
            return reMerkle.Equals(b.merkleRoot); // Verifies against the stored Merkle root
        }

        // Calculates the balance of a given wallet address
        public double GetBalance(String address)
        {
            double balance = 0; // Starting balance

            // Iterates over each block and transaction to update balance
            foreach (Block b in blocks)
            {
                foreach (Transaction t in b.transactionList)
                {
                    if (t.recipientAddress.Equals(address))
                    {
                        balance += t.amount; // Adds received funds to balance
                    }
                    if (t.senderAddress.Equals(address))
                    {
                        balance -= (t.amount + t.fee); // Subtracts sent funds and fees from balance
                    }
                }
            }
            return balance; // Final calculated balance
        }

        // Compiles all blocks' information into a single string
        public override string ToString()
        {
            return String.Join("\n", blocks); // Concatenates block information with newline separators
        }
    }
}
