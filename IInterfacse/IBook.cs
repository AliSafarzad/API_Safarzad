using Dapper;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace IInterfacse
{

    
    public interface IBook
    {
       Result CheckUserPassword(User user);
       int InsertBook(Book book);
        int UpdateBook(UpdateBook book);
        List<ListBook> SelectBooks();
        ListBook SelectBooksById(BookById bookById);

        int DeleteBook(BookById book);

    }


    public class UserService : IBook
    {
        private readonly Context _context;
        public UserService(Context context) { _context = context; }

        public Result CheckUserPassword(User user)
        {
            Result result = new Result();
            result.Success = false;
            using (var connection = _context.CreateConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", user.username);
                parameters.Add("@Password", user.password);
                int _result = connection.QuerySingleOrDefault<int>("SP_CHECKUserPaswword", parameters, commandType: CommandType.StoredProcedure);
                if (_result != 1)
                {
                    result.Success = false;
                    result.Error = _result;
                }

                else if (_result == 1)
                {
                    result.Success = true;
                   
                }

                return result;
            }
           
        }

        public int DeleteBook(BookById book)
        {
            using (var connection = _context.CreateConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IDBook", book.IDBook);
               
                int _result = connection.QuerySingleOrDefault<int>("SP_DelectBook", parameters, commandType: CommandType.StoredProcedure);

                return _result;
            }
        }

        public int InsertBook(Book book)
        {

            using (var connection = _context.CreateConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BookName", book.BookName);
                parameters.Add("@Nevisande", book.Nevisande);
                parameters.Add("@Price", book.Price);
                parameters.Add("@Mozo", book.Mozo);
                int _result = connection.QuerySingleOrDefault<int>("SP_NewBook", parameters, commandType: CommandType.StoredProcedure);

                return _result;
            }
        }

        public List<ListBook> SelectBooks()
        {
            using (var connection = _context.CreateConnection())
            {
                
                List<ListBook> _book = connection.Query<ListBook>("SP_Select_All_Book", commandType: CommandType.StoredProcedure).ToList();

                return _book;
            }
        }

        public ListBook SelectBooksById(BookById bookById)
        {
            using (var connection = _context.CreateConnection())
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Idbook", bookById.IDBook);
                ListBook _book = connection.Query<ListBook>("SP_Select_Book_ById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return _book;
            }
        }

        public int UpdateBook(UpdateBook book)
        {
            using (var connection = _context.CreateConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Idbook", book.IdBook);
                parameters.Add("@BookName", book.BookName);
                parameters.Add("@Nevisande", book.Nevisande);
                parameters.Add("@Price", book.Price);
                parameters.Add("@Mozo", book.Mozo);
                int _result = connection.QuerySingleOrDefault<int>("SP_EditBook", parameters, commandType: CommandType.StoredProcedure);

                return _result;
            }
        }
    }
}


