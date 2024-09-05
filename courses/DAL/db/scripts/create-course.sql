create procedure create_course(p_name varchar(255), p_description text)
    language plpgsql
as
$$
begin
    insert into courses (name, description)
    values (p_name, p_description);
end;
$$;