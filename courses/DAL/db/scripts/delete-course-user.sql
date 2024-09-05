create procedure delete_course_user(p_id integer)
    language plpgsql
as
$$
begin
    delete
    from course_users
    where id = p_id;
end;
$$;