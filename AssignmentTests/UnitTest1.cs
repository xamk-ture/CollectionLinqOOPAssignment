using CollectionLinqAssignment;
using static CollectionLinqAssignment.Assignments;

namespace AssignmentTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(null, new int[] { })]
        [InlineData(new int[] { }, new int[] { })]
        [InlineData(new int[] { 1, 3, 5 }, new int[] { })]
        [InlineData(new int[] { 2, 4, 6 }, new int[] { 2, 4, 6 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, new int[] { 2, 4 })]
        public void FindEvenNumbers_GivenNumbers_ReturnsEvenNumbers(int[]? input, int[]? expected)
        {
            // Act
            var result = Assignments.FindEvenNumbers(input);
            
            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(FindStringsStartingWithAData))]
        public void FindStringsStartingWithA_GivenStrings_ReturnsStringsStartingWithA(List<string> input, List<string> expected)
        {
            // Act
            var result = Assignments.FindStringsStartingWithA(input);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> FindStringsStartingWithAData()
        {
            yield return new object[] { null, new List<string>() };
            yield return new object[] { new List<string>(), new List<string>() };
            yield return new object[] { new List<string> { "Apple", "Banana", "Cherry" }, new List<string> { "Apple" } };
            yield return new object[] { new List<string> { "Apple", "Apricot" }, new List<string> { "Apple", "Apricot" } };
            yield return new object[] { new List<string> { "Banana", "Cherry" }, new List<string>() };
        }

        [Theory]
        [MemberData(nameof(FindCitiesData))]
        public void FindCitiesWithPopulationOverOneMillion_GivenCities_ReturnsFilteredCities(Dictionary<string, int>? input, Dictionary<string, int>? expected)
        {
            // Act
            var result = Assignments.FindCitiesWithPopulationOverOneMillion(input);

            // Assert
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> FindCitiesData()
        {
            yield return new object[] { null, new Dictionary<string, int>() };
            yield return new object[] { new Dictionary<string, int>(), new Dictionary<string, int>() };
            yield return new object[] { new Dictionary<string, int> { { "CityA", 900_000 }, { "CityB", 800_000 } }, new Dictionary<string, int>() };
            yield return new object[] { new Dictionary<string, int> { { "CityA", 1_100_000 }, { "CityB", 1_200_000 } }, new Dictionary<string, int> { { "CityA", 1_100_000 }, { "CityB", 1_200_000 } } };
            yield return new object[] { new Dictionary<string, int> { { "CityA", 900_000 }, { "CityB", 1_200_000 } }, new Dictionary<string, int> { { "CityB", 1_200_000 } } };
        }


        [Theory]
        [MemberData(nameof(ProductTestData))]
        public void FilterElectronicsUnder100_GivenList_ReturnsMatchingProducts(List<Assignments.Product> input, List<Assignments.Product> expected)
        {
            var result = Assignments.FilterElectronicsUnder100(input);

            Assert.Equal(expected.Count, result.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, result[i].Name);
                Assert.Equal(expected[i].Price, result[i].Price);
                Assert.Equal(expected[i].Category, result[i].Category);
            }
        }

        public static IEnumerable<object[]> ProductTestData()
        {
            yield return new object[]
            {
                new List<Assignments.Product>
                {
                    new Assignments.Product { Name = "Phone", Price = 500, Category = "Electronics" },
                    new Assignments.Product { Name = "USB Cable", Price = 10, Category = "Electronics" },
                    new Assignments.Product { Name = "Shirt", Price = 50, Category = "Apparel" }
                },
                new List<Assignments.Product>
                {
                    new Assignments.Product { Name = "USB Cable", Price = 10, Category = "Electronics" }
                }
            };

            yield return new object[]
            {
                new List<Assignments.Product>(),
                new List<Assignments.Product>()
            };

            yield return new object[]
            {
                new List<Assignments.Product>
                {
                    new Assignments.Product { Name = "Shirt", Price = 50, Category = "Apparel" },
                    new Assignments.Product { Name = "Hat", Price = 20, Category = "Apparel" }
                },
                new List<Assignments.Product>()
                };

                yield return new object[]
                {
                new List<Assignments.Product>
                {
                    new Assignments.Product { Name = "TV", Price = 1000, Category = "Electronics" },
                    new Assignments.Product { Name = "Laptop", Price = 1200, Category = "Electronics" }
                },
                new List<Assignments.Product>()
            };
        }


        [Theory]
        [MemberData(nameof(PersonTestData))]
        public void ProjectFullNameAndIsAdult_GivenPeople_ReturnsProjection(List<Person> input, List<object> expected)
        {
            var result = ProjectFullNameAndIsAdult(input);

            Assert.Equal(expected.Count, result.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                dynamic expectedItem = expected[i];
                dynamic resultItem = result[i];

                Assert.Equal(expectedItem.FullName, resultItem.FullName);
                Assert.Equal(expectedItem.IsAdult, resultItem.IsAdult);
            }
        }

        public static IEnumerable<object[]> PersonTestData()
        {
            yield return new object[]
            {
                null,
                new List<object>()
            };

            yield return new object[]
            {
                new List<Person>(),
                new List<object>()
            };

            yield return new object[]
            {
                new List<Person>
                {
                    new Person { FirstName = "John", LastName = "Doe", Age = 25 },
                    new Person { FirstName = "Jane", LastName = "Doe", Age = 15 }
                },
                new List<object>
                {
                    new { FullName = "John Doe", IsAdult = true },
                    new { FullName = "Jane Doe", IsAdult = false }
                }
            };
        }

        public static List<object> ProjectFullNameAndIsAdult(List<Person> people)
        {
            if (people == null)
                return new List<object>();

            return people.Select(p => new { FullName = $"{p.FirstName} {p.LastName}", IsAdult = p.Age >= 18 }).ToList<object>();
        }

        [Theory]
        [MemberData(nameof(SalesTestData))]
        public void TotalAmountSoldPerProduct_GivenSales_ReturnsAggregatedAmounts(List<Sale> input, Dictionary<int, decimal> expected)
        {
            var result = TotalAmountSoldPerProduct(input);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> SalesTestData()
        {
            yield return new object[]
            {
                null,
                new Dictionary<int, decimal>()
            };

            yield return new object[]
            {
                new List<Sale>(),
                new Dictionary<int, decimal>()
            };

            yield return new object[]
            {
                new List<Sale>
                {
                    new Sale { ProductId = 1, AmountSold = 10 },
                    new Sale { ProductId = 2, AmountSold = 15 },
                    new Sale { ProductId = 3, AmountSold = 20 }
                },
                new Dictionary<int, decimal>
                {
                    { 1, 10 },
                    { 2, 15 },
                    { 3, 20 }
                }
            };

            yield return new object[]
            {
                new List<Sale>
                {
                    new Sale { ProductId = 1, AmountSold = 10 },
                    new Sale { ProductId = 1, AmountSold = 15 },
                    new Sale { ProductId = 2, AmountSold = 5 }
                },
                new Dictionary<int, decimal>
                {
                    { 1, 25 },
                    { 2, 5 }
                }
            };
        }

        [Theory]
        [MemberData(nameof(StudentTestData))]
        public void TopStudentFromEachGrade_GivenStudents_ReturnsTopStudents(List<Student> input, List<Student> expected)
        {
            var result = TopStudentFromEachGrade(input);

            Assert.Equal(expected.Count, result.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, result[i].Name);
                Assert.Equal(expected[i].GradeLevel, result[i].GradeLevel);
                Assert.Equal(expected[i].GPA, result[i].GPA);
            }
        }

        public static IEnumerable<object[]> StudentTestData()
        {
            yield return new object[]
            {
                null,
                new List<Student>()
            };

            yield return new object[]
            {
                new List<Student>(),
                new List<Student>()
            };

            yield return new object[]
            {
                new List<Student>
                {
                    new Student { Name = "Alice", GradeLevel = 9, GPA = 3.5 },
                    new Student { Name = "Bob", GradeLevel = 10, GPA = 3.8 },
                    new Student { Name = "Charlie", GradeLevel = 9, GPA = 3.9 }
                },
                new List<Student>
                {
                    new Student { Name = "Charlie", GradeLevel = 9, GPA = 3.9 },
                    new Student { Name = "Bob", GradeLevel = 10, GPA = 3.8 }
                }
            };

            yield return new object[]
            {
                new List<Student>
                {
                    new Student { Name = "Dave", GradeLevel = 11, GPA = 2.5 },
                    new Student { Name = "Eve", GradeLevel = 11, GPA = 3.0 },
                    new Student { Name = "Frank", GradeLevel = 11, GPA = 2.8 }
                },
                new List<Student>
                {
                    new Student { Name = "Eve", GradeLevel = 11, GPA = 3.0 }
                }
            };
        }

        public static IEnumerable<object[]> SchoolData()
        {
            var highSchoolA = new School
            {
                Name = "HighSchool A",
                Students = new List<Student>
                {
                    new Student { Name = "Eva", Grade = 91 },
                    new Student { Name = "Frank", Grade = 85 }
                }
            };

            var highSchoolB = new School
            {
                Name = "HighSchool B",
                Students = new List<Student>
                {
                    new Student { Name = "Grace", Grade = 89 }
                }
            };

            return new List<object[]>
            {
                new object[] { new List<School> { highSchoolA, highSchoolB }, new List<School> { highSchoolA } },
                new object[] { null, new List<School>() }
            };
        }

        [Theory]
        [MemberData(nameof(SchoolData))]
        public void TestSchoolsWithTopGrades(List<School> input, List<School> expected)
        {
            // Act
            var result = SchoolsWithTopGrades(input);

            // Assert
            Assert.Equal(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, result[i].Name);
            }
        }
    }
}