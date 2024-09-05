create or replace procedure update_course_user(p_id integer, p_course_id integer)
    language plpgsql
as
$$
begin
    update course_users
    set course_id = p_course_id
    where id = p_id;
end;
$$;