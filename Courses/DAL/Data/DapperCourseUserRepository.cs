using DAL.Models;
using Dapper;
using Npgsql;

namespace DAL.Data;

public class DapperCourseUserRepository(NpgsqlConnection connection)
{
    public async Task Create(CourseUser courseUser)
    {
        await connection.OpenAsync();

        const string insertCourseUser = "insert into course_users (course_id) values (@CourseId)";
        var param = new { courseUser.CourseId };
        await connection.ExecuteAsync(insertCourseUser, param);

        await connection.CloseAsync();
    }

    public async Task<IEnumerable<CourseUser>> GetAll()
    {
        await connection.OpenAsync();

        const string selectCourseUsers = "select * from course_users";
        var courseUsers = await connection.QueryAsync<CourseUser>(selectCourseUsers);

        await connection.CloseAsync();
        return courseUsers;
    }

    public async Task<CourseUser?> GetById(int id)
    {
        await connection.OpenAsync();

        const string selectCourseUserById = "select * from course_users where id = @Id";
        var param = new { id };
        CourseUser? courseUser = await connection.QuerySingleOrDefaultAsync<CourseUser>(selectCourseUserById, param);

        await connection.CloseAsync();
        return courseUser;
    }

    public async Task UpdateById(int id, CourseUser courseUser)
    {
        await connection.OpenAsync();

        const string updateCourseUserById = "update course_users set course_id = @CourseId where id = @Id";
        var param = new { courseUser.CourseId, id };
        await connection.ExecuteAsync(updateCourseUserById, param);

        await connection.CloseAsync();
    }

    public async Task DeleteById(int id)
    {
        await connection.OpenAsync();

        const string deleteCourseUserById = "delete from course_users where id = @Id";
        var param = new { id };
        await connection.ExecuteAsync(deleteCourseUserById, param);

        await connection.CloseAsync();
    }
}