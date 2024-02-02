using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    class Block
    {
        int index;
        String hash, prevHash, merkleRoot;
        DateTime timestamp;

        List<Transaction> transactionList;

        public long nonce = 0;
        public int difficulty = 4;

        public double reward = 1.0;
        public String minerAddress = "";

        /* Genesis Block Constructor */
        public Block()
        {
            this.index = 0;
            this.timestamp = DateTime.Now;
            this.prevHash = String.Empty;
            this.transactionList = new List<Transaction>();
            this.hash = Mine();
        }

        public Block(int index, String hash)
        {
            this.index = index + 1;
            this.timestamp = DateTime.Now;
            this.prevHash = hash;
            this.hash = Mine();
        }

        public Block(Block block, List<Transaction> transactions, String minerAddress)
        {
            this.index = block.index + 1;
            this.timestamp = DateTime.Now;
            this.prevHash = block.hash;

            this.minerAddress = minerAddress;
            transactions.Add(CreateRewardTransaction(transactions));
            
            this.transactionList = transactions;
            this.merkleRoot = MerkleRoot(transactions);

            this.hash = Mine();
        }

        public String CreateHash()
        {
            String hash = String.Empty;

            SHA256 hasher = SHA256Managed.Create();
            String input = index.ToString() + hash + prevHash + timestamp.ToString() + nonce;

            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            foreach (byte x in hashByte)
                hash += String.Format("{0:x2}", x);

            return hash;
        }

        public String Mine()
        {
            nonce = 0;
            String hash = CreateHash();

            String re = new string('0', difficulty);

            while(!hash.StartsWith(re))
            {
                nonce++;
                hash = CreateHash();
            }
            return hash;
        }

        public static String MerkleRoot(List<Transaction> transactionList)
        {
            List<String> hashes = transactionList.Select(t => t.hash).ToList();
            if(hashes.Count == 0)
            {
                return String.Empty;
            }
            if(hashes.Count == 1)
            {
                return hashes[0];
            }
            while (hashes.Count != 1)
            {
                List<String> merkleLeaves = new List<String>();
                for (int i=0; i<hashes.Count; i+=2)
                {
                    if (i == hashes.Count - 1)
                    {
                        merkleLeaves.Add(HashCode.HashTools.CombineHash(hashes[i], hashes[i]));
                    }
                    else
                    {
                        merkleLeaves.Add(HashCode.HashTools.CombineHash(hashes[i], hashes[i+1]));
                    }
                }
                hashes = merkleLeaves;
            }

            return hashes[0];
        }

        public Transaction CreateRewardTransaction(List<Transaction> transactions)
        {
            double fees = transactions.Aggregate(0.0, (acc, t) => acc + t.fee);
            return new Transaction("Mine Rewards", minerAddress, (reward + fees), 0, "");
        }

        public override string ToString()
        {
            return "Index: " + index.ToString() + 
                "\nTimestamp: " + timestamp.ToString() +
                "\nHash: " + hash +
                "\nPrevious Hash: " + prevHash +
                "\nTransactions: " + String.Join("\n", transactionList) +
                "\nNonce: " + nonce.ToString() + 
                "\nMerkle Root: " + merkleRoot;
        }
    }
}
