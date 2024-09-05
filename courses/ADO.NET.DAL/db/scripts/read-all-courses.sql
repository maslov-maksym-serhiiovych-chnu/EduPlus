create or replace function read_all_courses()
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
    return query select * from courses;
end;
$$;