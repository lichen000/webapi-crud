using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using WebApplication1.dto;
using WebApplication1.entity;

namespace WebApplication1.service
{
    public class StudentService
    {
        private const string connStr = "host=localhost;userid=root;password=bstek;database=db_test1;charset=utf8;";

        public Student get(int id)
        {
            string queryStr = "select * from t_student where id = " + id;
            MySqlConnection conn = new MySqlConnection(connStr);
            Student student = null;
            try
            {
                conn.Open();
                MySqlCommand com = new MySqlCommand(queryStr, conn);
                MySqlDataReader reader = com.ExecuteReader();                
                if (reader.HasRows)
                {
                    reader.Read();
                    Int32 idx = reader.GetInt32("id");
                    DateTime create_time = reader.GetDateTime("create_time");
                    DateTime update_time = reader.GetDateTime("update_time");
                    String note = reader.GetString("note");
                    String number = reader.GetString("number");
                    String name = reader.GetString("name");
                    Int32 age = reader.GetInt32("age");

                    student = new Student();
                    student.id = idx;
                    student.create_time = create_time;
                    student.update_time = update_time;
                    student.note = note;
                    student.number = number;
                    student.name = name;
                    student.age = age;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            } finally
            {
                conn.Close();
            }
            

            return student;
        }

        public List<Student> getAll()
        {

            string queryStr = "select * from t_student";
            MySqlConnection conn = new MySqlConnection(connStr);
            List<Student> list = null;
            try
            {
                conn.Open();
                MySqlCommand com = new MySqlCommand(queryStr, conn);
                MySqlDataReader reader = com.ExecuteReader();
                list = new List<Student>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Int32 idx = reader.GetInt32("id");
                        DateTime create_time = reader.GetDateTime("create_time");
                        DateTime update_time = reader.GetDateTime("update_time");
                        String note = reader.GetString("note");
                        String number = reader.GetString("number");
                        String name = reader.GetString("name");
                        Int32 age = reader.GetInt32("age");


                        Student student = new Student();
                        student.id = idx;
                        student.create_time = create_time;
                        student.update_time = update_time;
                        student.note = note;
                        student.number = number;
                        student.name = name;
                        student.age = age;
                        list.Add(student);
                    }
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }


            return list;
        }

        public MyPage<Student> getAllPage(int page, int size)
        {
            int offset = page * size;

            string queryStr = "select * from t_student limit " + size + " offset " + offset; 
            MySqlConnection conn = new MySqlConnection(connStr);
            List<Student> list = null;
            try
            {
                conn.Open();
                MySqlCommand com = new MySqlCommand(queryStr, conn);
                MySqlDataReader reader = com.ExecuteReader();
                list = new List<Student>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Int32 idx = reader.GetInt32("id");
                        DateTime create_time = reader.GetDateTime("create_time");
                        DateTime update_time = reader.GetDateTime("update_time");
                        String note = reader.GetString("note");
                        String number = reader.GetString("number");
                        String name = reader.GetString("name");
                        Int32 age = reader.GetInt32("age");


                        Student student = new Student();
                        student.id = idx;
                        student.create_time = create_time;
                        student.update_time = update_time;
                        student.note = note;
                        student.number = number;
                        student.name = name;
                        student.age = age;
                        list.Add(student);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }

            MyPage<Student> myPage = new MyPage<Student>();
            myPage.page = page;
            myPage.size = size;
            myPage.content = list;

            int totalCounts = 0;

            String sql2 = "select count(id) as totalCounts from t_student";

            try
            {
                conn.Open();
                MySqlCommand com2 = new MySqlCommand(sql2, conn);
                MySqlDataReader reader2 = com2.ExecuteReader();
                if (reader2.HasRows)
                {
                    reader2.Read();
                    totalCounts = reader2.GetInt32("totalCounts");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }

            if (totalCounts == 0) {
			    myPage.totalElements = 0;
			    myPage.totalPages = 0;
		    } else {
			    int totalPages = (totalCounts - 1) / size + 1;
                myPage.totalElements = totalCounts;
			    myPage.totalPages = totalPages;
		    }

		    return myPage;
        }

        public int delete(int id)
        {
            int affectedRowNum = -1;
            string queryStr = "delete from t_student where id = " + id;
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                MySqlCommand com = new MySqlCommand(queryStr, conn);
                affectedRowNum = com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }

            return affectedRowNum;
        }

        public Student add(Student student)
        {
            Student student2 = null;
            int num = -1;
            string sql = "insert into t_student (note, number, name, age) values (@note,@number,@name,@age)";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                MySqlCommand com = new MySqlCommand();
                com.CommandText = sql;
                com.Connection = conn;
                com.Parameters.AddWithValue("@note", student.note);
                com.Parameters.AddWithValue("@number", student.number);
                com.Parameters.AddWithValue("@name", student.name);
                com.Parameters.AddWithValue("@age", student.age);
                num = com.ExecuteNonQuery();
                long id = com.LastInsertedId;
                student2 = get(Convert.ToInt32(id));                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }

            return student2;
            
        }

        public Student update(Student entity, Dictionary<string, object> map)
        {
            int id = entity.id;
            Student student2 = null;
            int num = -1;
            

            //以下字段不允许通过update接口更新
            map.Remove("id");  //id不能更新
            map.Remove("create_time"); //createTime在创建记录时由数据库自动生成
            map.Remove("update_time"); //updateTime在每次更新记录时由数据库自动生成

            if (map.Keys.Count == 0)
            {
                return entity;
            }
            string sql2 = "";
            foreach (string key in map.Keys)
            {
                sql2 += "," + key + " = @" + key;
            }
            sql2 = sql2.Substring(1);
            string sql = "update t_student set " + sql2 + " where id = @id";            

            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                MySqlCommand com = new MySqlCommand();
                com.CommandText = sql;
                com.Connection = conn;
                foreach (string key in map.Keys)
                {
                    com.Parameters.AddWithValue("@" + key, map[key]);
                }
                com.Parameters.AddWithValue("@id", id);
                num = com.ExecuteNonQuery();
                student2 = get(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }

            return student2;

        }
    }
}