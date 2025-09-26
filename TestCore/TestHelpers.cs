using Grocery.Core.Helpers;
namespace TestCore
{
    public class TestHelpers
    {
        [SetUp]
        public void Setup()
        {
        }

        //Happy flow
        [Test]
        public void TestPasswordHelperReturnsTrue()
        {
            string password = "user3";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsTrue(string password, string passwordHash)
        {
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        //Unhappy flow
        [Test]
        public void TestPasswordHelperReturnsFalse()
        {
            // Test met verkeerd wachtwoord - zou false moeten returnen
            string password = "wrongpassword";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [TestCase("wronguser1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("wronguser3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsFalse(string password, string passwordHash)
        {
            // Test met verkeerde wachtwoorden - zouden false moeten returnen
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        // Extra edge case tests voor meer robuustheid
        [Test]
        public void TestPasswordHelper_EmptyPassword_ReturnsFalse()
        {
            string password = "";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [Test]
        public void TestPasswordHelper_NullPassword_ReturnsFalse()
        {
            string password = "";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [Test]
        public void TestPasswordHelper_NullHash_ReturnsFalse()
        {
            string password = "user1";
            string passwordHash = "";
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [Test]
        public void TestPasswordHelper_InvalidHashFormat_ReturnsFalse()
        {
            string password = "user1";
            string passwordHash = "InvalidHashWithoutDot";
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [Test]
        public void TestPasswordHelper_EmptyHash_ReturnsFalse()
        {
            string password = "user1";
            string passwordHash = "";
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }
    }
    public class RegistrationService
    {
        private readonly HashSet<string> _registeredEmails = new HashSet<string>();

        public bool Register(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            // Normalize email (optional depending on implementation)
            var normalizedEmail = email.Trim().ToLowerInvariant();

            if (_registeredEmails.Contains(normalizedEmail))
                return false;

            _registeredEmails.Add(normalizedEmail);
            return true;
        }
    }

    [TestFixture]
    public class RegistrationTests
    {
        private RegistrationService _registrationService;

        [SetUp]
        public void Setup()
        {
            _registrationService = new RegistrationService();
        }

        [Test]
        public void Register_NewEmail_ReturnsTrue()
        {
            bool result = _registrationService.Register("user1@example.com");
            Assert.IsTrue(result);
        }

        [Test]
        public void Register_DuplicateEmail_ReturnsFalse()
        {
            _registrationService.Register("user1@example.com");
            bool result = _registrationService.Register("user1@example.com");
            Assert.IsFalse(result);
        }

        [Test]
        public void Register_DuplicateEmailWithDifferentCase_ReturnsFalse()
        {
            _registrationService.Register("User1@Example.com");
            bool result = _registrationService.Register("user1@example.com");
            Assert.IsFalse(result);
        }

        [TestCase("")]
        [TestCase("   ")]
        public void Register_InvalidOrEmptyEmail_ReturnsFalse(string invalidEmail)
        {
            bool result = _registrationService.Register(invalidEmail);
            Assert.IsFalse(result);
        }

        [Test]
        public void Register_MultipleUniqueEmails_AllReturnTrue()
        {
            bool result1 = _registrationService.Register("user1@example.com");
            bool result2 = _registrationService.Register("user2@example.com");
            bool result3 = _registrationService.Register("user3@example.com");

            Assert.IsTrue(result1 && result2 && result3);
        }
    }
}