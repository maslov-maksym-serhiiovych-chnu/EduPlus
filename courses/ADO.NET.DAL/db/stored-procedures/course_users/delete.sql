create or replace procedure delete(p_id serial)
    language plpgsql
as
$$
begin
    delete
    from course_users
    where id = p_id;
end;
$$;