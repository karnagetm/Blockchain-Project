using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

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

        public Block(Block previousBlock, List<Transaction> transactions, String minerAddress)
        {
            this.index = previousBlock.index + 1;
            this.timestamp = DateTime.Now;
            this.prevHash = previousBlock.hash;
            this.minerAddress = minerAddress;
            transactions.Add(CreateRewardTransaction(transactions));
            this.transactionList = transactions;
            this.merkleRoot = MerkleRoot(transactions);
            this.hash = Mine();
        }

        // Part of the Block class...

        private string CalculateHash(long nonce, SHA256 hasher)
        {
            string input = $"{index}{timestamp}{prevHash}{nonce}";
            byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashByte).Replace("-", "").ToLower();
        }

        private bool IsValidHash(string hash)
        {
            return hash.StartsWith(new string('0', difficulty));
        }

        public long MiningDurationMs { get; private set; }

        public String Mine()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            nonce = 0;
            int maxNonce = int.MaxValue;
            int numberOfThreads = Environment.ProcessorCount;
            var cts = new CancellationTokenSource();
            Task<string>[] miningTasks = new Task<string>[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                int threadIndex = i;
                miningTasks[threadIndex] = Task.Run(() =>
                {
                    SHA256 hasher = SHA256Managed.Create();
                    long localNonce = Interlocked.Increment(ref nonce) - 1;
                    string localHash = String.Empty;
                    while (!cts.Token.IsCancellationRequested && localNonce < maxNonce)
                    {
                        localHash = CalculateHash(localNonce, hasher);
                        if (IsValidHash(localHash))
                        {
                            cts.Cancel();
                            return localHash;
                        }
                        localNonce = Interlocked.Increment(ref nonce) - 1;
                    }
                    return null;
                }, cts.Token);
            }

            string validHash = null;
            try
            {
                var completedTask = Task.WhenAny(miningTasks).Result;
                validHash = completedTask.Result;
            }
            catch (AggregateException)
            {
                // Handle exceptions
            }
            finally
            {
                stopwatch.Stop();
                MiningDurationMs = stopwatch.ElapsedMilliseconds;
                // Note: Update this line to match how your application updates the UI.
                // UpdateUIText($"Mining time: {stopwatch.ElapsedMilliseconds}ms");
            }

            if (validHash != null)
            {
                nonce = Interlocked.Read(ref nonce) - 1;
                hash = validHash;
            }

            return validHash;
        }



        public Transaction CreateRewardTransaction(List<Transaction> transactions)
        {
            double fees = transactions.Aggregate(0.0, (acc, t) => acc + t.fee);
            // The "Mine Rewards" address is a placeholder. Replace with actual miner address.
            return new Transaction("Mine Rewards", minerAddress, reward + fees, 0, "");
        }

        public static String MerkleRoot(List<Transaction> transactionList)
        {
            // Assuming HashCode.HashTools.CombineHash is a method you've defined elsewhere to combine two hashes
            List<String> hashes = transactionList.Select(t => t.hash).ToList();
            while (hashes.Count > 1)
            {
                List<String> newHashes = new List<String>();
                for (int i = 0; i < hashes.Count; i += 2)
                {
                    if (i == hashes.Count - 1)
                        newHashes.Add(HashCode.HashTools.CombineHash(hashes[i], hashes[i]));
                    else
                        newHashes.Add(HashCode.HashTools.CombineHash(hashes[i], hashes[i + 1]));
                }
                hashes = newHashes;
            }
            return hashes.FirstOrDefault() ?? String.Empty;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Index: {index}");
            sb.AppendLine($"Timestamp: {timestamp}");
            sb.AppendLine($"Hash: {hash}");
            sb.AppendLine($"Previous Hash: {prevHash}");
            sb.AppendLine($"Transactions: {String.Join("\n", transactionList.Select(t => t.ToString()))}");
            sb.AppendLine($"Nonce: {nonce}");
            sb.AppendLine($"Merkle Root: {merkleRoot}");
            sb.AppendLine($"Mining Duration: {MiningDurationMs} ms");
            return sb.ToString();
        }
        
    }
    
}

