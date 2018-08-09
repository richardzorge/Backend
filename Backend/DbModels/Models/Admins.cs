using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    [DataContract]
    public partial class Admins:Object, IComparable
    {
        private static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }

        public static Admins Deserialize(Stream stream)
        {
            Logger.Trace("Admins.Deserialize() IN");
            if (stream == null)
                return null;
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Admins));
                stream.Position = 0;
                Admins result = (Admins)serializer.ReadObject(stream);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Admins deserialization error");
            }
            finally
            {
                Logger.Trace("Admins.Deserialize() OUT");
            }
            return null;
        }

        public static void Add(string userLogin, string password, string fullName, string comment = null)
        {
            try
            {
                Logger.Trace("Admins.Add IN");
                Logger.Debug("Admins.Add userLogin: {0}", userLogin);
                Logger.Debug("Admins.Add fullName: {0}", fullName);
                Logger.Debug("Admins.Add comment: {0}", comment);
                using (cap01devContext db = new cap01devContext())
                {
                    Admins item = new Admins() { Email = userLogin.ToLower(), Password = password, FullName = fullName };
                    item.Comment = comment;
                    db.Admins.Add(item);
                    db.SaveChanges();
                }
            }
            catch(ValidationException ex)
            {
                Logger.Error(ex, "Add new user to DB validation error");
                throw ex;
            }
            catch(Exception ex)
            {
                Logger.Error(ex, "Add new user to DB error");
                throw new Exception("Add new user to DB error", ex);
            }
            finally
            {
                Logger.Trace("Admins.Add OUT");
            } 
        }
        public static void Add(Admins Item)
        {
            using (cap01devContext db = new cap01devContext())
            {
                Add(Item, db);
            }
        }
        public static void Add(Admins Item, cap01devContext db)
        {
            try
            {
                if (db == null)
                    throw new ApplicationException("Database connection not initialized");
                Logger.Trace("Admins.Add IN");
                if(Item!=null)
                {
                    Logger.Debug("Admins.Add userLogin: {0}", Item.Email);
                    Logger.Debug("Admins.Add fullName: {0}", Item.FullName);
                    Logger.Debug("Admins.Add comment: {0}", Item.Comment);
                }
                else
                {
                    Logger.Debug("Admins.Add Item==null");
                    return;
                }

                if(string.IsNullOrEmpty(Item.Email))
                {
                    throw new ValidationException("Missing required property: Email");
                }
                if (string.IsNullOrEmpty(Item.PasswordHash))
                {
                    throw new ValidationException("Missing required property: PasswordHash");
                }
                if (string.IsNullOrEmpty(Item.FullName))
                {
                    throw new ValidationException("Missing required property: FullName");
                }


                db.Admins.Add(Item);
                db.SaveChanges();

            }
            catch(ValidationException ex)
            {
                Logger.Error(ex, "Add new user to DB error");
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Add new user to DB error");
                throw new Exception("Add new user to DB error", ex);
            }
            finally
            {
                Logger.Trace("Admins.Add OUT");
            }
        }

        public static void Setpassword(string userLogin, string password)
        {
            try
            {
                Logger.Trace("Admins.Setpassword IN");
                Logger.Debug(string.Format("Admins.Setpassword userLogin: {0}", userLogin));
                using (cap01devContext db = new cap01devContext())
                {
                    Admins item = GetUserByLogin(userLogin,db);
                    if (item!=null)
                    {
                        Logger.Debug("User founded");
                        Logger.Debug("Admins.Setpassword user_id: {0}", item.Id);
                        item.Password = password;
                        db.SaveChanges();
                    }
                    else
                    {
                        string mes = $"User {userLogin} not founded";
                        Logger.Debug(mes);
                        throw new KeyNotFoundException(mes);
                    }
                }
            }
            catch(ValidationException ex)
            {
                Logger.Debug($"Update user error: {ex.Message}");
                throw ex;
            }
            catch (KeyNotFoundException ex)
            {
                Logger.Debug( $"Update user error: {ex.Message}");
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Update user error");
                throw new Exception("Update user error", ex);
            }
            finally
            {
                Logger.Trace("Admins.Setpassword OUT");
            }
        }

        public static bool TestPassword(string userLogin, string password)
        {
            try
            {
                Logger.Trace("Admins.TestPassword IN");
                Logger.Debug("Admins.TestPassword userLogin: {0}", userLogin);
                using (cap01devContext db = new cap01devContext())
                {
                    Admins item = GetUserByLogin(userLogin,db);
                    if (item != null)
                    {
                        Logger.Debug("User founded");
                        Logger.Debug("Admins.TestPassword user_id: {0}", item.Id);
                        bool result = item.TestPassword(password);
                        Logger.Debug("Admins.TestPassword result: {0}", result);
                        return result;
                    }
                    else
                    {
                        string mes = $"User {userLogin} not founded";
                        Logger.Debug(mes);
                        throw new KeyNotFoundException(mes);
                    }
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Test user password error");
                throw new Exception("Test user password error", ex);
            }
            finally
            {
                Logger.Trace("Admins.TestPassword OUT");
            }
        }

        public static void Enable(string userLogin, cap01devContext db)
        {
            try
            {
                Logger.Trace("Admins.Enable IN");
                Logger.Debug("Admins.Enable userLogin: {0}", userLogin);
                
                    Admins item = GetUserByLogin(userLogin,db);
                    if (item != null)
                    {
                        Logger.Debug("User founded");
                        Logger.Debug("Admins.Enable user_id: {0}", item.Id);
                        item.Enable();
                        db.Update<Admins>(item);
                        db.SaveChanges();
                    }
                    else
                    {
                        Logger.Debug("User not founded");
                        throw new KeyNotFoundException("User not founded");
                    }
                
            }
            catch (KeyNotFoundException ex)
            {
                Logger.Error(ex, $"User {userLogin} not exist");
                throw new KeyNotFoundException("Test user password error", ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Enable user error");
                throw new Exception("Enable user error", ex);
            }
            finally
            {
                Logger.Trace("Admins.Enable OUT");
            }
        }
        public static void Enable(string userLogin)
        {
            using (cap01devContext db = new cap01devContext())
            {
                Enable(userLogin, db);
            }
        }


        public static void Disable(string userLogin, cap01devContext db)
        {
            try
            {
                Logger.Trace("Admins.Disable IN");
                Logger.Debug("Admins.Disable userLogin: {0}", userLogin);

                Admins item = GetUserByLogin(userLogin,db);
                if (item != null)
                {
                    Logger.Debug("User founded");
                    Logger.Debug("Admins.Disable user_id: {0}", item.Id);
                    item.Disable();
                    db.Update<Admins>(item);
                    db.SaveChanges();
                }
                else
                {
                    Logger.Debug("User not founded");
                    throw new KeyNotFoundException("User not founded");
                }
            }
            catch (KeyNotFoundException ex)
            {
                Logger.Error(ex, $"User {userLogin} not exist");
                throw new KeyNotFoundException("Test user password error", ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Disable user error");
                throw new Exception("Disable user error", ex);
            }
            finally
            {
                Logger.Trace("Admins.Disable OUT");
            }
        }
        public static void Disable(string userLogin)
        {
            using (cap01devContext db = new cap01devContext())
            {
                Disable(userLogin, db);
            }
        }

        public static void Delete(string userLogin)
        {
            using (cap01devContext db = new cap01devContext())
            {
                Delete(userLogin, db);
            }
        }
        public static void Delete(string userLogin, cap01devContext db)
        {
            try
            {
                Logger.Trace("Admins.Delete IN");
                Logger.Debug("Admins.Delete userLogin: {0}", userLogin);
                Admins item = GetUserByLogin(userLogin,db);

                if (item != null)
                {
                    Logger.Debug("User founded");
                    Logger.Debug("Admins.Delete user_id: {0}", item.Id);
                    Logger.Debug("Admins.Delete User with user_id {0} and login {1} will be deleted", item.Id, item.Email);
                    db.Admins.Remove(item);
                    db.Remove<Admins>(item);
                    db.SaveChanges();
                    Logger.Debug("Admins.Del User deleted", item.Id, item.Email);
                }
                else
                {
                    string mes = $"User {userLogin} not founded";
                    Logger.Debug(mes);
                    throw new KeyNotFoundException(mes);
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Delete user error");
                throw new Exception("Delete user error", ex);
            }
            finally
            {
                Logger.Trace("Admins.Delete OUT");
            }
        }
        public static Admins GetUserByLogin(string userLogin)
        {
            using (cap01devContext db = new cap01devContext())
            {
                return GetUserByLogin(userLogin, db);
            }
        }
        public static Admins GetUserByLogin(string userLogin, cap01devContext db)
        {
            try
            {
                Logger.Trace("Admins.GetUserByLogin IN");
                Logger.Debug("Admins.GetUserByLogin userLogin: {0}", userLogin);

                string lowerUserLogin = userLogin.ToLower();
                //Admins item = db.Admins.Where(row => row.Email.Equals(userLogin.ToLower())).FirstOrDefault();
                Admins item = db.Admins.FirstOrDefault(elem => elem.Email == lowerUserLogin);
                if (item != null)
                {
                    Logger.Debug("User founded");
                    return item;
                }
                else
                {
                    Logger.Debug("User not founded");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "GetUserByLogin error");
                throw new Exception("GetUserByLogin error", ex);
            }
            finally
            {
                Logger.Trace("Admins.Delete OUT");
            }
        }
        public static void AddOrUpdate(Admins Item)
        {
            using (cap01devContext db = new cap01devContext())
            {
                AddOrUpdate(Item, db);
            }
        }
        public static void AddOrUpdate(Admins Item, cap01devContext db)
        {
            try
            {
                Logger.Trace("Admins.Update IN");

                db.Update<Admins>(Item);
                db.SaveChanges();
            
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Update user error");
                throw new Exception("Update user error", ex);
            }
            finally
            {
                Logger.Trace("Admins.Delete OUT");
            }
        }


        public static List<Admins> All(cap01devContext db)
        {
            try
            {
                Logger.Trace("Admins.All IN");
                List<Admins> result = new List<Admins>();
                result.AddRange(db.Admins.ToList<Admins>());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Get user list error");
                throw new Exception("Get user list error", ex);
            }
            finally
            {
                Logger.Trace("Admins.TestPassword OUT");
            }
        }
        public static List<Admins> All()
        {
            using (cap01devContext db = new cap01devContext())
            {
                return All(db);
            }
        }

        public static void Clear()
        {
            using (cap01devContext db = new cap01devContext())
            {
                Clear(db);
            }
        }
        public static void Clear(cap01devContext db)
        {
            try
            {
                Logger.Trace("Admins.Clear IN");
                List<Admins> result = new List<Admins>();

                db.Admins.RemoveRange(All(db));
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Delete all admins error");
                throw new Exception("Delete all admins error", ex);
            }
            finally
            {
                Logger.Trace("Admins.Clear OUT");
            }
        }


        public string Password
        {
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Password should not be null or empty or spaces");
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(value);
                UpdateDate = DateTime.Now;
            }
        }


        public Guid Id { get; set; }

        private string _Email = string.Empty;
        [DataMember]
        [Required(ErrorMessage = "Email is required.")]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ValidationException("Email should not be null or empty");
                }
                if (value.Length > 32)
                {
                    throw new ValidationException("Email should not be longer than 32 characters.");
                }
                _Email = value;
            }
        }


        private string _PasswordHash;
        [DataMember]
        [Required(ErrorMessage = "PasswordHash is required.")]
        [StringLength(60, MinimumLength = 0, ErrorMessage = "PasswordHash should not be longer than 60 characters.")]
        public string PasswordHash
        {
            get
            {
                return _PasswordHash;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ValidationException("PasswordHash should not be null or empty");
                }
                if (value.Length > 60)
                {
                    throw new ValidationException("PasswordHash should not be longer than 60 characters.");
                }
                SetPasswordHash(value);
            }
        }

        private bool? _IsActive;
        public bool? IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
            }
        }

        private string _FullName=string.Empty;
        [StringLength(32, MinimumLength = 2, ErrorMessage = "FullName should not be longer than 32 characters.")]
        [DataMember]
        public string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ValidationException("FullName should not be null or empty");
                }
                if (value.Length > 32)
                {
                    throw new ValidationException("FullName should not be longer than 32 characters.");
                }
                UpdateDate = DateTime.Now;
                _FullName = value;
            }
        }

        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        private string _Comment=string.Empty;
        [DataMember]
        [StringLength(256, MinimumLength = 0, ErrorMessage = "Comment should not be longer than 256 characters.")]
        public string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                if (value.Length > 32)
                {
                    throw new ValidationException("FullName should not be longer than 256 characters.");
                }
                _Comment = value;
            }
        }

        public bool TestPassword(string Password)
        {
            return BCrypt.Net.BCrypt.Verify(Password, this.PasswordHash);
        }

        public Admins()
        {
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            IsActive = false;
        }

        public void Enable()
        {
            this.IsActive = true;
            UpdateDate = DateTime.Now;
        }

        public void Disable()
        {
            this.IsActive = false;
            UpdateDate = DateTime.Now;
        }

        public void SetComment(string Comment)
        {
            this.Comment = Comment;
            this.UpdateDate = DateTime.Now;
        }
        public void SetPasswordHash(string PasswordHash)
        {
            this._PasswordHash = PasswordHash;
            this.UpdateDate = DateTime.Now;
        }

        public void SetFullName(string FullName)
        {
            this.FullName = FullName;
            this.UpdateDate = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("Admins< Email={0}, FullName={1}, Comment={2}>", this.Email,this.FullName, this.Comment);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            Admins item = obj as Admins;
            return Email.CompareTo(item.Email);
        }

        public bool Equals(Admins Item)
        {
            return this.CompareTo(Item) == 0;
        }

        public void Prepare()
        {
            _PasswordHash = string.Empty;

        }
    }
}
