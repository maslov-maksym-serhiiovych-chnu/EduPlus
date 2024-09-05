create or replace function read_course_user_by_id(p_id integer)
    returns table
            (
                id        integer,
                course_id integer
            )
    language plpgsql
as
$$
begin
    return query
        select *
        from course_users
        where id = p_id;
end;
$$;