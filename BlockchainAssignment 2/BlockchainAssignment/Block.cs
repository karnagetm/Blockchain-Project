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
        String hash, prevHash;
        DateTime timestamp;

        /* Genesis Block Constructor */
        public Block()
        {
            this.index = 0;
            this.timestamp = DateTime.Now;
            this.prevHash = String.Empty;
            this.hash = CreateHash();
        }

        public Block(int index, String hash)
        {
            this.index = index + 1;
            this.timestamp = DateTime.Now;
            this.prevHash = hash;
            this.hash = CreateHash();
        }

        public String CreateHash()
        {
            String hash = String.Empty;

            SHA256 hasher = SHA256Managed.Create();
            String input = index.ToString() + hash + prevHash + timestamp.ToString();

            Byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            foreach (byte x in hashByte)
                hash += String.Format("{0:x2}", x);

            return hash;
        }

        public override string ToString()
        {
            return "Index: " + index.ToString() + 
                "\nTimestamp: " + timestamp.ToString() +
                "\nHash: " + hash +
                "\nPrevious Hash: " + prevHash;
        }
    }
}
