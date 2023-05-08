using BusEntity;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserBLL
    {
        UserDal usdal = new UserDal();

        private string Encode(string Pass)
        {
            byte[] encData_byte = new byte[Pass.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(Pass);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        private string Decode(string EncodedPass) 
        {
            System.Text.UTF32Encoding encoder = new System.Text.UTF32Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(EncodedPass);   
            int charCount = utf8Decode.GetCharCount(todecode_byte,0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte,0,todecode_byte.Length, decoded_char,0);  
            string result = new string(decoded_char);
            return result;
        }

        public string Create(User u)
        {
            if (usdal.ReadNotExist(u))
            {
                u.Password = Encode(u.Password);
                return usdal.Create(u);
            }
            else
            {
                return "User already registered";
            }
        }

        public DataTable Read()
        {
            return usdal.Read();
        }

        public DataTable Read(string s)
        {
            return usdal.Read(s);
        }

        public DataTable ReadActive()
        {
            return usdal.ReadActive();
        }


        public User Read(int id)
        {
            return usdal.Read(id);
        }

        public User ReadUser(string s)
        {
            return usdal.ReadUser(s);
        }

        public List<User> ReadUsers()
        {
            return usdal.ReadUsers();
        }

        public List<User> ReadUsersActiv()
        {
            return usdal.ReadUsersActiv();
        }

        public List<string> ReadUsernames()
        {
            return usdal.ReadUsernames();
        }

        public int ReadCountAll()
        {
            return usdal.ReadCountAll();
        }
        public int ReadCountAct()
        {
            return usdal.ReadCountAct();
        }

            public String Update(User u, int id)
        {
            u.Password = Encode(u.Password);
            return usdal.Update(u, id);
        }

        public String Delete(int id)
        {
            return usdal.Delete(id);
        }

        public User Login(string user, string pass)
        {
            return usdal.Login(user, Encode(pass));
        }

        public string SetStat(int id)
        {
            return usdal.SetStat(id);
        }

    }
}
