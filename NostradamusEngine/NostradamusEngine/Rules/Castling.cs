using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Rules
{
    public class Castling
    {
        private Boolean kingside, queenside;
        public Castling()
        {
            kingside = true;
            queenside = true;
        }

        public Boolean Kingside
        {
            get
            {
                return kingside;
            }
            set
            {
                kingside = value;
            }
        }

        public Boolean Queenside
        {
            get
            {
                return queenside;
            }
            set
            {
                queenside = value;
            }
        }


    }
}
