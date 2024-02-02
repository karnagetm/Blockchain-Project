using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    class Blockchain
    {
        public List<Block> Blocks;

        public int transactionsPerBlock = 5;
        public List<Transaction> transactionPool;

        public Blockchain()
        {
            Blocks = new List<Block>() {
                new Block()
            };
            transactionPool = new List<Transaction>();
        }

        public String getBlock(int index)
        {
            if (index >= 0 && index < Blocks.Count)
                return Blocks[index].ToString();
            return "Block does not Exist";
        }

        public Block getLastBlock()
        {
            return Blocks[Blocks.Count - 1];
        }

        public List<Transaction> GetPendingTransactions()
        {
            int n = Math.Min(transactionsPerBlock, transactionPool.Count);
            List<Transaction> transactions = transactionPool.GetRange(0, n);
            transactionPool.RemoveRange(0, n);
            return transactions;
        }

        public override string ToString()
        {
            return String.Join("\n", Blocks);
        }
    }
}
