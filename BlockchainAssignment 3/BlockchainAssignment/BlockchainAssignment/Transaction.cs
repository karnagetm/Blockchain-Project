using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    class Transaction
    {
        DateTime timestamp;
        public String senderAddress, recipientAddress, hash, signature;
        public Double amount, fee;

        public Transaction(String senderAddress, String recipientAddress, Double amount, Double fee, String privateKey)
        {
            this.timestamp = DateTime.Now;
            this.senderAddress = senderAddress;
            this.recipientAddress = recipientAddress;
            this.amount = amount;
            this.fee = fee;
            this.hash = CreateHash();
            this.signature = Wallet.Wallet.CreateSignature(senderAddress, privateKey, hash);
        }

        public String CreateHash()
        {
            String hash = String.Empty;

            SHA256 hasher = SHA256Managed.Create();
            String input = timestamp.ToString() + senderAddress + recipientAddress + amount.ToString() + fee.ToString();

            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            foreach (byte x in hashByte)
                hash += String.Format("{0:x2}", x);

            return hash;
        }

        public override string ToString()
        {
            return "Timestamp: " + timestamp.ToString() + 
                "\nSender Address: " + senderAddress + 
                "\nRecipient Address: " + recipientAddress + 
                "\nAmount: " + amount.ToString() + " Assignment coin" + 
                "\nFee: " + fee.ToString() + " Assignment Coin" + 
                "\nHash: " + hash +
                "\nSignature: " + signature;
        }
    }
}
