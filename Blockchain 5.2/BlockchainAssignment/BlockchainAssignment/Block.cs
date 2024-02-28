using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    class Block
    {
        // Variables associated with the block
        private DateTime timestamp; // Time when the block was created
        private int index; // Block's position in the blockchain
        private int difficulty = 4; // Number of leading zeros required in the block's hash

        // References to block data
        public string prevHash; // Hash of the previous block in the chain
        public string hash; // Unique identifier for the current block
        public string merkleRoot; // Hash of all the transactions within the block
        public string minerAddress; // Identifier for the entity that mined the block

        // List of all transactions included in the block
        public List<Transaction> transactionList;

        // Variable for Proof-of-Work process
        public long nonce; // Arbitrary number that can only be used once in mining

        // Record of the time taken to mine the block
        public long MiningDurationMs { get; private set; }

        // Variable for block reward
        public double reward; // Incentive given for successfully mining the block

        // Constructor for the genesis block
        public Block()
        {
            timestamp = DateTime.Now;
            index = 0;
            transactionList = new List<Transaction>();
            hash = Mine();
        }

        // Constructor for a new block in the chain
        public Block(Block lastBlock, List<Transaction> transactions, string minerAddress)
        {
            timestamp = DateTime.Now;
            index = lastBlock.index + 1;
            prevHash = lastBlock.hash;
            this.minerAddress = minerAddress; // Recipient of the mining reward
            reward = 1.0; // Set a predetermined reward amount
            transactions.Add(createRewardTransaction(transactions)); // Add the reward to the list of transactions
            transactionList = new List<Transaction>(transactions);
            merkleRoot = MerkleRoot(transactionList); // Determine the merkle root from the transactions
            hash = Mine(); // Perform the mining process to find a suitable hash
        }

        // Generates the block's hash
        public string CreateHash()
        {
            string hash = string.Empty;
            using (SHA256 hasher = SHA256Managed.Create())
            {
                // Combine block data into a single string for hashing
                string input = timestamp.ToString() + index + prevHash + nonce + merkleRoot;
                // Compute the hash value for the combined string
                byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                // Convert the hash bytes to a hexadecimal string
                hash = string.Join("", hashByte.Select(x => x.ToString("x2")));
            }
            return hash;
        }

        // Mining process to find a hash that meets the difficulty criteria
        public string Mine()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            nonce = 0;
            string hash;
            string requiredStart = new string('0', difficulty);

            do
            {
                nonce++;
                hash = CreateHash();
            } while (!hash.StartsWith(requiredStart));

            stopwatch.Stop();
            MiningDurationMs = stopwatch.ElapsedMilliseconds;
            return hash;
        }

        // Builds the merkle root from the list of transactions
        public static string MerkleRoot(List<Transaction> transactionList)
        {
            List<string> hashes = transactionList.Select(t => t.hash).ToList();

            while (hashes.Count > 1)
            {
                List<string> merkleLeaves = new List<string>();
                for (int i = 0; i < hashes.Count; i += 2)
                {
                    string leaf = i == hashes.Count - 1 ?
                        HashCode.HashTools.combineHash(hashes[i], hashes[i]) :
                        HashCode.HashTools.combineHash(hashes[i], hashes[i + 1]);
                    merkleLeaves.Add(leaf);
                }
                hashes = merkleLeaves;
            }
            return hashes.FirstOrDefault() ?? string.Empty;
        }

        // Generates a transaction to reward the miner
        public Transaction createRewardTransaction(List<Transaction> transactions)
        {
            double fees = transactions.Sum(t => t.fee);
            return new Transaction("Mine Rewards", minerAddress, reward + fees, 0, "");
        }

        // Converts the block's information into a string format
        public override string ToString()
        {
            // Build the complete string with all the block information
            string blockInfo = "[BLOCK START]"
                + "\nIndex: " + index
                + "\tTimestamp: " + timestamp
                + "\nPrevious Hash: " + prevHash
                + "\n-- PoW --"
                + "\nDifficulty Level: " + difficulty
                + "\nNonce: " + nonce
                + "\nHash: " + hash
                + "\n-- Rewards --"
                + "\nReward: " + reward
                + "\nMiners Address: " + minerAddress
                + "\n-- " + transactionList.Count + " Transactions --"
                + "\nMerkle Root: " + merkleRoot
                + "\n" + String.Join("\n", transactionList)
                + "\nMining Duration: " + MiningDurationMs + " ms"
                + "\n[BLOCK END]";

            return blockInfo; // Return the complete block information
        }




    }
}

