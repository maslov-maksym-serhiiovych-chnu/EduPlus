create or replace function read_all_course_users()
    returns table
            (
                id        integer,
                course_id integer
            )
    language plpgsql
as
$$
begin
    return query select * from course_users;
end;
$$;