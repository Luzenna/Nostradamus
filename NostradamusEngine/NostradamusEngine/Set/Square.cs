﻿using System;
using NostradamusEngine.Pieces;

namespace NostradamusEngine.Set
{
    public class Square
    {
        private Boolean isWhite;
        private Int32 file, rank;
        private Piece piece;

        public Square(Boolean isWhite, Int32 file, Int32 row)
        {
            this.isWhite = isWhite;
            this.rank = row;
            this.file = file;
        }

        public Int32 Rank
        {
            get
            {
                return rank;
            }
        }

        public Int32 File
        {
            get
            {
                return file;
            }
        }

        public Boolean IsWhite
        {
            get
            {
                return isWhite;
            }
        }

        public Piece Piece
        {
            get
            {
                return piece;
            }
            set
            {
                piece = value;
            }
        }

        public string Name
        {
            get
            {
                var fileName = (char)(file + 97);
                return $"{fileName}{rank+1}";
            }
        }

        public override string ToString()
        {
            return $"Square.{Name}";
        }
    }
}
