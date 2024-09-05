create or replace function read_course(p_id integer)
    returns table
            (
                id          integer,
                name        varchar(255),
                description text
            )
    language plpgsql
as
$$
begin
    return query
        select *
        from courses
        where id = p_id;
end;
$$;