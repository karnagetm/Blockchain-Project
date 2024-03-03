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
            if (index >= 0 && index < blocks.Count)
                return blocks[index].ToString();
            else
                return "No such block exists";
        }

        // Returns the last block added to the chain
        public Block GetLastBlock()
        {
            return blocks[blocks.Count - 1];
        }

        // Processes and clears pending transactions for block inclusion
        public List<Transaction> GetPendingTransactions()
        {
            int n = Math.Min(transactionsPerBlock, transactionPool.Count);
            List<Transaction> transactions = transactionPool.GetRange(0, n);
            transactionPool.RemoveRange(0, n);
            return transactions;
        }

        // Validates a block's hash to ensure integrity
        public static bool ValidateHash(Block b)
        {
            String rehash = b.CreateHash();
            return rehash.Equals(b.hash);
        }

        // Ensures the Merkle root's accuracy within a block
        public static bool ValidateMerkleRoot(Block b)
        {
            String reMerkle = Block.MerkleRoot(b.transactionList);
            return reMerkle.Equals(b.merkleRoot);
        }

        // Calculates the balance of a given wallet address
        public double GetBalance(String address)
        {
            double balance = 0;
            foreach (Block b in blocks)
            {
                foreach (Transaction t in b.transactionList)
                {
                    if (t.recipientAddress.Equals(address))
                    {
                        balance += t.amount;
                    }
                    if (t.senderAddress.Equals(address))
                    {
                        balance -= (t.amount + t.fee);
                    }
                }
            }
            return balance;
        }

        public double GetBalanceForAddress(string address)
        {
            return GetBalance(address);
        }

        public bool AddTransactionToPool(Transaction transaction)
        {
            // Check the sender's balance
            double senderBalance = GetBalance(transaction.senderAddress);
            if (senderBalance < transaction.amount + transaction.fee)
            {
                return false; // Insufficient balance
            }

            // Check for duplicate transactions in the transaction pool
            if (transactionPool.Any(t => t.hash == transaction.hash))
            {
                return false; // Duplicate transaction
            }

            // Check for duplicate transactions in the blockchain
            foreach (Block block in blocks)
            {
                if (block.transactionList.Any(t => t.hash == transaction.hash))
                {
                    return false; // Transaction is already in the blockchain
                }
            }

            // If all checks pass, add the transaction to the pool
            transactionPool.Add(transaction);
            return true;
        }

        // Compiles all blocks' information into a single string
        public override string ToString()
        {
            return String.Join("\n", blocks);
        }

        // New method to get pending transactions as a string
        public string GetPendingTransactionsAsString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Transaction t in transactionPool)
            {
                sb.AppendLine(t.ToString());
            }
            return sb.ToString();
        }
    }

    
}
