using System;
using System.Linq;
using System.Reflection;
using UnitTesting02.Projects;
using Xunit;

namespace UnitTesting02.ProjectsTests
{
    public class IssueTests
    {
        [Fact]
        public void Constructor_WithIssueDescIsNull_ThrowsInvalidIssueDescriptionException()
        {
            // Arrange 

            // Act
            Action ctor = () => new Issue(null, Priority.Low, Category.Hardware, DateTime.Now);
            
            // Assert

            Assert.Throws<InvalidIssueDescriptionException>(() => ctor());
        }

        [Fact]
        public void Constructor_WithIssueDescIsWhiteSpace_ThrowsInvalidIssueDescriptionException()
        {
            // Arrange 

            // Act
            Action ctor = () => new Issue("   ", Priority.Low, Category.Hardware, DateTime.Now);

            // Assert

            Assert.Throws<InvalidIssueDescriptionException>(() => ctor());
        }

        [Fact]
        public void Constructor_WithIssueNotProvidingCreatedAt_ReturnsCreatedAtCurrentDateTime()
        {
            // Arrange 
            var sut = new Issue("Issue #1", Priority.Low, Category.Hardware);

            // Act
            var actual = sut.CreatedAt;

            // Assert

            Assert.False(actual == default(DateTime));
        }


        [Fact]
        public void GenerateKey_WithIssueValidProperties_Return18CharIssueKey()
        {
            // Arrange 
            var sut = new Issue("Issue #1", Priority.Low, Category.Hardware,
                new DateTime(2022, 10, 11, 12, 30, 00));

            // Act
               MethodInfo methodInfo = typeof(Issue).GetMethod("GenerateKey",
                   BindingFlags.NonPublic | BindingFlags.Instance);

            var actual = methodInfo.Invoke(sut, null).ToString();
           
            var expected = "HW-2022-L-ABCD1234";

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.Length, actual.Length);
         
        }



      
        [Fact]
        public void GenerateKey_WithIssueCategoryHardware_ReturnIssueKeyFirstSegmentHW()
        {
            // Arrange 
            var sut = new Issue("Issue #1", Priority.High, Category.Hardware,
                new DateTime(2022, 10, 11, 12, 30, 00));

            // Act
            MethodInfo methodInfo = typeof(Issue).GetMethod("GenerateKey",
                BindingFlags.NonPublic | BindingFlags.Instance);

            var actual = methodInfo.Invoke(sut, null).ToString();

            var expected = "HW";

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual.Split("-")[0]);

        }

     

        [Fact]
        public void GenerateKey_WithIssuePriorityLow_ReturnIssueKeyThirdSegmentL()
        {
            // Arrange 
            var sut = new Issue("Issue #1", Priority.Low, Category.Hardware,
                new DateTime(2022, 10, 11, 12, 30, 00));

            // Act
            MethodInfo methodInfo = typeof(Issue).GetMethod("GenerateKey",
                BindingFlags.NonPublic | BindingFlags.Instance);

            var actual = methodInfo.Invoke(sut, null).ToString();

            var expected = "L";

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual.Split("-")[2]);

        }

        [Fact]
        public void GenerateKey_WithIssueCreatedAt_ReturnIssueKeySecondSegmentYYYY()
        {
            // Arrange 
            var sut = new Issue("Issue #1", Priority.Low, Category.Hardware,
                new DateTime(2022, 10, 11, 12, 30, 00));

            // Act
            MethodInfo methodInfo = typeof(Issue).GetMethod("GenerateKey",
                BindingFlags.NonPublic | BindingFlags.Instance);

            var actual = methodInfo.Invoke(sut, null).ToString();

            var expected = "2022";

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual.Split("-")[1]);

        }

        [Fact]
        public void GenerateKey_WithIssueValidProperies_ReturnIssueKeyFourthSegment8AlphaNumeric()
        {
            // Arrange 
            var sut = new Issue("Issue #1", Priority.Low, Category.Hardware,
                new DateTime(2022, 10, 11, 12, 30, 00));

            // Act
            MethodInfo methodInfo = typeof(Issue).GetMethod("GenerateKey",
                BindingFlags.NonPublic | BindingFlags.Instance);

            var fourthSegment = methodInfo.Invoke(sut, null).ToString().Split("-")[3];
            var isAlphaNumeric = fourthSegment.All(x => char.IsLetterOrDigit(x));

            // Assert
            Assert.True(isAlphaNumeric); 

        }

        [Theory]
        [InlineData("Issue #1", Priority.Urgent, Category.Software, "2000-10-10", "SW-2000-U-ABCD1234")]
        [InlineData("issue #1", Priority.Low, Category.Software, "2022-10-10", "SW-2022-L-ABCD1234")]
        [InlineData("issue #1", Priority.Low, Category.UnKnown, "2018-10-10", "NA-2018-L-ABCD1234")]
        [InlineData("issue #1", Priority.Low, Category.Hardware, "1992-10-10", "HW-1992-L-ABCD1234")]
        [InlineData("issue #1", Priority.Medium, Category.Hardware, "2003-10-10", "HW-2003-M-ABCD1234")]
        [InlineData("issue #1", Priority.High, Category.Hardware, "2015-10-10", "HW-2015-H-ABCD1234")]
        [InlineData("issue #1", Priority.Urgent, Category.Hardware, "1980-10-10", "HW-1980-U-ABCD1234")]
        public void GenerateKey_WithValidIssueProperties_ReturnsExpectedKey
            (string desc, Priority priority, Category category, string createdAt, string expected)
        {
            // Arrange 
            var sut = new Issue(desc, priority, category, DateTime.Parse(createdAt));

            // Act
            MethodInfo methodInfo = typeof(Issue).GetMethod("GenerateKey",
                BindingFlags.NonPublic | BindingFlags.Instance);

            var actual = methodInfo.Invoke(sut, null).ToString();

         

            // Assert 
            Assert.Equal(expected.Substring(0, 10), actual.Substring(0, 10));

        }
    }
}
