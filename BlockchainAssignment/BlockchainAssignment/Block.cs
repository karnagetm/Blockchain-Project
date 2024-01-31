using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    internal class Block
    {
        public int index;
        DateTime timestamp;
        public String hash;
        String prevHash;

        /*Genisis Block Constructor*/
        public Block()
        {
            this.timestamp = DateTime.Now;
            this.index = 0;
            this.prevHash = String.Empty;
            this.hash = CreateHash();

        }



        public Block(int index, String hash)
        {
            this.timestamp = DateTime.Now;
            this.index = index + 1;
            this.prevHash = hash;
            this.hash = CreateHash();

        }
        public Block(Block lastBlock)
        {
            this.timestamp = DateTime.Now;
            this.index = lastBlock.index + 1;
            this.prevHash = lastBlock.hash;
            this.hash = CreateHash();

        }

        public String CreateHash()
        {
            string hash = String.Empty;

            SHA256 hasher = SHA256Managed.Create();
            String input = index.ToString() + timestamp.ToString() + prevHash;

            byte[] hashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            //Cconvert Hash from bye array to string
            foreach (byte b in hashByte)
            {
                hash += String.Format("{0:x2}", b);// change to b instead of x if it is not fixed 


            }
            
            
            return hash;

        }

        public override string ToString()
        {
            return "Index:" + index.ToString() 
                + "\nTimestamp: " + timestamp.ToString() 
                + "\nPrevious Hash: " + prevHash 
                + "\nHash:" + hash;

        }


    }

}
