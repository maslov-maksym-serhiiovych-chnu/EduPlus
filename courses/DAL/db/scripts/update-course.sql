create procedure update_course(p_id integer, p_name varchar(255), p_description text)
    language plpgsql
as
$$
begin
    update courses
    set name        = p_name,
        description = p_description
    where id = p_id;
end;
$$;