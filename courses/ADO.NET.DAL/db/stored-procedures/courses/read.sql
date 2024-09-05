create or replace function read(p_id serial)
    returns table
            (
                id          serial,
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