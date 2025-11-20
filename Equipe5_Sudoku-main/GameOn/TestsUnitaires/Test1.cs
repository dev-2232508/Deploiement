using GameOn.Data.Context;
using GameOn.Models;
using GameOn.ViewModels;
using GameOn.Views.Sudoku;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace TestsUnitaires
{
    [TestClass]
    public sealed class ManagerTests
    {
        private Manager _manager;
        private string _password;
        private string _email;
        private string _hash;

        [TestInitialize]
        public void TestSetup()
        {
            _manager = new Manager();
            _password = "password";
            _email = "user@gmail.com";
            _hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(_password))).ToLower();
        }

        //Test AddUser quand les données sont corrects
        [TestMethod]
        public void AddUser_Should_Return_True()
        {
            User user = new User(1, "Jean", "Doe", _email, _hash, false, "+12445");
            bool result = _manager.AddUser(user);

            Assert.IsTrue(result);
        }

        //Test AddUser quand les données sont incorrectes
        [TestMethod]
        public void AddUser_Should_Return_False()
        {
            User user = new User(1, "Jean", "Doe", _email, _hash, false, "+12445");
            _manager.AddUser(user);
            bool result = _manager.AddUser(user);

            Assert.IsFalse(result);
        }

        //Test Login quand les données sont corrects
        [TestMethod]
        public void Login_Should_Return_True()
        {
            User user = new User(1, "Jean", "Doe", _email, _hash, false, "+12445");
            _manager.AddUser(user);

            bool result = _manager.Login(_password, _email);
            Console.WriteLine(result);
            Assert.IsTrue(result);
        }

        //Test Login quand le motdepasse est incorrect 

        [TestMethod]
        public void Login_Should_Return_False()
        {
            User user = new User(1, "Jean", "Doe", _email, _hash, false, "+12445");
            _manager.AddUser(user);

            bool result = _manager.Login("wrongpass", _email);
            Console.WriteLine(result);
            Assert.IsFalse(result);
        }
    }


    [TestClass]
    public sealed class LoginVMTests
    {
        //Test Login quand les données sont corrects
        [TestMethod]
        public void Login_Should_Call_Manager()
        {
            Mock<Manager> mockManager = new Mock<Manager>();
            var vm = new LoginVM(mockManager.Object, null);

            vm.Pwd = "password";
            vm.Email = "user@gmail.com";

            vm.LoginCommand.Execute(null);

            mockManager.Verify(m => m.Login("password", "user@gmail.com"), Times.Once());

        }

        //Test Login quand le motdepasse est incorrect 
        [TestMethod]
        public void Login_Should_Not_Call_Manager()
        {
            Mock<Manager> mockManager = new Mock<Manager>();
            var vm = new LoginVM(mockManager.Object, null);

            vm.Pwd = null;
            vm.Email = "user@gmail.com";

            vm.LoginCommand.Execute(null);

            mockManager.Verify(m => m.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

        }
    }

    [TestClass]
    public sealed class SudokuVMTests
    {
        private Manager _manager;
        [TestInitialize]
        public void TestSetup()
        {
            _manager = new Manager();
        }

        // Teste que ConvertirEnGrille lève une exception si la chaîne est invalide.
        [DataTestMethod]
        [DataRow("123")]
        [DataRow("")]
        [DataRow(null)]
        public void ConvertirEnGrille_Should_Throw_Exception(string input)
        {
            var vm = new SudokuVM(_manager);

            var method = typeof(SudokuVM).GetMethod("ConvertirEnGrille", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method, "ConvertirEnGrille method not found.");

            var ex = Assert.ThrowsException<TargetInvocationException>(() => method.Invoke(vm, new object[] { input }));
            Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentOutOfRangeException));

        }

        //Teste que ConvertirEnGrille crée une matrice 9x9 correcte.
        [TestMethod]
        public void ConvertirEnGrille_Should_Return_Valid_Grid()
        {
            var vm = new SudokuVM(_manager);
            string chaine = new string('1', 81);
            var method = typeof(SudokuVM).GetMethod("ConvertirEnGrille", BindingFlags.NonPublic | BindingFlags.Instance);

            int[,] resultat = (int[,])method.Invoke(vm, new object[] { chaine });

            Assert.AreEqual(9, resultat.GetLength(0));
            Assert.AreEqual(9, resultat.GetLength(1));
            Assert.AreEqual(1, resultat[0, 0]);
            Assert.AreEqual(1, resultat[8, 8]);
        }

        //Teste que CheckGrid renvoie "Vous avez gagné" quand la solution est correcte.
        [TestMethod]
        public void CheckGrid_Should_Return_Win_When_Solution_Is_Correct()
        {
            string solution = new string('1', 81);

            Sudoku puzzle = new Sudoku
            {
                Id = 21,
                GrilleInitiale = solution,
                GrilleSolution = solution
            };

            var vm = new SudokuVM(_manager);

            var Cellules = new System.Collections.ObjectModel.ObservableCollection<SudokuCell>();
            for (int i = 0; i < 81; i++)
            {
                Cellules.Add(new SudokuCell(1, true, $"Cell_{i / 9}_{i % 9}"));
            }

            vm.Cellules = Cellules;
            vm.PuzzleActuel = puzzle;

            vm.CheckGrid();
            Assert.AreEqual("Vous avez gagné", vm.MessageFinDePartie);
        }

        // Teste que CheckGrid renvoie "Vous avez perdu" quand la solution est incorrecte.
        [TestMethod]
        public void CheckGrid_Should_Return_Lose_When_Solution_Is_Correct()
        {
            string GrilleInitiale = new string('1', 81);
            string solution = new string('2', 81);

            Sudoku puzzle = new Sudoku
            {
                Id = 21,
                GrilleInitiale = GrilleInitiale,
                GrilleSolution = solution
            };

            var vm = new SudokuVM(_manager);

            var Cellules = new System.Collections.ObjectModel.ObservableCollection<SudokuCell>();
            for (int i = 0; i < 81; i++)
            {
                Cellules.Add(new SudokuCell(1, true, $"Cell_{i}"));
            }

            vm.Cellules = Cellules;
            vm.PuzzleActuel = puzzle;

            vm.CheckGrid();
            Assert.AreEqual("Vous avez perdu", vm.MessageFinDePartie);
        }

        // Vérifie que la VM crée bien 81 cellules.
        [TestMethod]
        public void SudokuVM_Should_Create_81_Cellules()
        {
            string grille = new string('1', 81);
            var sudoku = new Sudoku
            {
                Id = 999,
                GrilleInitiale = grille,
                GrilleSolution = grille
            };

            var mockContext = new Mock<SudokuContext>();
            mockContext.Setup(c => c.Puzzles).Returns(MockDbSet(new List<Sudoku> { sudoku }));

            var mockManager = new Mock<Manager>();
            mockManager.Setup(m => m.ChargerDernierSudoku()).Returns(sudoku.Id);

            var vm = new SudokuVM_Testable(mockManager.Object, mockContext.Object);

            Assert.AreEqual(81, vm.Cellules.Count);
        }

        private static DbSet<T> MockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            return mockDbSet.Object;
        }

    }
    internal class SudokuVM_Testable : SudokuVM
    {
        public SudokuVM_Testable(Manager manager, SudokuContext context): base(manager)
        {
            var field = typeof(SudokuVM).GetField("_sudokuContext", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(this, context);

        }
    }
    
}
