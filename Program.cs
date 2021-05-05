using System;
using System.Data;
using System.Data.SqlClient;

namespace ADOExampleProject
{
    class Program
    {
        string conString;
        SqlConnection con;
        SqlCommand cmd;
        public Program()
        {
            conString = @"server=LAPTOP-3BGPN85C\SQLEXPRESS;Integrated security=true;Initial catalog=pubs";
            con = new SqlConnection(conString);
        }
        void FetchAuthorsFromDatabase()
        {
            string strCmd = "select * from tblMovie";
            cmd = new SqlCommand(strCmd, con);
            try
            {
                con.Open();
               // Console.WriteLine("please enter the id");
                //int id = Convert.ToInt32(Console.ReadLine());
                //cmd.Parameters.Add("@mid", SqlDbType.Int);
                //cmd.Parameters[0].Value = id;
                SqlDataReader drMovies = cmd.ExecuteReader();
                while(drMovies.Read())
                {
                    //Console.WriteLine("author id : "+drAuthors[0]);
                    //Console.WriteLine("author first name : "+drAuthors[1]);
                    //Console.WriteLine("author last name : "+drAuthors[2]);
                    //Console.WriteLine("author phone : "+drAuthors[3]);
                    //Console.WriteLine("-------------------------------");
                    Console.WriteLine("movie id : " + drMovies[0].ToString());
                    Console.WriteLine("movie name : " + drMovies[1]);
                    Console.WriteLine("movie duration : " + drMovies[2].ToString());
                   
                    Console.WriteLine("-------------------------------");

                }
            }
            catch(SqlException sqlExeception)
            {
                Console.WriteLine(sqlExeception.Message);

            }
            finally
            {
                con.Close();
            }
        }
        void PrintMovieById()
        {
            string strCmd = "select * from tblMovie where id=@mid";
            cmd = new SqlCommand(strCmd, con);
            try
            {
                con.Open();
                Console.WriteLine("please enter the id");
                int id = Convert.ToInt32(Console.ReadLine());
                cmd.Parameters.Add("@mid", SqlDbType.Int);
                cmd.Parameters[0].Value = id;
                SqlDataReader drMovies = cmd.ExecuteReader();
                while (drMovies.Read())
                { 
                    Console.WriteLine("movie id : " + drMovies[0].ToString());
                    Console.WriteLine("movie name : " + drMovies[1]);
                    Console.WriteLine("movie duration : " + drMovies[2].ToString());

                    Console.WriteLine("-------------------------------");

                }
            }
            catch (SqlException sqlExeception)
            {
                Console.WriteLine(sqlExeception.Message);

            }
            finally
            {
                con.Close();
            }
        }
        void AddMovie()
        {
            Console.WriteLine("please enter the movie name");
            string mName = Console.ReadLine();
            Console.WriteLine("please enter the movie duration");
            float mDuration = (float)Math.Round(float.Parse(Console.ReadLine()), 2);
            string strCmd = "insert into tblMovie(name,duration) values(@mname,@mdur)";
            cmd = new SqlCommand(strCmd, con);
            cmd.Parameters.AddWithValue("@mname", mName);
            cmd.Parameters.AddWithValue("@mdur", mDuration);
            try
            {
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if(result>0)
                {
                    Console.WriteLine("movie inserted");
                }
                else
                {
                    Console.WriteLine("no no not done");
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        void UpdateMovieDuration()
        {
            Console.WriteLine("please enter the  id");
            // string mName = Console.ReadLine();
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("please enter the movie duration");
            float mDuration = (float)Math.Round(float.Parse(Console.ReadLine()), 2);
            string strCmd = "update tblMovie set duration=@mduration where id=@mid";
            cmd = new SqlCommand(strCmd,con);
            cmd.Parameters.AddWithValue("@mid", id);
            cmd.Parameters.AddWithValue("@mduration", mDuration);
            try
            {
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("movie updated");
                }
                else
                {
                    Console.WriteLine("no no not done");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        void DeleteMovie()
        {
            Console.WriteLine("please enter the  id");
            // string mName = Console.ReadLine();
            int id = Convert.ToInt32(Console.ReadLine());
            //float mDuration = (float)Math.Round(float.Parse(Console.ReadLine()), 2);
            string strCmd = "delete from tblMovie where id=@mid";
            cmd = new SqlCommand(strCmd, con);
            cmd.Parameters.AddWithValue("@mid", id);
           // cmd.Parameters.AddWithValue("@mduration", mDuration);
            try
            {
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("movie deleted");
                }
                else
                {
                    Console.WriteLine("no no not done");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        void PrintMenu()
        {
            Program program = new Program();
            int choice = 0;
            do
            {
                Console.WriteLine("1.add movie");
                Console.WriteLine("2.update movie duration");
                Console.WriteLine("3.print all movies in database");
                Console.WriteLine("4.print any movie with id");
                Console.WriteLine("5.delete movie");
                Console.WriteLine("6.exit");
                Console.WriteLine("please enter your choice");
                choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        
                        program.AddMovie();
                        
                        break;
                    case 2:
                       
                        program.UpdateMovieDuration();
                        break;
                    case 3:
                        
                        program.FetchAuthorsFromDatabase();
                        break;
                    case 4:
                        
                        program.PrintMovieById();
                        break;
                    case 5:
                       
                        program.DeleteMovie();
                        break;
                    case 6:
                        break;
                    default:
                        Console.WriteLine("invalid entry");
                        break;
                }
            } while (choice != 5);
        }
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            Program program = new Program();
            // program.AddMovie();
            //program.DeleteMovie();
            // program.FetchAuthorsFromDatabase();
            // program.UpdateMovieDuration();
            program.PrintMenu();
            Console.ReadKey();
        }
    }
}
