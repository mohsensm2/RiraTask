using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRira.Core;
using TestRira.Core.Entities;
using TestRira.Core.interfaces;
using TestRira.Data;


namespace TestRira.Application
{
    public class UsersService : IUsersService
    {
        private dbContexts _dbContext;
        public UsersService(dbContexts db)
        {
            _dbContext = db;
        }
        public byte[] addUser(byte[] user)
        {
            try
            {
                var person = new Person();
                var newUser = Users.Parser.ParseFrom(user);
                person.Name = newUser.Nam;
                person.LastName = newUser.LastName;
                person.NationCode = newUser.NationCode;
                person.BirthDate = DateTime.Now;
                _dbContext.Person.Add(person);
                _dbContext.SaveChanges();
                var resultService = new result
                {
                    StatusCode = 0,
                    StatusMessage = "عملیات با موفقیت انجام شد"
                };
                return resultService.ToByteArray();
            }
            catch (Exception ex)
            {

                var res = new result
                {
                    StatusCode = -1,
                    StatusMessage = "اشکال در ثبت اطلاعات"
                };
                return res.ToByteArray();
            }
        }
        public byte[] updateUsers(byte[] user)
        {

            var users = updateUser.Parser.ParseFrom(user);
            var person = _dbContext.Person.Where(a => a.Id == users.Id).FirstOrDefault();
            person.Name = users.Name;
            person.LastName = users.LastName;
            person.NationCode = users.NationCode;
            try
            {
                _dbContext.Person.Update(person);
                _dbContext.SaveChanges();
                var resultmessage = new result
                {
                    StatusCode = 0,
                    StatusMessage = "تغییرات انجام شد"
                };
                var result = resultmessage.ToByteArray();
                return result;
            }
            catch (Exception ex)
            {

                var resultmessage = new result
                {
                    StatusCode = -100,
                    StatusMessage = "خطا در انجام عملیات"
                };
                var result = resultmessage.ToByteArray();
                return result;
            }
        }
        public byte[] deleteUser(byte[] user)
        {
            try
            {
                var item = delete.Parser.ParseFrom(user);
                var pers = _dbContext.Person.Where(a => a.Id == item.Id).FirstOrDefault();
                _dbContext.Remove(pers);
                _dbContext.SaveChanges();
                var resulmessage = new result
                {
                    StatusCode = 0,
                    StatusMessage = "عملیات با موفقیت انجام شد"
                };
                return resulmessage.ToByteArray();
            }
            catch (Exception ex)
            {

                var resulmessage = new result
                {
                    StatusCode = -100,
                    StatusMessage = "خطا در انجام عملیات"
                };
                return resulmessage.ToByteArray();
            }
        }
        public byte[] listOfUsers()
        {
            try
            {
                var item = _dbContext.Person.ToList();
                var resultMessage = new ListOfUSer();
                foreach (var x in item)
                {
                    resultMessage.UserList.Add(new Users { 
                        BirthDate=x.BirthDate.ToString(),
                        LastName=x.LastName,
                        Nam=x.Name,
                        NationCode=x.NationCode,
                        Id=x.Id
                    });
                }
                var result = resultMessage.ToByteArray();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }            

        }
        public byte[] getByUser(byte[] user)
        {
            var us = delete.Parser.ParseFrom(user);
            var item = _dbContext.Person.Where(a => a.Id == us.Id).FirstOrDefault();
            var model = new Users
            {
                BirthDate = item.BirthDate.ToString(),
                LastName = item.LastName,
                Id = item.Id,
                Nam = item.Name,
                NationCode = item.NationCode,
            };
            var result= model.ToByteArray();
            return result;
        }
    }
}
