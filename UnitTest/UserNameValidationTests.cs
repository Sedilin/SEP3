using System;
using NUnit.Framework;
using Application.Logic;
using Domain.DTOs;

namespace TestProject1
{
    [TestFixture]
    public class UserNameValidationTests
    {
        [Test]
        public void ValidateData_UserNameLessThan3Characters_ThrowsException()
        {
            // Arrange
            var userToCreate = new UserCreationDto ("ab",  "1234","User"  );

            // Act and assert
            Assert.Throws<Exception>(() => ValidateData(userToCreate));
        }

        [Test]
        public void ValidateData_UserNameGreaterThan15Characters_ThrowsException()
        {
            // Arrange
            var userToCreate = new UserCreationDto ("abasdfghjsdfghjsdfghj",  "1234","User" )  ;

            // Act and assert
            Assert.Throws<Exception>(() => ValidateData(userToCreate));
        }

        [Test]
        public void ValidateData_UserNameBetween3And15Characters_DoesNotThrowException()
        {
            // Arrange
            var userToCreate = new UserCreationDto ("abcde",  "1234","User" );

            // Act and assert
            Assert.DoesNotThrow(() => ValidateData(userToCreate));
        }
        
        private static void ValidateData(UserCreationDto userToCreate)
        {
            string userName = userToCreate.UserName;

            if (userName.Length < 3)
                throw new Exception("Username must be at least 3 characters!");

            if (userName.Length > 15)
                throw new Exception("Username must be less than 16 characters!");
        }
    }
    
   
}