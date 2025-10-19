using StOopLab;

namespace OopLab5.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestInitialize]
        public void Setup()
        {
            // Reset global state before each test
            Program.database.Clear();
            WaterVendingMachine.Count = 0;
        }

        [TestMethod]
        public void CreateItemManually_ValidInput_AddsToDatabase()
        {
            // Arrange
            var address = "1 floor";
            var name = "Tom";
            var phone = "123456";
            var comp = "Aqua";
            var capacity = 1000;
            var input = new StringReader($"{address}\n{name}\n{phone}\n{comp}\n{capacity}\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.CreateItemManually();

            // Assert
            Assert.AreEqual(1, Program.database.Count);
            var machine = Program.database[0];
            Assert.AreEqual(address, machine.Address);
            Assert.AreEqual(name, machine.OperatorName);
            Assert.AreEqual(phone, machine.Phone);
            Assert.AreEqual(comp, machine.CompanyName);
            Assert.AreEqual(capacity, machine.WaterCapacityLiters);
            Assert.AreEqual(1, WaterVendingMachine.Count);
        }

        [TestMethod]
        public void CreateItemManually_InvalidAddress_RetriesUntilValid()
        {
            // Arrange
            var address = "1 floor";
            var input = new StringReader("\n" + // Empty address
                                         "ab\n" + // Too short
                                         $"{address}\n" + // Valid address
                                         "Tom\n123456\nAqua\n1000\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.CreateItemManually();

            // Assert
            Assert.AreEqual(1, Program.database.Count);
            var machine = Program.database[0];
            Assert.AreEqual(address, machine.Address);
            StringAssert.Contains(output.ToString(), "Value is empty");
            StringAssert.Contains(output.ToString(), "Value is out of range");
        }

        [TestMethod]
        public void CreateItemManually_InvalidCapacity_RetriesUntilValid()
        {
            // Arrange
            var input = new StringReader("1 floor\nTom\n123456\nAqua\n" +
                                        "abc\n" + // Invalid number
                                        "400\n" + // Out of range
                                        "1000\n"); // Valid capacity
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.CreateItemManually();

            // Assert
            Assert.AreEqual(1, Program.database.Count);
            var machine = Program.database[0];
            Assert.AreEqual(1000, machine.WaterCapacityLiters);
            StringAssert.Contains(output.ToString(), "Invalid format");
            StringAssert.Contains(output.ToString(), "Value is out of range");
        }

        [TestMethod]
        public void CreateItemManually_Name_RetriesUntilValid()
        {
            // Arrange
            var input = new StringReader("1 floor" +
                                        "\nxx" + // Out of range
                                        "\nTom" + // Valid name
                                        "\n123456\nAqua\n1000\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.CreateItemManually();

            // Assert
            Assert.AreEqual(1, Program.database.Count);
            StringAssert.Contains(output.ToString(), "Operator Name value is out of range");
        }

        [TestMethod]
        public void Delete_NonEmptyDatabase_ValidIndex_RemovesItem()
        {
            // Arrange
            Program.database.Add(new WaterVendingMachine { Address = "1 floor", OperatorName = "Tom", Phone = "123456", CompanyName = "Aqua", WaterCapacityLiters = 1000 });
            Program.database.Add(new WaterVendingMachine { Address = "7 floor", OperatorName = "Joe", Phone = "654321", CompanyName = "Water", WaterCapacityLiters = 1500 });
            WaterVendingMachine.Count = 2;
            var input = new StringReader("1\n"); // Delete index 1
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.Delete();

            // Assert
            Assert.AreEqual(1, Program.database.Count);
            Assert.AreEqual("1 floor", Program.database[0].Address);
            Assert.AreEqual(1, WaterVendingMachine.Count);
            var outputString = output.ToString();
            StringAssert.Contains(outputString, "5 – Видалити об’єкт");
            StringAssert.Contains(outputString, "Enter item index to delete: ");
            StringAssert.Contains(outputString, "Element 1 removed");
        }

        [TestMethod]
        public void Delete_EmptyDatabase_PrintsEmptyMessage()
        {
            // Arrange
            var input = new StringReader("");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.Delete();

            // Assert
            Assert.AreEqual(0, Program.database.Count, "Database should remain empty.");
            var outputString = output.ToString();
            StringAssert.Contains(outputString, "5 – Видалити об’єкт");
            StringAssert.Contains(outputString, "Create at least 1 machine");
        }

        [TestMethod]
        public void Delete_InvalidIndexInput_RetriesUntilValid()
        {
            // Arrange
            Program.database.Add(new WaterVendingMachine { Address = "1 floor", OperatorName = "Tom", Phone = "123456", CompanyName = "Aqua", WaterCapacityLiters = 1000 });
            WaterVendingMachine.Count = 1;
            var input = new StringReader("abc\n8\n0\n"); // Invalid, out-of-range, valid
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            // Act
            Program.Delete();

            // Assert
            Assert.AreEqual(0, Program.database.Count);
            Assert.AreEqual(0, WaterVendingMachine.Count);
            var outputString = output.ToString();
            StringAssert.Contains(outputString, "Invalid format");
            StringAssert.Contains(outputString, "Value is out of range");
            StringAssert.Contains(outputString, "Element 0 removed");
            StringAssert.Contains(outputString, "Database is empty.");
        }
    }
}