﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    class Transaction
    {
        /* Transaction Variables */
        DateTime timestamp; // Time of creation
        public String senderAddress, recipientAddress; // Participants public key addresses
        public double amount, fee; // Quantities transferred
        public String hash, signature; // Attributes for verification of validity

        /* Transaction Constructor */
        public Transaction(String from, String to, double amount, double fee, String privateKey)
        {
            timestamp = DateTime.Now;
            senderAddress = from;
            recipientAddress = to;
            this.amount = amount;
            this.fee = fee;
            hash = CreateHash(); // Hash the transaction attributes
            // Assuming Wallet.Wallet.CreateSignature is a valid external method to create the signature
            signature = Wallet.Wallet.CreateSignature(from, privateKey, hash); // Sign the hash with the sender's private key ensuring validity
        }

        /* Hash the transaction attributes using SHA256 */
        public String CreateHash()
        {
            String hash = String.Empty;
            SHA256 hasher = SHA256Managed.Create();

            /* Concatenate all transaction properties */
            String input = timestamp + senderAddress + recipientAddress + amount + fee;

            /* Apply the hash function to the "input" string */
            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            /* Reformat to a string */
            foreach (byte x in hashByte)
                hash += String.Format("{0:x2}", x);

            return hash;
        }

        // Represent a transaction as a string for output to UI
        public override string ToString()
        {
            return "  [TRANSACTION START]"
                + "\n  Timestamp: " + timestamp
                + "\n  -- Verification --"
                + "\n  Hash: " + hash
                + "\n  Signature: " + signature
                + "\n  -- Quantities --"
                + "\n  Transferred: " + amount + " Assignment Coin"
                + "\t  Fee: " + fee
                + "\n  -- Participants --"
                + "\n  Sender: " + senderAddress
                + "\n  Recipient Address: " + recipientAddress 
                + "\n  [TRANSACTION END]";
        }

        // This should be in the same namespace as the Transaction class

        class TransactionPool
        {
            private List<Transaction> pool;

            public TransactionPool()
            {
                pool = new List<Transaction>();
            }

            // Add a new transaction to the pool
            public void AddTransaction(Transaction transaction)
            {
                // TODO: Add validation to ensure transaction is not already in the pool
                // and meets other criteria as necessary (e.g., valid signature)
                pool.Add(transaction);
            }

            // Remove a transaction from the pool by its hash
            public bool RemoveTransaction(string transactionHash)
            {
                var transaction = pool.FirstOrDefault(t => t.hash == transactionHash);
                if (transaction != null)
                {
                    pool.Remove(transaction);
                    return true;
                }
                return false;
            }

            // Get all transactions currently in the pool
            public List<Transaction> GetAllTransactions()
            {
                return pool;
            }

            // Select transactions for a new block
            // This example prioritizes transactions by fee, selecting up to a specified limit
            public List<Transaction> GetTransactionsForBlock(int limit)
            {
                return pool.OrderByDescending(t => t.fee).Take(limit).ToList();
            }

            // Clear the pool, for example, after a block containing these transactions is mined
            public void ClearPool()
            {
                pool.Clear();
            }
        }

        // Example usage, assuming you have access to a TransactionPool instance somewhere in your code:
        public class BlockchainApp
        {
            private TransactionPool transactionPool;

            public BlockchainApp()
            {
                transactionPool = new TransactionPool();
            }

            public void CreateNewTransaction(string fromAddress, string toAddress, double amount, double fee, string senderPrivateKey)
            {
                Transaction newTransaction = new Transaction(fromAddress, toAddress, amount, fee, senderPrivateKey);
                transactionPool.AddTransaction(newTransaction);
            }
        }


    }
}

