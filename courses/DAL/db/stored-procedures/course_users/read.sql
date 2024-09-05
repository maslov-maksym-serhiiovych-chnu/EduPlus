create or replace function read(p_id serial)
    returns table
            (
                id        serial,
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