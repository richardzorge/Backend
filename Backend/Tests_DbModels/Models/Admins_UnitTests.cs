using Backend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class Admins_UnitTests
    {
        [TestMethod]
        public void create_new_empty_item()
        {
            Admins item = null;
            // TEST: create empty Admins item
            try
            {
                item = new Admins();
            }
            catch(Exception ex)
            {
                Assert.Fail("Failed TEST: create empty Admins item");
            }

        }

        [TestMethod]
        public void create_new_item_with_empty_email()
        {
            Admins item = null;
            string Email = string.Empty;
            // TEST: create Admins item with empty Email
            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.Email = Email;
            });
        }

        [TestMethod]
        public void create_new_item_with_longer_email()
        {
            Admins item = null;
            string Email = "email_email_email_email_email_email_email_email_email_email_email_email_email_email_email_email_email_email_email_email_email_";
            // TEST: create Admins item with longer Email
            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.Email = Email;
            });

        }

        [TestMethod]
        public void create_new_item_with_empty_full_name()
        {
            Admins item = null;
            string FullName = string.Empty;
            // TEST: create Admins item with empty FullName
            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.FullName = FullName;
            });
        }

        [TestMethod]
        public void create_new_item_with_longer_full_name()
        {
            Admins item = null;
            string FullName = "fullName_fullName_fullName_fullName_fullName_fullName_fullName_fullName_fullName_fullName_fullName_fullName_fullName_fullName";
            // TEST: create Admins item with longer FullName
            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.FullName = FullName;
            });
        }

        [TestMethod]
        public void create_new_item_with_empty_password_hash()
        {
            Admins item = null;
            string PasswordHash = string.Empty;
            // TEST: create Admins item with empty PasswordHash
            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.PasswordHash = PasswordHash;
            });
        }

        [TestMethod]
        public void create_new_item_with_longer_password_hash()
        {
            // TEST: create Admins item with longer PasswordHash
            Admins item = null;
            string PasswordHash = "passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash";
            
            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.PasswordHash = PasswordHash;
            });
        }

        [TestMethod]
        public void set_empty_or_white_space_full_name()
        {
            // TEST: create Admins item with longer PasswordHash
            Admins item = null;
            string PasswordHash = "passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash";

            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.FullName = string.Empty;
            });
            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.FullName = "  ";
            });

            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.FullName = null;
            });
        }

        [TestMethod]
        public void set_empty_or_white_space_email()
        {
            // TEST: create Admins item with longer PasswordHash
            Admins item = null;
            string PasswordHash = "passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash_passwordHash";

            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.Email = string.Empty;
            });
            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.Email = "  ";
            });

            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.Email = null;
            });
        }

        [TestMethod]
        public void set_empty_or_white_space_password()
        {
            // TEST: create Admins item with longer PasswordHash
            Admins item = null;

            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.Password = string.Empty ;
            });
            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.Password = "  ";
            });
            Assert.ThrowsException<ValidationException>(() =>
            {
                item = new Admins();
                item.Password = null;
            });
        }

        [TestMethod]
        public void test_password()
        {
            // TEST: testing password
            string true_Password = "very_hard_password";
            string false_Password = "!very_hard_password";
            Admins item = new Admins();
            try
            {
                item.Password = true_Password;
            }
            catch(Exception ex)
            {
                Assert.Fail($"Exceptions in item.Password.set: {ex}");
            }
            Assert.IsTrue(item.TestPassword(true_Password));
            Assert.IsFalse(item.TestPassword(false_Password));
        }

        [TestMethod]
        public void test_update_time_changing_if_chaged_comment()
        {
            // TEST: testing update date and time changing, when comment has been changed
            Admins item = new Admins();
            DateTime updateDateTime = item.UpdateDate;
            Thread.Sleep(50);
            item.SetComment("Value");
            Assert.AreNotEqual(updateDateTime, item.UpdateDate);
        }

        [TestMethod]
        public void test_update_time_changing_if_chaged_password_hash()
        {
            // TEST: testing update date and time changing, when PasswordHash has been changed
            Admins item = new Admins();
            DateTime updateDateTime = item.UpdateDate;
            Thread.Sleep(50);
            item.Password = "Value";
            Assert.AreNotEqual(updateDateTime, item.UpdateDate);
        }

        [TestMethod]
        public void test_update_time_changing_if_enabled()
        {
            // TEST: testing update date and time changing, when IsActive has been changed
            Admins item = new Admins();
            DateTime updateDateTime = item.UpdateDate;
            Thread.Sleep(50);
            item.Enable();
            Assert.AreNotEqual(updateDateTime, item.UpdateDate);
        }

        [TestMethod]
        public void test_update_time_changing_if_disabled()
        {
            // TEST: testing update date and time changing, when IsActive has been changed
            Admins item = new Admins();
            DateTime updateDateTime = item.UpdateDate;
            Thread.Sleep(50);
            item.Disable();
            Assert.AreNotEqual(updateDateTime, item.UpdateDate);
        }

        [TestMethod]
        public void test_compare_items()
        {
            // TEST: testing update date and time changing, when IsActive has been changed
            Admins item1 = new Admins() { Email = "email1" };
            Admins item2 = new Admins() { Email = "email2" };

            Assert.AreNotEqual(item1.CompareTo(item2),0);
            Assert.AreEqual(item1.CompareTo(item1), 0);
        }

        [TestMethod]
        public void test_equals_items()
        {
            // TEST: testing update date and time changing, when IsActive has been changed
            Admins item1 = new Admins() { Email = "email1" };
            Admins item2 = new Admins() { Email = "email2" };

            Assert.IsFalse(item1.Equals(item2));
            Assert.IsTrue(item1.Equals(item1));
        }

        [TestMethod]
        public void test_enable()
        {
            // TEST: testing enabling
            Admins item = new Admins() { Email = "email1", IsActive=false};
            item.Enable();
            Assert.IsTrue(item.IsActive.HasValue);
            Assert.IsTrue(item.IsActive.Value);
        }

        [TestMethod]
        public void test_disable()
        {
            // TEST: testing disabling
            Admins item = new Admins() { Email = "email1", IsActive = true };
            item.Disable();
            Assert.IsTrue(item.IsActive.HasValue);
            Assert.IsFalse(item.IsActive.Value);
        }


    }
}
