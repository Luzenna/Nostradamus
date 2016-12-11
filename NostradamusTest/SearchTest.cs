using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NostradamusEngine;
using NostradamusEngine.Evaluators;
using NostradamusEngine.Pieces;
using NostradamusEngine.Set;
using NostradamusEngine.Set.SimpleBoard;

namespace NostradamusTest
{
    [TestClass]
    public class SearchTest
    {
        [AssemblyInitialize]
        public static void Configure(TestContext tc)
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private ChessEngine game;
        [TestInitialize]
        public void Init()
        {
            var board = new Board();
            game = new ChessEngine(new SimpleEvaluator(), board);
        }

    [TestMethod]
        public void NumberOfOpeningsMovesShouldBe20()
        {
            game.LoadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            var numberOfMoves = game.Perft(1,Color.White);
            Assert.AreEqual(20,numberOfMoves);
        }

        [TestMethod]
        // 400 second moves + 20 first moves
        public void NumberOfMovesAfter2PlyShouldBe420()
        {
            game.LoadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            var numberOfMoves = game.Perft(2, Color.White);
            Assert.AreEqual(420, numberOfMoves);
        }

        [TestMethod]
        // 400 second moves + 20 first moves
        public void NumberOfMovesAfter3PlyShouldBe9322()
        {
            game.LoadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            var numberOfMoves = game.Perft(3, Color.White);
            Assert.AreEqual(9322, numberOfMoves);
        }

        [TestMethod]
        // 400 second moves + 20 first moves
        public void NumberOfMovesAfter4PlyShouldBe206603()
        {
            game.LoadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            var numberOfMoves = game.Perft(4, Color.White);
            Assert.AreEqual(206603, numberOfMoves);
        }


        [TestMethod]
        public void ThisShouldBeStaleMate1()
        {
            game.LoadFEN("k7/6b1/8/8/8/n7/PP6/K7 w - -");
            var numberOfMoves = game.Perft(1, Color.White);
            Assert.AreEqual(0,numberOfMoves);
        }

        [TestMethod]
        public void ComplexPositionWith102Moves()
        {
            game.LoadFEN("4R3/2Q5/6R1/1K1B1N2/3B4/3N4/P6P/1k6 w - - 0 1");
            var numberOfMoves = game.Perft(1, Color.White);
            Assert.AreEqual(102,numberOfMoves);
        }

        [TestMethod]
        public void ComplexPositionWith218Moves()
        {
            game.LoadFEN("R6R/3Q4/1Q4Q1/4Q3/2Q4Q/Q4Q2/pp1Q4/kBNN1KB1 w - - 0 1");
            var numberOfMoves = game.Perft(1, Color.White);
            Assert.AreEqual(218,numberOfMoves);
        }

        [TestMethod]
        public void Only1LegalMoveButItGivesMate()
        {
            game.LoadFEN("7k/3n1KRP/6P1/8/8/8/8/4r3 w - -");
            var numberOfMoves = game.Perft(1, Color.White);
            Assert.AreEqual(1,numberOfMoves);
        }

    }
}
