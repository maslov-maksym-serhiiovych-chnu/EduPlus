create procedure delete_course(p_id integer)
    language plpgsql
as
$$
begin
    delete
    from courses
    where id = p_id;
end;
$$;