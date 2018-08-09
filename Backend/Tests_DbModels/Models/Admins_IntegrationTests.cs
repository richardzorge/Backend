using Backend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class Admins_IntegrationTests
    {
        ConfigurationRoot Configuration;

        Admins item_with_valid_data = new Admins()
        {
            Email = "email@email.com",
            Password = "Password",
            FullName = "Full Name"
        };

        Admins item_without_email = new Admins()
        {
            Password = "Password",
            FullName = "Full Name"
        };

        Admins item_without_password = new Admins()
        {
            Email = "email@email.com",
            FullName = "Full Name"
        };

        Admins item_without_full_name = new Admins()
        {
            Email = "email@email.com",
            Password = "Password",
        };

        Admins item_enabled = new Admins()
        {
            Email = "email@email.com",
            Password = "Password",
            FullName = "Full Name",
            IsActive = true
        };

        Admins item_disabled = new Admins()
        {
            Email = "email@email.com",
            Password = "Password",
            FullName = "Full Name",
            IsActive = false
        };

        private void Init()
        {
            Settings.Instance = new Settings() { DbConnectionString = Configuration["DatabaseConnectionString"] };
            using (cap01devContext db = new cap01devContext())
            {
                foreach(Admins a in db.Admins)
                {
                    db.Remove<Admins>(a);
                }
                db.SaveChanges();
            }
        }

        public Admins_IntegrationTests()
        {
            string configurationPath = "appsettings.json";
            ConfigurationBuilder confBuilder = new ConfigurationBuilder();
            confBuilder = confBuilder.AddJsonFile(configurationPath, optional: true, reloadOnChange: true) as ConfigurationBuilder;
            Configuration = confBuilder.Build() as ConfigurationRoot;
            Configuration["DatabaseConnectionString"] = string.Format("Host={0};Port={1};Database={2};Username={3};Password={4}", Configuration["Database:Host"], Configuration["Database:Port"], Configuration["Database:Database"], Configuration["Database:User"], Configuration["Database:Password"]);
        }

        [TestMethod]
        public void add_valid_item_to_db()
        {
            Init();
            // TEST: add new Admins with good data to db
            try
            {
                Admins.Add(item_with_valid_data);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed TEST: add_good_item_to_db: {ex}");
            }
            bool result = false;
            using (cap01devContext db = new cap01devContext())
            {
                foreach (Admins a in db.Admins)
                {
                    if (a.Email == item_with_valid_data.Email)
                    {
                        result = true;
                        break;
                    }
                }
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void add_without_email_item_to_db()
        {
            Init();
            // TEST: add new Admins without email to db
            Assert.ThrowsException<ValidationException>(() =>
            {
                Admins.Add(item_without_email);
            });
        }

        [TestMethod]
        public void add_without_full_name_item_to_db()
        {
            Init();
            // TEST: add new Admins without FullName to db
            Assert.ThrowsException<ValidationException>(() =>
            {
                Admins.Add(item_without_full_name);
            });
        }

        [TestMethod]
        public void add_without_password_item_to_db()
        {
            Init();
            // TEST: add new Admins without PasswordHash to db
            Assert.ThrowsException<ValidationException>(() =>
            {
                Admins.Add(item_without_password);
            });
        }



        [TestMethod]
        public void list_items()
        {
            Init();

            try
            {
                Admins.Add(item_with_valid_data);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed TEST: add_good_item_to_db: {ex}");
            }
            List<Admins> items = Admins.All();
            Assert.AreEqual(items.Count, 1);
            Assert.IsTrue(items[0].Equals(item_with_valid_data));
        }

        [TestMethod]
        public void enable_admin()
        {
            Init();

            try
            {
                Admins.Add(item_disabled);
                Admins.Enable(item_disabled.Email);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed TEST: add_good_item_to_db: {ex}");
            }
            List<Admins> items = Admins.All();
            Assert.AreEqual(items.Count, 1);
            Assert.AreEqual(items[0].IsActive, true);
        }

        [TestMethod]
        public void disable_admin()
        {
            Init();

            try
            {
                Admins.Add(item_enabled);
                Admins.Disable(item_enabled.Email);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Failed TEST: add_good_item_to_db: {ex}");
            }
            List<Admins> items = Admins.All();
            Assert.AreEqual(items.Count, 1);
            Assert.AreEqual(items[0].IsActive, false);
        }

        [TestMethod]
        public void not_found_admin()
        {
            Init();
            Assert.IsNull(Admins.GetUserByLogin("not_exist_user"));
        }

        [TestMethod]
        public void delete_admin()
        {
            Init();
            // TEST: add new Admins without email to db
            Admins.Add(item_enabled);
            Admins.Delete(item_enabled.Email);
            Assert.AreEqual(Admins.All().Count, 0);
        }

        [TestMethod]
        public void delete_not_exists_admin()
        {
            Init();
            // TEST: add new Admins without email to db
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                Admins.Delete("not_exist_user");
            });
        }
        [TestMethod]
        public void disable_not_exists_admin()
        {
            Init();
            // TEST: add new Admins without email to db
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                Admins.Disable("not_exist_user");
            });
        }

        [TestMethod]
        public void enable_not_exists_admin()
        {
            Init();
            // TEST: add new Admins without email to db
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                Admins.Enable("not_exist_user");
            });
        }

        [TestMethod]
        public void set_new_password_for_not_exists_admin()
        {
            Init();
            // TEST: add new Admins without email to db
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                Admins.Setpassword("not_exist_user","new_password");
            });
        }

        [TestMethod]
        public void test_password_for_not_exists_admin()
        {
            Init();
            // TEST: add new Admins without email to db
            Assert.ThrowsException<KeyNotFoundException>(() =>
            {
                Admins.TestPassword("not_exist_user", "password");
            });
        }

        [TestMethod]
        public void update_not_exists_admin()
        {
            Init();
            // TEST: add new Admins without email to db
            Admins.AddOrUpdate(item_with_valid_data);
            //Assert.ThrowsException<KeyNotFoundException>(() =>
            //{
                
            //});
        }



    }
}