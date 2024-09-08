using System.Data;
using DAL.Models;
using Dapper;
using Npgsql;

namespace DAL.Data;

public class CourseRepository(NpgsqlConnection connection)
{
    public async Task Create(Course course)
    {
        await connection.OpenAsync();

        const string sql = "insert into courses (name, description) values ($1, $2)";
        var param = new { course.Name, course.Description };
        await connection.ExecuteAsync(sql, param);
       
        await connection.CloseAsync();
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
        await connection.OpenAsync();

        const string sql = "select * from courses";
        var courses = await connection.QueryAsync<Course>(sql);

        await connection.CloseAsync();
        return courses;
    }

    public async Task<Course?> GetById(int id)
    {
        await connection.OpenAsync();

        const string sql = "select * from courses where id = $1";
        var param = new { id };
        Course? course = await connection.QuerySingleOrDefaultAsync<Course>(sql, param);

        await connection.CloseAsync();
        return course;
    }

    public async Task UpdateById(int id, Course course)
    {
        await connection.OpenAsync();

        const string sql = "update courses set name = $1, description = $2 where id = $3";
        var param = new { course.Name, course.Description, id };
        await connection.ExecuteAsync(sql, param);

        await connection.CloseAsync();
    }

    public async Task DeleteById(int id)
    {
        await connection.OpenAsync();

        const string sql = "delete from courses where id = $1";
        var param = new { id };
        await connection.ExecuteAsync(sql, param);

        await connection.CloseAsync();
    }
}