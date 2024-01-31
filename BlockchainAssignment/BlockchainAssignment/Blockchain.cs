using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainAssignment
{
    internal class Blockchain
    {
        List<Block> Blocks = new List<Block>();

        public Blockchain()
        {
            Blocks.Add(new Block());
        }

            
       public String GetBlockAsString(int index)
        {
            return Blocks[index].ToString();
        }
       



    }
}
