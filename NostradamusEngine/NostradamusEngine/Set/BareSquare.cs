using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Set
{
    public class BareSquare : ISquare
    {
        public BareSquare(int file, int rank)
        {
            Rank = rank;
            File = file;
        }
        public int Rank { get; private set; }
        public int File { get; private set; }
    }
}
